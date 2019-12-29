using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class Magazyn : Business.GuidedRow, API.Magazyny.Magazyn
    {
        public string Symbol
        {
            get { return (string)GetValue("Symbol"); }
        }

        public string Nazwa
        {
            get { return (string)GetValue("Nazwa"); }
        }


        public bool Firmowy
        {
            get
            {
                var firma = this.Session.GetModule<API.Magazyny.MagazynyModule>().Magazyny.Firma;
                return this.Guid == firma.Guid;
            }
        }

    }
}
