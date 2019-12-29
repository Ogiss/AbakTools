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
            get { return (API.Magazyny.Magazyn)EnovaHelper.FromEnova(GetValue("Magazyn")); }
        }

        public API.Magazyny.OkresMagazynowy Okres
        {
            get { return (API.Magazyny.OkresMagazynowy)EnovaHelper.FromEnova(GetValue("Okres")); }
        }

        public API.Magazyny.PartiaTowaru this[API.Magazyny.KierunekPartii kierunek]
        {
            get
            {
                var e = EnovaHelper.ToEnova(kierunek);
                return (API.Magazyny.PartiaTowaru) EnovaHelper.FromEnova(GetObjValue(EnovaObject, "Item", new Type[] { e.GetType() }, new object[] { e }));
            }
        }

        public API.Magazyny.PartiaTowaru Przychod
        {
            get { return (API.Magazyny.PartiaTowaru)EnovaHelper.FromEnova(GetValue("Przychod")); }
        }

        public API.Magazyny.PartiaTowaru Rozchod
        {
            get { return (API.Magazyny.PartiaTowaru)EnovaHelper.FromEnova(GetValue("Rozchod")); }
        }

        public API.CRM.Kontrahent PrzychodKontrahent
        {
            get { return (API.CRM.Kontrahent)EnovaHelper.FromEnova(GetValue("PrzychodKontrahent")); }
        }

        public API.CRM.Kontrahent RozchodKontrahent
        {
            get { return (API.CRM.Kontrahent)EnovaHelper.FromEnova(GetValue("RozchodKontrahent")); }
        }

        public API.Magazyny.Obrot Stornowany
        {
            get
            {
                return (API.Magazyny.Obrot)EnovaHelper.FromEnova(GetValue("Stornowany"));
            }
            set
            {
                SetValue("Stornowany", EnovaHelper.ToEnova(value));
            }
        }

        public API.Towary.Towar Towar
        {
            get { return (API.Towary.Towar)EnovaHelper.FromEnova(GetValue("Towar")); }
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
            get { return (API.Magazyny.Obrot)EnovaHelper.FromEnova(GetValue("Stornujący")); }
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
