using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Connector.Business
{
    internal class Table<T> : API.Types.ObjectBase, IEnumerable, API.Business.Table<T>, API.Business.ISessionable
        where T : class, API.Business.Row
    {

        #region Fields

        internal API.Business.Module module;
        private List<T> rows;
        private string filter;
        private API.Business.IFeatureDefinitions featureDefinitions;

        #endregion

        #region Properties

        public API.Business.Row this[int id]
        {
            get
            {
                var pinfo = this.EnovaObject.GetType().GetProperty("Item", new Type[] { typeof(int) });
                var row = pinfo.GetValue(EnovaObject, new object[] { id });
                return row == null ? (API.Business.Row)null : (API.Business.Row)CreateRow(row);
            }
        }

        public virtual string TableName
        {
            get { return GetValue<string>("TableName"); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                if (this.filter != value) ;
                {
                    rows = null;
                    filter = value;
                }
            }
        }

        public API.Business.Session Session
        {
            get { return new Session() { EnovaObject = GetObjValue(this.EnovaObject, "Session") }; }
        }

        public List<T> Rows
        {
            get
            {
                if (rows == null)
                    initRows();
                return rows;
            }
        }

        public API.Business.IFeatureDefinitions FeatureDefinitions
        {
            get
            {
                if (featureDefinitions == null)
                    featureDefinitions = new FeatureDefinitions(GetValue("FeatureDefinitions"));
                return featureDefinitions;
            }
        }

        #endregion

        #region Methods

        private void initRows()
        {
            var t = new EnovaTable() { EnovaObject = EnovaObject };
            var view = t.CreateView();
            if (!string.IsNullOrEmpty(this.Filter))
                view.Filter = this.Filter;
            rows = new List<T>();
            foreach (var r in view)
            {
                rows.Add((T)CreateRow(r));
            }
        }

        public virtual T CreateRow(object enovaRow)
        {
            return EnovaHelper.FromEnova<T>(enovaRow);
        }

        public API.Business.View CreateView()
        {
            var view = CallMethod("CreateView");
            return view == null ? null : new View() { EnovaObject = view };
        }


        public IEnumerator<T> GetEnumerator()
        {
            return Rows.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void AddRow(API.Business.Row row)
        {
            if (row != null && row.EnovaObject != null)
                CallMethodFull("AddRow", new Type[] { Type.GetType("Soneta.Business.Row, Soneta.Business") }, new object[] { row.EnovaObject });
        }

        public SubTable GetSubTable(string nazwa, params object[] args) 
        {
            if (args != null && args.Length > 0)
            {
                var p = GetValue(nazwa);
                if (p != null)
                {
                    Type[] types = null;
                    object[] index = null;
                    for (var i = args.Length; i > 0; i--)
                    {
                        if (args[i-1] == null)
                            continue;
                        if (types == null)
                        {
                            types = new Type[i];
                            index = new object[i];
                        }
                        object obj = ToEnova(args[i - 1]);
                        /*
                        if (obj is API.Types.IObjectBase)
                            obj = ((API.Types.IObjectBase)obj).EnovaObject;
                         */
                        types[i - 1] = obj.GetType();
                        index[i - 1] = obj;
                    }
                    var subTable = GetObjValue(p, "Item", types, index);
                    if (subTable != null)
                        return new SubTable() { EnovaObject = subTable };
                }
            }
            return null;
        }

        #endregion

    }
}
