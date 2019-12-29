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
                return (Types.FromTo)EnovaHelper.FromEnova(GetValue("Okres"));
            }
            set
            {
                SetValue("Okres", EnovaHelper.ToEnova(value));
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
            get { return (API.Magazyny.OkresMagazynowy)EnovaHelper.FromEnova(GetValue("Poprzedni")); }
        }

        public string ZamkniętyText
        {
            get { return (string)GetValue("ZamkniętyText"); }
        }
    }
}
