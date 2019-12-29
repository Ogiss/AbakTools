using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class Obrot : Business.Row , API.Magazyny.Obrot
    {
        public Types.Time Czas
        {
            get { return EnovaHelper.FromEnova<Types.Time>(GetValue("czas")); }
        }

        public Types.Date Data
        {
            get { return EnovaHelper.FromEnova<Types.Date>(GetValue("Data")); }
        }

        public API.Towary.Quantity Ilosc
        {
            get
            {
                return EnovaHelper.FromEnova<API.Towary.Quantity>(GetValue("Ilosc"));
            }
            set
            {
                SetValue("Ilosc", EnovaHelper.ToEnova(value));
            }
        }

        public API.Magazyny.KorektaObrotu Korekta
        {
            get
            {
                return (API.Magazyny.KorektaObrotu)(int)GetValue("Korekta");
            }
            set
            {
                var e = Enum.ToObject(Type.GetType("Soneta.Magazyny.KorektaObrotu, Soneta.Handel"), (int)value);
                SetValue("Korekta", e);
            }
        }

        public API.Magazyny.Magazyn Magazyn
        {
            get { return FromEnova<API.Magazyny.Magazyn>("Magazyn"); }
        }

        public API.Magazyny.OkresMagazynowy Okres
        {
            get { return FromEnova<API.Magazyny.OkresMagazynowy>("Okres"); }
        }

        public API.Magazyny.PartiaTowaru this[API.Magazyny.KierunekPartii kierunek]
        {
            get
            {
                var e = EnovaHelper.ToEnova(kierunek);
                return EnovaHelper.FromEnova<API.Magazyny.PartiaTowaru>(GetObjValue(EnovaObject, "Item", new Type[] { e.GetType() }, new object[] { e }));
            }
        }

        public API.Magazyny.PartiaTowaru Przychod
        {
            get { return FromEnova<API.Magazyny.PartiaTowaru>("Przychod"); }
        }

        public API.Magazyny.PartiaTowaru Rozchod
        {
            get { return FromEnova<API.Magazyny.PartiaTowaru>("Rozchod"); }
        }

        public API.CRM.Kontrahent PrzychodKontrahent
        {
            get { return FromEnova<API.CRM.Kontrahent>("PrzychodKontrahent"); }
        }

        public API.CRM.Kontrahent RozchodKontrahent
        {
            get { return FromEnova<API.CRM.Kontrahent>("RozchodKontrahent"); }
        }

        public API.Magazyny.Obrot Stornowany
        {
            get
            {
                return FromEnova<API.Magazyny.Obrot>("Stornowany");
            }
            set
            {
                ToEnova("Stornowany", value);
            }
        }

        public API.Towary.Towar Towar
        {
            get { return EnovaHelper.FromEnova<API.Towary.Towar>("Towar"); }
        }

        public decimal Marża
        {
            get { return (decimal)GetValue("Marża"); }
        }

        public double MarżaJednostkowa
        {
            get { return (double)GetValue("MarżaJednostkowa"); }
        }

        public bool MinimalnaMarża
        {
            get { return (bool)GetValue("MinimalnaMarża"); }
        }

        public Types.Percent ProcentMarży
        {
            get { return EnovaHelper.FromEnova<Types.Percent>(GetValue("ProcentMarży")); }
        }

        public Types.Percent ProcentNarzutu
        {
            get { return EnovaHelper.FromEnova<Types.Percent>(GetValue("ProcentNarzutu")); }
        }

        public API.Magazyny.Obrot Stornujący
        {
            get { return FromEnova<API.Magazyny.Obrot>("Stornujący"); }
        }

        public bool UjemnaMarża
        {
            get { return (bool)GetValue("UjemnaMarża"); }
        }

        public bool Zamkniety
        {
            get { return (bool)GetValue("Zamkniety"); }
        }
    }
}
