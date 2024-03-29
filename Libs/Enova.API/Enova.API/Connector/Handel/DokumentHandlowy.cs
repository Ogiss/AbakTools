﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Handel
{
    [TableName("DokHandlowe")]
    internal class DokumentHandlowy : Business.GuidedRow, Enova.API.Handel.DokumentHandlowy
    {
        #region Properties

        public API.Handel.DefDokHandlowego Definicja
        {
            get
            {
                var definicja = GetValue("Definicja");
                return definicja==null ? null :  new DefDokHandlowego() { EnovaObject = definicja };
            }
            set
            {
                SetValue("Definicja", value.EnovaObject);
            }
        }

        public API.Magazyny.Magazyn Magazyn
        {
            get
            {
                return FromEnova<API.Magazyny.Magazyn>("Magazyn");
            }
            set
            {
                SetValue("Magazyn", value.EnovaObject);
            }
        }

        public API.Magazyny.Magazyn MagazynDo
        {
            get
            {
                return FromEnova<API.Magazyny.Magazyn>("MagazynDo");
            }
            set
            {
                SetValue("MagazynDo", value.EnovaObject);
            }
        }

        public API.Types.Date Data
        {
            get
            {
                return EnovaHelper.FromEnova<API.Types.Date>(GetValue("Data"));
            }
            set
            {
                SetValue("Data", value.EnovaObject);
            }
        }

        public Types.Date DataOperacji
        {
            get
            {
                return FromEnova<Types.Date>("DataOperacji");
            }
            set
            {
                SetValue("DataOperacji", EnovaHelper.ToEnova(value));
            }
        }

        public API.Core.NumerDokumentu Numer
        {
            get { return FromEnova<API.Core.NumerDokumentu>("Numer"); }
        }


        public string NumerPelny
        {

            get
            {
                var numer = GetValue("Numer");
                return numer == null ? null : (string)GetObjValue(numer, "NumerPelny");
            }
        }

        public Enova.API.CRM.Kontrahent Kontrahent
        {
            get
            {
                var kontrahent = GetValue("Kontrahent");
                return kontrahent == null ? null : new CRM.Kontrahent() { EnovaObject = kontrahent };
            }
            set
            {
                this.SetValue("Kontrahent", value.EnovaObject);
            }
        }

        public Enova.API.CRM.Kontrahent Odbiorca
        {
            get { return EnovaHelper.FromEnova<Enova.API.CRM.Kontrahent>(GetValue("Odbiorca")); }
            set { SetValue("Odbiorca", EnovaHelper.ToEnova(value)); }
        }

        public IEnumerable<Enova.API.Handel.PozycjaDokHandlowego> Pozycje
        {
            get
            {
                var list = new List<API.Handel.PozycjaDokHandlowego>();
                foreach (var poz in (IEnumerable)GetValue("Pozycje"))
                    list.Add(new PozycjaDokHandlowego() { EnovaObject = poz });
                return list;
            }
        }

        public API.Types.Currency WartoscNetto
        {
            get
            {
                return EnovaHelper.FromEnova<API.Types.Currency>(GetObjValue(GetValue("Suma"), "NettoCy"));
            }
        }

        public API.Types.Currency WartoscVat
        {
            get
            {
                return EnovaHelper.FromEnova<API.Types.Currency>(GetObjValue(GetValue("Suma"), "VATCy"));
            }
        }

        public API.Types.Currency WartoscBrutto
        {
            get
            {
                return EnovaHelper.FromEnova<API.Types.Currency>(GetObjValue(GetValue("Suma"), "BruttoCy"));
            }
        }

        public bool Korekta 
        {
            get
            {
                return (bool)GetValue("Korekta");
            }
        }

        public Enova.API.Handel.DokumentHandlowy DokumentKorygowany
        {
            get
            {
                var dok = GetValue("DokumentKorygowany");
                return dok == null ? null : new DokumentHandlowy() { EnovaObject = dok };
            }
        }

        public bool Zatwierdzony
        {
            get
            {
                return (bool)GetValue("Zatwierdzony");
            }
        }

        public bool Anulowany
        {
            get
            {
                return (bool)GetValue("Anulowany");
            }
        }

        public bool Bufor
        {
            get
            {
                return (bool)GetValue("Bufor");
            }
        }

        public Enova.API.Handel.StanDokumentuHandlowego Stan
        {
            get
            {
                return (API.Handel.StanDokumentuHandlowego)(int)GetValue("Stan");
            }
            set
            {
                var val = Enum.ToObject(Type.GetType("Soneta.Handel.StanDokumentuHandlowego, Soneta.Handel"), (int)value);
                SetValue("Stan", val);
            }
        }

        public string Opis
        {
            get
            {
                return (string)GetValue("Opis");
            }
            set
            {
                //SetValue("Opis", value);
                CallObjMethod(GetValue("Opis"), "Add", new Type[] { typeof(string) }, new object[] { value });
            }
        }

        public IEnumerable Platnosci
        {
            get { return new Business.EnovaEnumerable() { EnovaObject = GetValue("Platnosci") }; }
        }

        API.Core.IKontrahent API.Core.IDokumentHandlowy.Kontrahent
        {
            get { return (API.Core.IKontrahent)Kontrahent; }
        }


        API.Core.IDefinicjaDokumentu API.Core.IDokument.Definicja
        {
            get { return (API.Core.IDefinicjaDokumentu)Definicja; }
        }

        public API.Core.IPodmiot Podmiot
        {
            get { return FromEnova<API.Core.IPodmiot>("Podmiot"); }
        }

        public Types.Date DataWpływu
        {
            get { return FromEnova<Types.Date>("DataWpływu"); }
        }

        public API.Core.DokEwidencji Ewidencja
        {
            get { return FromEnova<API.Core.DokEwidencji>("Ewidencja"); }
        }

        public API.Core.TypDokumentu Typ
        {
            get { return FromEnova<API.Core.TypDokumentu>("Typ"); }
        }

        public Types.Currency Wartosc
        {
            get { return FromEnova<Types.Currency>("Wartosc"); }
        }

        public API.Handel.DokumentHandlowy DokumentMagazynowyGłówny
        {
            get { return FromEnova<API.Handel.DokumentHandlowy>("DokumentMagazynowyGłówny"); }
        }

        public API.Business.SubTable NadrzedneRelacje
        {
            get { return FromEnova<API.Business.SubTable>("NadrzedneRelacje"); }
        }

        public API.Business.SubTable PodrzedneRelacje
        {
            get { return FromEnova<API.Business.SubTable>("PodrzedneRelacje"); }
        }

        public API.Handel.BruttoNetto Suma
        {
            get { return FromEnova<API.Handel.BruttoNetto>("Suma"); }
        }

        public API.Handel.DokumentObcy Obcy
        {
            get { return FromEnova<API.Handel.DokumentObcy>("Obcy"); }
        }

        #endregion

        #region Methods

        public DokumentHandlowy(object dok)
        {
            this.EnovaObject = dok;
        }

        public DokumentHandlowy()
            : this(null)
        {
        }

        public API.Handel.PozycjaDokHandlowego PozycjaWgIdent(int ident)
        {
            return EnovaHelper.FromEnova<API.Handel.PozycjaDokHandlowego>(CallMethod("PozycjaWgIdent", ident));
        }


        public API.Handel.PozycjaDokHandlowego NowaPozycja(API.Towary.Towar towar, double ilosc)
        {
            /*
            var pdh = new Soneta.Handel.PozycjaDokHandlowego((Soneta.Handel.DokumentHandlowy)Record);
            Soneta.Handel.HandelModule.GetInstance(this).PozycjeDokHan.AddRow(pdh);
            pdh.Towar = (Soneta.Towary.Towar)((Business.Row)towar).Record;
            pdh.Ilosc = new Soneta.Towary.Quantity(ilosc);
            return new PozycjaDokHan() { Record = pdh };
             */
            var pdh = (API.Handel.PozycjaDokHandlowego)EnovaConnector.Instance.CreateObject(typeof(API.Handel.PozycjaDokHandlowego), null, new object[] { this });

            pdh.Towar = towar;
            pdh.Ilosc = ilosc;
            return pdh;
        }

        public Types.Date WyliczDateZaplaty(API.Core.KierunekPlatnosci kierunek)
        {
            throw new NotImplementedException();
        }

        public void UstawTermin(int dni)
        {
            /*
            var platnosci = GetValue("Platnosci") as IEnumerable;
            if(platnosci != null)
            {
                foreach(var platnosc in platnosci)
                {
                    var t = Type.GetType("Soneta.Kasa.IRozliczalny");
                    var p = platnosc.GetType().GetProperty("TerminDni");
                    if (p != null)
                        p.SetValue(platnosc, dni, null);
                }
            }
             */
            var termin = ((DateTime)this.Data).AddDays(dni);
            UstawTermin(termin);
        }

        public void UstawTermin(DateTime termin)
        {
            var date = EnovaHelper.ToEnova(termin);
            var platnosci = GetValue("Platnosci") as ICollection;
            if (platnosci != null)
            {
                if (platnosci.Count > 0)
                {
                    foreach (var platnosc in platnosci)
                    {
                        var p = platnosc.GetType().GetProperty("Termin");
                        if (p != null)
                            p.SetValue(platnosc, date, null);
                    }
                }
                else
                {

                }
            }

        }

        #endregion

        #region Nested Types

        #endregion

    }
}
