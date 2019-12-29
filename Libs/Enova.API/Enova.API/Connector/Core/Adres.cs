using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Core
{
    internal class Adres : Business.Row, Enova.API.Core.Adres
    {
        public string Faks
        {
            get { return (string)GetValue("Faks"); }
            set { SetValue("Faks", value); }
        }

        public string Gmina
        {
            get { return (string)GetValue("Gmina"); }
            set { SetValue("Gmina", value); }
        }

        public string KodPocztowy
        {
            get { return (string)GetValue("KodPocztowyS"); }
            set { SetValue("KopPocztowyS", value); }
        }

        public string Miejscowosc
        {
            get { return (string)GetValue("Miejscowosc"); }
            set { SetValue("Miejscowosc", value); }
        }

        public string NrDomu
        {
            get { return (string)GetValue("NrDomu"); }
            set { SetValue("NrDomu", value); }
        }

        public string NrLokalu
        {
            get { return (string)GetValue("NrLokalu"); }
            set { SetValue("NrLocalu", value); }
        }

        public string Poczta
        {
            get { return (string)GetValue("Poczta"); }
            set { SetValue("Poczta", value); }
        }

        public string Telefon
        {
            get { return (string)GetValue("Telefon"); }
            set { SetValue("Telefon", value); }
        }

        public string Ulica
        {
            get { return (string)GetValue("Ulica"); }
            set { SetValue("Ulica", value); }
        }

    }
}
