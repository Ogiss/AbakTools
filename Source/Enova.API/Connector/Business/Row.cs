using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Row : API.Types.ObjectBase, API.Business.Row
    {

        #region Properties

        public int ID
        {
            get { return GetValue<int>("ID"); }
        }

        public API.Business.Session Session
        {
            get { return new Session() { EnovaObject = GetObjValue(EnovaObject, "Session") }; }
        }

        public API.Business.FeatureCollection Features
        {
            get { return new API.Business.FeatureCollection(this); }
        }

        public bool IsLive
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.IRow Parent
        {
            get { throw new NotImplementedException(); }
        }

        public string Prefix
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Row Root
        {
            get { throw new NotImplementedException(); }
        }

        public API.Business.Table Table
        {
            get { return FromEnova<API.Business.Table>("Table",Type.GetType("Soneta.Business.IRow, Soneta.Business")); }
        }

        public int TableHandle
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        public bool IsReadOnly()
        {
            return (bool)CallMethod("IsReadOnly");
        }

        public void Refresh()
        {
            var table = this.Table;
            var row = table[this.ID];
            this.EnovaObject = row.EnovaObject;
        }

        #endregion
    }
}
