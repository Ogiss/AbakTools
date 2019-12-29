using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Old.Types;
using Enova.Business.Old;
using Enova.Business.Old.DB;

namespace Enova.Old.Handel
{
    internal sealed class InicjalizatorWaluty
    {
        // Fields
        private WeakReference _defRelacji;
        private WeakReference _dokument;
        private WeakReference _nadrzedny;
        private static readonly InicjalizatorWaluty instance = new InicjalizatorWaluty();

        // Methods
        private InicjalizatorWaluty()
        {
        }

        internal void Add(DokumentHandlowy dokument, DokumentHandlowy nadrzedny, DefRelacjiHandlowej defRelacji)
        {
            this._dokument = new WeakReference(dokument);
            this._nadrzedny = new WeakReference(nadrzedny);
            this._defRelacji = new WeakReference(defRelacji);
        }

        [Obsolete("Dorobić obsługe klasy Currency")]
        internal string GetWalutaPlatnosci(DokumentHandlowy dokument, out double kursWaluty)
        {
            InicjalizatorWalutyInfo info;
            DokumentHandlowy handlowy;
            kursWaluty = 0.0;
            if (!this.TryGetInicjalizatorWalutyInfo(dokument, out handlowy, out info))
            {
                //return dokument.BruttoCy.Symbol;
                return dokument.BruttoCySymbol;
            }
            var type = new
            {
                //nadrzedny = (handlowy == null) ? null : handlowy.BruttoCy.Symbol,
                nadrzedny = (handlowy == null) ? null : handlowy.BruttoCySymbol,
                dokument = this.GetWalutaPlatnosci(dokument, handlowy, info.WalutaPlatnosci)
            };
            //if (info.KursZNadrzednego && Currency.EqualSymbols(type.nadrzedny, type.dokument))
            if (info.KursZNadrzednego && type.nadrzedny == type.dokument)
            {
                kursWaluty = handlowy.KursWaluty;
            }
            return type.dokument;
        }

        private string GetWalutaPlatnosci(DokumentHandlowy dokument, DokumentHandlowy nadrzedny, ZrodloWaluty zrodloWaluty)
        {
            ZrodloWaluty waluty = zrodloWaluty;
            switch (waluty)
            {
                case ZrodloWaluty.ZNadrzednego:
                    //return nadrzedny.BruttoCy.Symbol;
                    return nadrzedny.BruttoCySymbol;

                case ZrodloWaluty.ZKartyKontrahenta:
                    {
                        DokumentHandlowy[] handlowyArray = new DokumentHandlowy[] { dokument, nadrzedny };
                        foreach (DokumentHandlowy handlowy in handlowyArray)
                        {
                            if ((handlowy != null) && (handlowy.Kontrahent != null))
                            {
                                if (handlowy.WalutaKontrahenta != null)
                                {
                                    return handlowy.WalutaKontrahenta.Symbol;
                                }
                                return this.GetWalutaPlatnosci(dokument, nadrzedny, ZrodloWaluty.ZDefinicji);
                            }
                        }
                        break;
                    }
                case (ZrodloWaluty.ZKartyKontrahenta | ZrodloWaluty.ZNadrzednego):
                    break;

                case ZrodloWaluty.ZDefinicjiPodrzednego:
                    return this.GetWalutaPlatnosci(dokument, nadrzedny, dokument.Definicja.InicjalizatorWalutyInfo.WalutaPlatnosci);

                case ZrodloWaluty.ZDefinicji:
                    //return dokument.Definicja.WalutaPlatnosci.Symbol;
                    return dokument.Definicja.WalutaPlatnosci.Symbol;

                default:
                    if (waluty == ZrodloWaluty.ZCennika)
                    {
                        throw new NotSupportedException(string.Format("Ustawienie {0} dla źr\x00f3dła waluty płatności nie jest obsługiwane.", zrodloWaluty));
                    }
                    break;
            }
            //return dokument.BruttoCy.Symbol;
            return dokument.BruttoCySymbol;
        }

