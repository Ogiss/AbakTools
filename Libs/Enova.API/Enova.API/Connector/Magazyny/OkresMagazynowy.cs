using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Magazyny
{
    internal class OkresMagazynowy : Business.GuidedRow, API.Magazyny.OkresMagazynowy
    {
        public Types.FromTo Okres
        {
            get
            {
                return FromEnova<Types.FromTo>("Okres");
            }
            set
            {
                ToEnova("Okres", value);
            }
        }

        public bool Zamkniety
        {
            get
            {
                return (bool)GetValue("Zamkniety");
            }
            set
            {
                SetValue("Zamkniety", value);
            }
        }

        public string Info
        {
            get { return (string)GetValue("Info"); }
        }

        public API.Magazyny.OkresMagazynowy Poprzedni
        {
            get { return FromEnova<API.Magazyny.OkresMagazynowy>("Poprzedni"); }
        }

        public string ZamkniętyText
        {
            get { return (string)GetValue("ZamkniętyText"); }
        }
    }
}
