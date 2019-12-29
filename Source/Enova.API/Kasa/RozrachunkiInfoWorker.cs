using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Kasa.RozrachunkiInfoWorker, Soneta.Kasa", typeof(Enova.API.Kasa.RozrachunkiInfoWorker), typeof(Enova.API.Connector.Kasa.RozrachunkiInfoWorker))]

namespace Enova.API.Kasa
{
    public interface RozrachunkiInfoWorker : Types.IObjectBase
    {
        bool BrakPrawDostępu { get; }
        [/*Context(typeof(ActualDate), "Actual", Required = false),*/ Browsable(false)]
        Types.Date CurrentDay { get; set; }
        Types.Currency Należności { get; }
        Types.Currency NależnościNetto { get; }
        [/*Context,*/ Browsable(false)]
        IPodmiotKasowy Podmiot { get; set; }
        Types.Currency Przeterminowane { get; }
        Types.Currency PrzeterminowaneNetto { get; }
        Types.Currency PrzeterminowaneSaldo { get; }
        Types.Currency Saldo { get; }
        Types.Currency Zobowiązania { get; }
        Types.Currency ZobowiązaniaNetto { get; }
        Types.Currency ZobowiązaniePrzeterminowane { get; }
    }

}
