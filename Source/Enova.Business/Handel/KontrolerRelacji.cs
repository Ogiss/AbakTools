using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;
using Enova.Old.Types;
using Enova.Old.Magazyny;
using Enova.Old.Towary;

namespace Enova.Old.Handel
{
    internal sealed class KontrolerRelacji
    {
        // Fields
        private readonly DokumentHandlowy _document;
        private readonly Mode _mode;
        private RelacjaHandlowa[] _relations;
        private Queue<PozycjaRelacjiHandlowej> _remove = new Queue<PozycjaRelacjiHandlowej>();
        private Queue<PozycjaDokHandlowego> _restore = new Queue<PozycjaDokHandlowego>();
        private Queue<RelacjaHandlowa> _split = new Queue<RelacjaHandlowa>();

        // Methods
        private KontrolerRelacji(DokumentHandlowy document, Mode mode)
        {
            this._document = document;
            this._mode = mode;
        }

        private bool Check()
        {
            if (this._document.SposobRozliczaniaNadrzednego == SposobRozliczaniaNadrzednego.Dynamicznie)
            {
                var lrn = this.Module.RelacjeHandlowe.WgPodrzednyTyp[this._document, TypRelacjiHandlowej.Kopiowania];
                var pozycje = this._document.Pozycje;
                switch (this._mode)
                {
                    case Mode.Restore:
                    case Mode.Remove:
                    case (Mode.Remove | Mode.Restore):
                        if (this._document.Definicja.KontrolerRelacjiInfo.PrzeliczajPozycjeRelacji)
                        {
                            this.Check(lrn, pozycje);
                        }
                        break;

                    case Mode.Split:
                        this.Check(lrn);
                        break;
                }
                if (this.ShouldRestore)
                {
                    this._relations = new RelacjaHandlowa[lrn.Count];
                    lrn.CopyTo(this._relations, 0);
                }
            }
            if (!this.ShouldRestore)
            {
                return this.ShouldSplit;
            }
            return true;
        }

        private void Check(RelacjeHandlowe lrn)
        {
            if (!lrn.IsEmpty)
            {
                foreach (RelacjaHandlowa handlowa in lrn)
                {
                    if (handlowa.Definicja.Zachowanie.UsuwajAutomatycznie && handlowa.Pozycje.IsEmpty)
                    {
                        this._split.Enqueue(handlowa);
                    }
                }
            }
        }

        private void Check(RelacjeHandlowe lrn, PozycjeDokHan lpd)
        {
            if (!lrn.IsEmpty && !lpd.IsEmpty)
            {
                foreach (PozycjaDokHandlowego handlowego in lpd)
                {
                    var relations = this.GetRelations(handlowego);
                    if (relations.IsEmpty)
                    {
                        this._restore.Enqueue(handlowego);
                    }
                    else
                    {
                        foreach (PozycjaRelacjiHandlowej handlowej in relations)
                        {
                            if ((handlowej.Relacja.Typ == TypRelacjiHandlowej.Kopiowania) && (handlowego.Towar != handlowej.Nadrzedna.Towar))
                            {
                                this._remove.Enqueue(handlowej);
                            }
                        }
                        this._restore.Enqueue(handlowego);
                    }
                }
            }
        }

        private bool Check(out Quantity mp, out IlośćWartość wp, out PozycjaRelacjiHandlowej pozr, RelacjaHandlowa rn, PozycjaDokHandlowego pn, PozycjaDokHandlowego pb)
        {
            pozr = null;
            mp = pn.IloscMagazynu;
            wp = pn.IlośćWartość;
            foreach (PozycjaRelacjiHandlowej handlowej in this.Module.PozRelHandlowej.WgNadrzednyDok[pn.Dokument, pn.Ident])
            {
                if (handlowej.Podrzedna == pb)
                {
                    pozr = handlowej;
                }
                else if (handlowej.Relacja.Typ == rn.Typ)
                {
                    mp -= handlowej.IloscMagazynu;
                    wp -= handlowej.IlośćWartość;
                }
            }
            return mp.IsPlus;
        }

        private PozRelHandlowej GetRelations(PozycjaDokHandlowego p)
        {
            return this.Module.PozRelHandlowej.WgPodrzednyDok[this._document, p.Ident];
        }

