using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using BAL.Types;
using BAL.Business;
using BAL.Extensions;
using BAL.Forms;

[assembly: MenuAction("Dokumenty\\Dokumenty sprzedaży", MenuAction = MenuActionsType.OpenView, ViewType = typeof(Enova.Forms.Handel.DokumentyView),
    Options =  ActionOptions.WithoutSession, Priority=410)]

namespace Enova.Forms.Handel
{
    public class DokumentyView : GridViewWithEnovaApi<Enova.API.Handel.DokumentHandlowy>
    {
        #region Fields

        private BAL.Forms.Controls.DateFromToControl okresControl;
        //private Enova.Business.Old.Controls.KontrahentSelectControl kontrahentControl;
        private Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect kontrahentControl;
        private System.Windows.Forms.ComboBox definicjaComboBox;
        private Enova.API.CRM.Kontrahent kontrahent;
        private Enova.API.Handel.DefDokHandlowego definicja;

        #endregion

        #region Properties

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Methods

        public DokumentyView(string key, Enova.API.CRM.Kontrahent kontrahent, Enova.API.Handel.DefDokHandlowego definicja = null)
            : base(key)
        {
            this.kontrahent = kontrahent;
            this.definicja = definicja;
        }

        public DokumentyView() : this("DokumentyEnovaView", null) { }

        protected override API.Business.Table<API.Handel.DokumentHandlowy> CreateTable(Enova.API.Business.Session session)
        {
            return session.GetModule<Enova.API.Handel.HandelModule>().DokHandlowe;
        }

        protected override IList GetRows()
        {
            using (var s = Service.CreateSession())
            {
                var table = s.GetModule<Enova.API.Handel.HandelModule>().DokHandlowe;
                table.Filter = this.getFilter();
                var list = table.ToList();
                if (this.IsSorted && this.SortProperty != null)
                    list.Sort(new DokHandlowyComparer(this.SortProperty, this.SortDirection));
                return (IList)list;
            }
        }

        private string getFilter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Data >= ");
            sb.Append("'");
            sb.Append(okresControl.FromTo.From.Date.ToShortDateString());
            sb.Append("'");
            sb.Append(" AND Data <= ");
            sb.Append("'");
            sb.Append(okresControl.FromTo.To.Date.ToShortDateString());
            sb.Append("'");
            //sb.Append(" AND (Definicja.Symbol = 'FV' OR Definicja.Symbol = 'FK')");

            if (kontrahent != null)
            {
                sb.Append(" AND Kontrahent.ID = " + this.kontrahent.ID);
            }
            else if (kontrahentControl.Kontrahent != null)
            {
                sb.Append(" AND Kontrahent.Guid = ");
                sb.Append("'");
                sb.Append(kontrahentControl.Kontrahent.Guid);
                sb.Append("'");
            }


            var def = definicja != null ? definicja : (definicjaComboBox == null ? null : definicjaComboBox.SelectedItem) as Enova.API.Handel.DefDokHandlowego;

            if (def == null)
            {
                sb.Append(" AND (Definicja.Kategoria = 2 OR Definicja.Kategoria = 3)");
            }
            else
            {
                sb.Append(" AND Definicja.Symbol = '");
                sb.Append(def.Symbol);
                sb.Append("'");
            }

            return sb.ToString();
        }

        public override Type GetDataType()
        {
            return typeof(Enova.API.Handel.DokumentHandlowy);
        }

        public override string GetTitle()
        {
            return "Dokumenty sprzedaży";
        }

        public override IEnumerable<DataContextParam> GetParams()
        {
            var list = new List<DataContextParam>();
            if (definicja == null)
                list.Add(new DataContextParam("DefinicjaParam", "Definicja:", null) { ControlType = typeof(System.Windows.Forms.ComboBox) });
            list.Add(new DataContextParam("DataParam", "Data:", new PropertyPath(this.GetDataType(), "Data")) { ControlType = typeof(BAL.Forms.Controls.DateFromToControl) });
            if (kontrahent == null)
                list.Add(new DataContextParam("KontrahentParam", null, new PropertyPath(this.GetDataType(), "Kontrahent")) { ControlType = typeof(Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect) });
            return list;
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            switch (e.Param.Name)
            {
                case "DefinicjaParam":
                    this.definicjaComboBox = e.Control as System.Windows.Forms.ComboBox;
                    this.initDefinicjaComboBox();
                    break;
                case "DataParam":
                    this.okresControl = e.Control as BAL.Forms.Controls.DateFromToControl;
                    break;
                case "KontrahentParam":
                    this.kontrahentControl = e.Control as Enova.Forms.Controls.KontrahentEnovaCheckBoxSelect;
                    break;
            }
            base.OnInitParam(e);
        }

