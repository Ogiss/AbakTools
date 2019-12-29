using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

/*
[assembly: RowMap(
    "Soneta.Business.Db.DictionaryItem",
    "Dictionary",
    typeof(Enova.API.Business.IDictionaryItem),
    typeof(Enova.API.Connector.Business.DictionaryItem),
    typeof(Enova.API.Business.IBusinessModule))]
 */

namespace Enova.API.Connector.Business
{
    internal class DictionaryItem : GuidedRow, API.Business.DictionaryItem
    {
        public string Category
        {
            get { return (string)GetValue("Category"); }
        }

        public string Value
        {
            get { return (string)GetValue("Value"); }
        }

        public int Lp
        {
            get { return (int)GetValue("Lp"); }
        }

        public string Path
        {
            get { return (string)GetValue("Path"); }
        }

        public API.Business.DictionaryItem Parent
        {
            get
            {
                var parent = GetValue("Parent");
                if (parent != null)
                    return new DictionaryItem() { EnovaObject = parent };
                return null;
            }
        }
    }
}
