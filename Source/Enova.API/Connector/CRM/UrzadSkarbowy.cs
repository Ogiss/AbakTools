﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.CRM
{
    internal class UrzadSkarbowy : Business.GuidedRow, API.CRM.UrzadSkarbowy
    {
        public string Kod
        {
            get
            {
                return (string)FromEnova("Kod");
            }
            set
            {
                ToEnova("Kod", value);
            }
        }

        public string Nazwa
        {
            get
            {
                return (string)FromEnova("Nazwa");
            }
            set
            {
                ToEnova("Nazwa", value);
            }
        }

        public string NazwaFormatowana
        {
            get
            {
                return (string)FromEnova("NazwaFormatowana");
            }
            set
            {
                ToEnova("NazwaFormatowana", value);
            }
        }

        public string NazwaPierwszaLinia
        {
            get { return (string)FromEnova("NazwaPierwszaLinia"); }
        }

        public API.Core.Adres Adres
        {
            get { return FromEnova<API.Core.Adres>("Adres"); }
        }


        public bool Blokada
        {
            get { throw new NotImplementedException(); }
        }

        public decimal Rabat
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.RodzajPodmiotu RodzajPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        public API.Core.StatusPodmiotu StatusPodmiotu
        {
            get { throw new NotImplementedException(); }
        }

        API.Core.TypPodmiotu API.Core.IPodmiot.Typ
        {
            get { return FromEnova<API.Core.TypPodmiotu, API.Core.IPodmiot>("Typ"); }
        }

        public string NIP
        {
            get { throw new NotImplementedException(); }
        }

        public string EuVAT
        {
            get { throw new NotImplementedException(); }
        }
    }
}