        private void initDefinicjaComboBox()
        {
            if (this.definicjaComboBox != null)
            {
                var service = Enova.API.EnovaService.Instance;
                if (service != null)
                {
                    using (var session = service.CreateSession())
                    {
                        this.definicjaComboBox.Items.Add("Razem");
                        var hm = session.GetModule<Enova.API.Handel.HandelModule>();
                        var defs = hm.DefDokHandlowych[Enova.API.Handel.KategoriaHandlowa.Sprzedaż].ToList();
                        defs.AddRange(hm.DefDokHandlowych[Enova.API.Handel.KategoriaHandlowa.KorektaSprzedaży].ToList());
                        defs = defs.Where(d => d.Blokada == false).OrderBy(d => d.Symbol).ToList();
                        this.definicjaComboBox.DisplayMember = "Symbol";
                        this.definicjaComboBox.Items.AddRange(defs.ToArray());
                        this.definicjaComboBox.SelectedIndex = 0;
                        this.definicjaComboBox.SelectionChangeCommitted += new EventHandler(definicjaComboBox_SelectionChangeCommitted);
                    }
                }
            }
        }

        private void definicjaComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Reload();
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            this.Reload();
            base.OnParamValueChanged(e);
        }

        public override string GetDefaultXmlDefinition()
        {
            return Enova.Forms.Properties.Resources.DokumentyEnovaView_grid;
        }

        public override void Reload()
        {
            base.Reload();
        }

        protected override IComparer GetSortComparer()
        {
            /*
            if (SortProperty != null)
                return new DokHandlowyComparer(SortProperty, SortDirection);
            else if (SortPropertyPath != null)
                return new DokHandlowyComparer(SortPropertyPath, SortDirection);
                */
            return null;
        }

        #endregion

        #region Nested Types

        public class DokHandlowyComparer : IComparer, IComparer<API.Handel.DokumentHandlowy>
        {
            #region fields

            private PropertyDescriptor sortProperty;
            private PropertyDescriptorPath sortPropertyPath;
            private ListSortDirection sortDirection;
            private IComparer defaultComparer;

            #endregion

            #region Methods

            public DokHandlowyComparer(PropertyDescriptor sortProperty, ListSortDirection sortDirection)
            {
                this.sortProperty = sortProperty;
                this.sortDirection = sortDirection;

                var type = typeof(Comparer<>).MakeGenericType(this.sortProperty.PropertyType);
                var pinfo = type.GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
                this.defaultComparer = (IComparer)pinfo.GetValue(null, null);

            }

            public DokHandlowyComparer(PropertyDescriptorPath sortPropertyPath, ListSortDirection sortDirection)
            {
                this.sortPropertyPath = sortPropertyPath;
                this.sortDirection = sortDirection;

                var type = typeof(Comparer<>).MakeGenericType(this.sortPropertyPath.Last.PropertyType);
                var pinfo = type.GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
                this.defaultComparer = (IComparer)pinfo.GetValue(null, null);
            }

            public int Compare(API.Handel.DokumentHandlowy x, API.Handel.DokumentHandlowy y)
            {

                object val1 = null;
                object val2 = null;

                if (this.sortProperty != null)
                {
                    val1 = this.sortProperty.GetValue(x);
                    val2 = this.sortProperty.GetValue(y);
                }
                else
                {
                    val1 = this.sortPropertyPath.GetValue(x);
                    val2 = this.sortPropertyPath.GetValue(y);
                }

                if (val1 == null && val2 != null)
                    return this.sortDirection == ListSortDirection.Ascending ? -1 : 1;
                if (val1 != null && val2 == null)
                    return this.sortDirection == ListSortDirection.Ascending ? 1 : -1;
                if (val1 == null && val2 == null)
                    return 0;

                if (val1 is IComparable)
                    return this.sortDirection == ListSortDirection.Ascending ? ((IComparable)val1).CompareTo(val2) : ((IComparable)val2).CompareTo(val1);
                return this.sortDirection == ListSortDirection.Ascending ? this.defaultComparer.Compare(val1, val2) : this.defaultComparer.Compare(val2, val1);

            }

            public int Compare(object x, object y)
            {
                return Compare((API.Handel.DokumentHandlowy)x, (API.Handel.DokumentHandlowy)y);
            }


            #endregion
        }

        #endregion
    }
}
