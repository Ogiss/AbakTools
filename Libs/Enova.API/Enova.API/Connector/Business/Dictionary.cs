using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Dictionary : Business.GuidedTable<API.Business.DictionaryItem>, API.Business.Dictionary
    {
        public override string TableName
        {
            get
            {
                return "Dictionary";
            }
        }

        public IEnumerable<API.Business.DictionaryItem> this[string category]
        {
            get
            {
                var enumerable = GetObjValue(GetValue("WgDataContext"), "Item", new Type[]{
                    Type.GetType("Soneta.Business.App.IOddzialFirmy, Soneta.Business"),
                    typeof(string)
                }, new object[]{
                    null,
                    category
                });
                return new Business.EnovaEnumerable<API.Business.DictionaryItem>(enumerable);
            }
        }

        public IEnumerable<API.Business.DictionaryItem> WgParent(API.Business.DictionaryItem parent)
        {
            var enumerable = GetObjValue(GetValue("WgParent"), "Item",
                new Type[] { Type.GetType("Soneta.Business.Db.DictionaryItem, Soneta.Business") },
                new object[] { EnovaHelper.ToEnova(parent) }
                );
            return new Business.EnovaEnumerable<API.Business.DictionaryItem>(enumerable);
        }
    }
}
