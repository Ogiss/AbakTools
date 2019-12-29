using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;
using Enova.API.Core;
using Enova.API.CRM;

namespace Enova.API.Kasa
{
    public interface IBank : IKontrahent, IPodmiot, INipHost, IEuVatHost,IRow, ISessionable
    {
        // Properties
        Adres Adres { get; }
        string Kierunek { get; }
        string Kod { get; }
        string Nazwa { get; }
        string NIP { get; }
        string SWIFT { get; }
    }




}
