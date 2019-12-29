using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;

namespace Enova.API.Core
{
        public interface IKontrahent : IPodmiot, INipHost, IEuVatHost, IRow, ISessionable
        {
            Adres Adres { get; }
            string EMAIL { get; set; }
            string EuVAT { get; }
            string Kod { get; }
            //Soneta.Core.Kontakt Kontakt { get; }
            SubTable Lokalizacje { get; }
            string Nazwa { get; }
            string NIP { get; }
            //Soneta.Core.Osoba Osoba { get; }
            View OsobyZOsobyKontrahent { get; }
            SubTable Projekty { get; }
            SubTable Urzadzenia { get; }
            //Soneta.Business.MemoText Uwagi { get; }
            SubTable Zadania { get; }
        }
}