        /*
        private string GetWalutaPozycja(PozycjaDokHandlowego pozycja, DokumentHandlowy nadrzedny, DefinicjaCeny defCeny, ZrodloWaluty zrodloWaluty)
        {
            ZrodloWaluty waluty = zrodloWaluty;
            switch (waluty)
            {
                case ZrodloWaluty.ZNadrzednego:
                    foreach (PozycjaDokHandlowego handlowego in pozycja.Nadrzędne)
                    {
                        if (handlowego.Dokument == nadrzedny)
                        {
                            //return handlowego.WartoscCy.Symbol;
                            return handlowego.WartoscCySymbol;
                        }
                    }
                    break;

                case ZrodloWaluty.ZKartyKontrahenta:
                    {
                        DokumentHandlowy[] handlowyArray = new DokumentHandlowy[] { pozycja.Dokument, nadrzedny };
                        foreach (DokumentHandlowy handlowy in handlowyArray)
                        {
                            if ((handlowy != null) && (handlowy.Kontrahent != null))
                            {
                                if (handlowy.WalutaKontrahenta != null)
                                {
                                    return handlowy.WalutaKontrahenta.Symbol;
                                }
                                return this.GetWalutaPozycja(pozycja, nadrzedny, defCeny, ZrodloWaluty.ZCennika);
                            }
                        }
                        break;
                    }
                case (ZrodloWaluty.ZKartyKontrahenta | ZrodloWaluty.ZNadrzednego):
                    break;

                case ZrodloWaluty.ZDefinicjiPodrzednego:
                    return this.GetWalutaPozycja(pozycja, nadrzedny, defCeny, pozycja.Dokument.Definicja.InicjalizatorWalutyInfo.WalutaPozycji);

                case ZrodloWaluty.ZDefinicji:
                    return pozycja.Dokument.Definicja.WalutaPlatnosci.Symbol;

                default:
                    {
                        if (((waluty != ZrodloWaluty.ZCennika) || (pozycja.Towar == null)) || (defCeny == null))
                        {
                            break;
                        }
                        CenaPozycjiWorker worker = new CenaPozycjiWorker
                        {
                            Pozycja = pozycja,
                            DefinicjaCeny = defCeny
                        };
                        return worker.Cena.Netto.Symbol;
                    }
            }
            return pozycja.Dokument.BruttoCy.Symbol;
        }
         */

        /*
        internal string GetWalutaPozycja(PozycjaDokHandlowego pozycja, DokumentHandlowy nadrzedny, DefinicjaCeny defCeny, string symbol)
        {
            InicjalizatorWalutyInfo info;
            if (((pozycja.Dokument.Status & RowStatus.Cloned) == RowStatus.Cloned) || ((nadrzedny != null) && FlagsHelper.Check(nadrzedny, Flags.ZmianaZatwierdzonegoDokumentu)))
            {
                return symbol;
            }
            if (!this.TryGetInicjalizatorWalutyInfo(pozycja.Dokument, out nadrzedny, out info))
            {
                return pozycja.Dokument.BruttoCy.Symbol;
            }
            return this.GetWalutaPozycja(pozycja, nadrzedny, defCeny ?? pozycja.Dokument.Definicja.Cena, info.WalutaPozycji);
        }
         */

        private bool TryGetInicjalizatorWalutyInfo(DokumentHandlowy dokument, out DokumentHandlowy nadrzedny, out InicjalizatorWalutyInfo info)
        {
            throw new NotImplementedException("InicjatorWaluty.TryGetInicjalizatorWalutyInfo(...)");
            /*
            info = dokument.Definicja.InicjalizatorWalutyInfo;
            nadrzedny = null;
            if ((((this._dokument != null) && this._dokument.IsAlive) && ((this._dokument.Target == dokument) && (this._defRelacji != null))) && ((this._defRelacji.IsAlive && (this._nadrzedny != null)) && this._nadrzedny.IsAlive))
            {
                DefRelacjiHandlowej target = this._defRelacji.Target as DefRelacjiHandlowej;
                if (target != null)
                {
                    nadrzedny = this._nadrzedny.Target as DokumentHandlowy;
                    info = (nadrzedny == null) ? null : target.Zachowanie.InicjalizatorWalutyInfo;
                }
            }
            return (info != null);
             */
        }

        // Properties
        internal static InicjalizatorWaluty Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
