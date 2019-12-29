using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Core.NumerDokumentu, Soneta.Core", null, typeof(Enova.API.Core.NumerDokumentu))]

namespace Enova.API.Core
{
    public class NumerDokumentu : Types.ObjectBase
    {
        //private IDokument Dokument { get; }
        public int Numer
        {
            get { return (int)GetValue("Numer"); }
            set { SetValue("Numer", value); }
        }

        public string NumerPelny
        {
            get { return (string)GetValue("NumerPelny"); }
            set { SetValue("NumerPelny", value); }
        }

        public string Pelny
        {
            get { return (string)GetValue("Pelny"); }
        }

        public string Symbol
        {
            get { return (string)GetValue("Symbol"); }
            set { SetValue("Symbol", value); }
        }

        public void PrzeliczSymbol()
        {
            CallMethod("PrzeliczSymbol");
        }

    }
}
