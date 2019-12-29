using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//[assembly: TypeMap("Soneta.Handel.PozycjaDokHandlowego, Soneta.Handel",typeof(Enova.API.Handel.IPozycjaDokHandlowego), typeof(Enova.API.Connector.Handel.PozycjaDokHandlowego))]

namespace Enova.API.Connector.Handel
{
    internal class PozycjaDokHandlowego : Business.Row, Enova.API.Handel.PozycjaDokHandlowego
    {
        #region Properties

        public int Lp
        {
            get
            {
                return (int)GetValue("Lp");
            }
        }

        public int Ident
        {
            get
            {
                return (int)GetValue("Ident");
            }
        }

        public Enova.API.Towary.Towar Towar
        {
            get
            {
                return EnovaHelper.FromEnova<API.Towary.Towar>(GetValue("Towar"));
            }
            set
            {
                SetValue("Towar", value.EnovaObject);
            }
        }

        public double Ilosc
        {
            get
            {
                return (double)GetObjValue(GetValue("Ilosc"), "Value");

            }
            set
            {
                var symbol = (string)GetObjValue(GetValue("Ilosc"), "Symbol");
                var qty = Type.GetType("Soneta.Towary.Quantity, Soneta.Handel").GetConstructor(new Type[] { typeof(double), typeof(string) }).Invoke(new object[] { value, symbol });
                //var qty = Type.GetType("Soneta.Towary.Quantity, Soneta.Handel").GetConstructor(new Type[] { typeof(double), typeof(string) }).Invoke(new object[] { value, null });
                SetValue("Ilosc", qty);
            }
        }

        public double IloscMagazynu
        {
            get
            {
                return (double)GetObjValue(GetValue("IloscMagazynu"), "Value");

            }
            set
            {
                var symbol = (string)GetObjValue(GetValue("IloscMagazynu"), "Symbol");
                var qty = Type.GetType("Soneta.Towary.Quantity, Soneta.Handel").GetConstructor(new Type[] { typeof(double), typeof(string) }).Invoke(new object[] { value, symbol });
                SetValue("IloscMagazynu", qty);
            }
        }


        public decimal? Cena
        {
            get
            {
                return (decimal?)(double)GetObjValue(GetValue("Cena"), "Value");
            }
            set
            {
                if (value != null)
                {
                    var val = Type.GetType("Soneta.Types.DoubleCy, Soneta.Types").GetConstructor(new Type[] { typeof(decimal) }).Invoke(new object[] { value.Value });
                    SetValue("Cena", val);
                }
            }
        }

        public decimal? Rabat
        {
            get
            {
                try
                {
                    decimal d = decimal.Parse(GetValue("Rabat").ToString().Replace("%", "")) / 100;
                    return d;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                if (value != null)
                {
                    var val = Type.GetType("Soneta.Types.Percent, Soneta.Types").GetConstructor(new Type[] { typeof(decimal) }).Invoke(new object[] { value.Value });
                    SetValue("Rabat", val);
                }
            }
        }

        public API.Handel.BruttoNetto Suma
        {
            get { return FromEnova<API.Handel.BruttoNetto>("Suma"); }
        }

        public string TowarNazwa
        {
            get
            {
                return this.Towar != null ? this.Towar.Nazwa : null;
            }
        }

        public bool Korekta
        {
            get
            {
                return (bool)GetValue("Korekta");
            }
        }

        public Enova.API.Handel.PozycjaDokHandlowego PozycjaKorygowana
        {
            get
            {
                var poz = GetValue("PozycjaKorygowana");
                return poz == null ? null : new PozycjaDokHandlowego() { EnovaObject = poz };
            }
        }

        public API.Business.SubTable Obroty
        {
            get { return FromEnova<API.Business.SubTable>("Obroty"); }
        }

        public API.Towary.DefinicjaCeny DefinicjaCeny
        {
            get { return FromEnova<API.Towary.DefinicjaCeny>("DefinicjaCeny"); }
        }

        #endregion

        #region Methods

        public void UstawCenę(API.Towary.WyliczenieCeny wylicz, API.Towary.DefinicjaCeny definicjaCeny, bool wymuśZmianęIlości)
        {
            try
            {
                CallMethod("UstawCenę", ToEnova(wylicz), ToEnova(definicjaCeny), wymuśZmianęIlości);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
