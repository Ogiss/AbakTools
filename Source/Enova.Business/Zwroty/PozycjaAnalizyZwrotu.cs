using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Zwroty
{
    public class PozycjaAnalizyZwrotu : IEquatable<PozycjaAnalizyZwrotu>, IComparable<PozycjaAnalizyZwrotu>
    {
        #region Fields

        private Guid guid;
        private AnalizaZwrotu analizaZwrotu;
        internal Towar towar;
        internal double ilosc;
        internal double iloscSkorygowana;
        internal int ident;
        private DokumentyAnalizyZwrotu dokumenty;
        private DokumentyAnalizyZwrotu dokumentyWgIlosciPozycji;
        private DokumentyAnalizyZwrotu dokumentyKorygowane;

        #endregion

        #region Properties

        public Towar Towar
        {
            get { return this.towar; }
        }

        public double Ilosc
        {
            get { return this.ilosc; }
        }

        public double IloscSkorygowana
        {
            get { return this.iloscSkorygowana; }
        }

        public double PozostaloDoSkorygowania
        {
            get { return this.ilosc - this.iloscSkorygowana; }
        }

        public int Ident
        {
            get { return this.ident; }
        }

        public Guid Guid
        {
            get { return this.guid; }
        }

        public DokumentyAnalizyZwrotu Dokumenty
        {
            get
            {
                return this.dokumenty;
            }
        }

        public DokumentyAnalizyZwrotu DokumentyWgIlosciPozycji
        {
            get
            {
                if (this.dokumentyWgIlosciPozycji == null)
                    this.dokumentyWgIlosciPozycji = new DokumentyAnalizyZwrotu(this.analizaZwrotu, this.dokumenty, new DokumentAnalizyZwrotu.IloscPozycjiComparer());
                return this.dokumentyWgIlosciPozycji;
            }
        }

        public DokumentyAnalizyZwrotu DokumentyKorygowane
        {
            get
            {
                if (this.dokumentyKorygowane == null)
                    this.Koryguj();
                return this.dokumentyKorygowane;
            }
        }

        public void RemoveDokument(DokumentAnalizyZwrotu dokument, bool regenerateColl = true)
        {
            if (this.dokumenty.Contains(dokument))
            {
                this.dokumenty.Remove(dokument);
                dokument.Pozycje.Remove(this);
                if (this.dokumentyKorygowane != null && this.dokumentyKorygowane.Contains(dokument))
                    this.dokumentyKorygowane.Remove(dokument);
                if (regenerateColl)
                {
                    if (this.dokumentyWgIlosciPozycji != null)
                    {
                        this.dokumentyWgIlosciPozycji = new DokumentyAnalizyZwrotu(this.analizaZwrotu, this.dokumenty, new DokumentAnalizyZwrotu.IloscPozycjiComparer());
                    }
                }
            }
        }

        public void RemoveDokumenty()
        {
            foreach (var dok in this.dokumenty.ToList())
                this.RemoveDokument(dok,false);
            this.dokumentyWgIlosciPozycji = null;
        }

        #endregion

        #region Methods

        public PozycjaAnalizyZwrotu(AnalizaZwrotu analizaZwrotu, Towar towar, double ilosc)
        {
            this.guid = Guid.NewGuid();
            this.analizaZwrotu = analizaZwrotu;
            this.towar = towar;
            this.ilosc = ilosc;
            this.dokumenty = new DokumentyAnalizyZwrotu(this.analizaZwrotu);
        }

        public bool GetHasDokument(string documentName)
        {
            if (documentName.StartsWith("Dokument"))
            {
                int idx;
                if (int.TryParse(documentName.Substring(8), out idx))
                {
                    DokumentAnalizyZwrotu dokument = this.analizaZwrotu.Dokumenty[idx];
                    if (dokument != null)
                    {
                        return this.dokumenty.Contains(dokument);
                    }
                }
            }
            return false;
        }

        public bool GetHasDokumentKorygowany(string documentName)
        {
            if (documentName.StartsWith("Dokument"))
            {
                int idx;
                if (int.TryParse(documentName.Substring(8), out idx))
                {
                    DokumentAnalizyZwrotu dokument = this.analizaZwrotu.Dokumenty[idx];
                    if (dokument != null)
                    {
                        return this.DokumentyKorygowane.Contains(dokument);
                    }
                }
            }
            return false;
        }

        public DokumentAnalizyZwrotu GetDokumentByName(string name)
        {
            var dokument = this.analizaZwrotu.GetDokumentByName(name);
            if (dokument != null && this.dokumenty.Contains(dokument))
                return dokument;
            return null;

        }

        public double GetIlosciPoKorektech()
        {
            double ilosc = 0;
            foreach (var dok in this.Dokumenty)
                ilosc += dok.GetIloscPoKorektach(this);
            return ilosc;
        }

        public void Koryguj()
        {
            this.iloscSkorygowana = 0;
            this.dokumentyKorygowane = new DokumentyAnalizyZwrotu(this.analizaZwrotu);
            foreach (var dok in this.DokumentyWgIlosciPozycji)
            {
                dok.SetKorygowany(this, true);
                if (this.PozostaloDoSkorygowania == 0)
                    break;
            }
        }

        public override string ToString()
        {
            return this.towar.Kod + " : " + this.ilosc.ToString() + " (" + this.Dokumenty.Count + ")";
        }

        public bool Equals(PozycjaAnalizyZwrotu p)
        {
            return p.guid == this.guid;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((PozycjaAnalizyZwrotu)obj);
        }

        public override int GetHashCode()
        {
            return this.guid.GetHashCode();
        }

        public int CompareTo(PozycjaAnalizyZwrotu poz)
        {
            return this.towar.Kod.CompareTo(poz.towar.Kod);
        }


        #endregion
    }
}
