using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Kasa
{
    internal class RozrachunkiInfoWorker : API.Types.ObjectBase, API.Kasa.RozrachunkiInfoWorker
    {
        public bool BrakPrawDostępu
        {
            get { return (bool)GetValue("BrakPrawDostępu"); }
        }

        public Types.Date CurrentDay
        {
            get
            {
                return FromEnova<Types.Date>("CurrentDay");
            }
            set
            {
                ToEnova("CurrentDay", value);
            }
        }

        public Types.Currency Należności
        {
            get { return FromEnova<Types.Currency>("Należności"); }
        }

        public Types.Currency NależnościNetto
        {
            get { return FromEnova<Types.Currency>("NależnościNetto"); }
        }

        public API.Kasa.IPodmiotKasowy Podmiot
        {
            get
            {
                return FromEnova<API.Kasa.IPodmiotKasowy>("Podmiot");
            }
            set
            {
                ToEnova("Podmiot", value);
            }
        }

        public Types.Currency Przeterminowane
        {
            get { return FromEnova<Types.Currency>("Przeterminowane"); }
        }

        public Types.Currency PrzeterminowaneNetto
        {
            get { return FromEnova<Types.Currency>("PrzeterminowaneNetto"); }
        }

        public Types.Currency PrzeterminowaneSaldo
        {
            get { return FromEnova<Types.Currency>("PrzeterminowaneSaldo"); }
        }

        public Types.Currency Saldo
        {
            get { return FromEnova<Types.Currency>("Saldo"); }
        }

        public Types.Currency Zobowiązania
        {
            get { return FromEnova<Types.Currency>("Zobowiązania"); }
        }

        public Types.Currency ZobowiązaniaNetto
        {
            get { return FromEnova<Types.Currency>("ZobowiązaniaNetto"); }
        }

        public Types.Currency ZobowiązaniePrzeterminowane
        {
            get { return FromEnova<Types.Currency>("ZobowiązaniePrzeterminowane"); }
        }
    }
}
