using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Towary
{
    internal class CenyGrupowe : Business.GuidedTable<API.Towary.CenaGrupowa>, API.Towary.CenyGrupowe
    {

        #region Properties

        public override string TableName
        {
            get
            {
                return "CenyGrupowe";
            }
        }

        public IEnumerable<API.Towary.CenaGrupowa> this[API.CRM.Kontrahent kontrahent]
        {
            get { throw new NotImplementedException(); }
        }

        public API.Towary.CenaGrupowa this[API.CRM.Kontrahent kontrahent, API.Business.DictionaryItem grupa, API.Business.DictionaryItem grupaTowarowa]
        {
            get
            {
                var view = GetValue("WgGrupaTowarowa");
                if (view != null && kontrahent != null)
                {
                    var cg = GetObjValue(view, "Item", new Type[]{
                        Type.GetType("Soneta.Business.Db.DictionaryItem, Soneta.Business"),
                        Type.GetType("Soneta.CRM.Kontrahent, Soneta.CRM"),
                        Type.GetType("Soneta.Business.Db.DictionaryItem, Soneta.Business")
                    },
                    new object[]{
                        grupaTowarowa == null ? null : grupaTowarowa.EnovaObject,
                        kontrahent.EnovaObject,
                        grupa == null ? null : grupa.EnovaObject
                    });
                    return cg == null ? null : new CenaGrupowa() { EnovaObject = cg };
                }
                return null;
            }
        }

        public API.Towary.CenaGrupowa Create(API.Business.DictionaryItem grupaTowarowa, API.CRM.Kontrahent kontrahent)
        {
            var row = Type.GetType("Soneta.Towary.CenaGrupowa, Soneta.Handel").GetConstructor(new Type[]{
                Type.GetType("Soneta.Business.Db.DictionaryItem, Soneta.Business"),
                Type.GetType("Soneta.CRM.Kontrahent, Soneta.CRM")
            }).Invoke(new object[]{
                grupaTowarowa == null ? null : grupaTowarowa.EnovaObject,
                kontrahent == null ? null : kontrahent.EnovaObject
            });
            return row == null ? null : new CenaGrupowa() { EnovaObject = row };
        }

        #endregion

    }
}
