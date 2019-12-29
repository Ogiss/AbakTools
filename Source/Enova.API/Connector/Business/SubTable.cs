using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.SubTable, Soneta.Business", typeof(Enova.API.Business.SubTable), typeof(Enova.API.Connector.Business.SubTable))]

namespace Enova.API.Connector.Business
{
    internal class SubTable : API.Types.ObjectBase, API.Business.SubTable
    {
        public API.Business.Table BaseTable
        {
            get { return null; }
        }

        public bool IsEmpty
        {
            get { return (bool)GetValue("IsEmpty"); }
        }

        public System.Collections.ICollection Loaded
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {

            return new Business.EnovaEnumerator() { EnovaObject = ((IEnumerable)EnovaObject).GetEnumerator() };
        }

        public API.Business.Session Session
        {
            get { return new Business.Session() { EnovaObject = GetValue("Session") }; }
        }

        public API.Business.View CreateView()
        {
            return new View() { EnovaObject = CallMethod("CreateView") };
        }

        public Type GetRowType()
        {
            return (Type)CallMethod("GetRowType");
        }

    }
}