        internal static void Kontroluj(DokumentHandlowy d, Mode m)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            if (!d.Definicja.KontrolerRelacjiInfo.Disabled)
            {
                KontrolerRelacji relacji = new KontrolerRelacji(d, m);
                if (relacji.Check())
                {
                    if (relacji.ShouldRemove)
                    {
                        relacji.Remove();
                    }
                    if (relacji.ShouldRestore)
                    {
                        relacji.Restore();
                    }
                    if (relacji.ShouldSplit)
                    {
                        relacji.Split();
                    }
                }
            }
        }

        internal static void KontrolujDynamicznie(DokumentHandlowy d)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            if (!d.Definicja.KontrolerRelacjiInfo.Disabled && !d.Module.RelacjeHandlowe.WgPodrzednyTyp[d, TypRelacjiHandlowej.Kopiowania].IsEmpty)
            {
                d.SposobRozliczaniaNadrzednego = SposobRozliczaniaNadrzednego.Dynamicznie;
            }
        }

        private void Remove()
        {
            while (this.ShouldRemove)
            {
                PozycjaRelacjiHandlowej handlowej = this._remove.Dequeue();
                bool flag = handlowej.Table.KasujPodrzędny;
                try
                {
                    handlowej.Table.KasujPodrzędny = false;
                    handlowej.Delete();
                    continue;
                }
                finally
                {
                    handlowej.Table.KasujPodrzędny = flag;
                }
            }
        }

        private void Restore()
        {
            while (this.ShouldRestore)
            {
                PozycjaDokHandlowego pb = this._restore.Dequeue();
                IlośćWartość w = new IlośćWartość(pb.Ilosc, pb.WartoscCy);
                Quantity iloscMagazynu = pb.IloscMagazynu;
                int index = -1;
                while ((w.Ilość.IsPlus && iloscMagazynu.IsPlus) && (++index < this._relations.Length))
                {
                    this.Restore(this._relations[index], pb, ref w, ref iloscMagazynu);
                }
            }
        }

        private void Restore(RelacjaHandlowa rn, PozycjaDokHandlowego pb, ref IlośćWartość w, ref Quantity m)
        {
            throw new NotImplementedException("Enova.Handel.KontrolerRelacji.Restore(...)");
            /*
            foreach (PozycjaDokHandlowego handlowego in rn.Nadrzedny.Pozycje)
            {
                IlośćWartość wartość;
                Quantity quantity;
                PozycjaRelacjiHandlowej handlowej;
                if (((handlowego.Towar == pb.Towar) && w.Ilość.IsPlus) && (m.IsPlus && this.Check(out quantity, out wartość, out handlowej, rn, handlowego, pb)))
                {
                    if (handlowej == null)
                    {
                        handlowej = new PozycjaRelacjiHandlowej(rn, handlowego, pb, true);
                        this.Module.PozRelHandlowej.AddRow(handlowej);
                    }
                    IlośćWartość iw = (w.Ilość > wartość.Ilość) ? wartość : w;
                    Quantity im = (m > quantity) ? quantity : m;
                    iw = handlowego.IlośćWartość.Proporcja(iw.Ilość);
                    handlowej.Ustaw(iw, im, false);
                    w -= w.Proporcja(iw.Ilość);
                    m -= im;
                }
            }
             */
        }

        private void Split()
        {
            while (this.ShouldSplit)
            {
                this._split.Dequeue().Delete();
            }
        }

        // Properties
        private HandelModule Module
        {
            get
            {
                return this._document.Module;
            }
        }

        private bool ShouldRemove
        {
            get
            {
                return (((this._mode & Mode.Remove) > Mode.None) && (this._remove.Count > 0));
            }
        }

        private bool ShouldRestore
        {
            get
            {
                return (((this._mode & Mode.Restore) > Mode.None) && (this._restore.Count > 0));
            }
        }

        private bool ShouldSplit
        {
            get
            {
                return (((this._mode & Mode.Split) > Mode.None) && (this._split.Count > 0));
            }
        }

        // Nested Types
        [Flags]
        internal enum Mode
        {
            None = 0,
            Remove = 2,
            Restore = 1,
            Split = 4
        }
    }
}
