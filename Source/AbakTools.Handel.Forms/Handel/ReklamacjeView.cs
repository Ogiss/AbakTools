using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using AbakTools.CRM;
using AbakTools.Handel;

[assembly: MenuAction(
    "Reklamacje",
    ViewType = typeof(AbakTools.Handel.Forms.ReklamacjeView),
    MenuAction = MenuActionsType.OpenView,
    Priority = 870,
    Options=ActionOptions.WithoutSession
    )]

namespace AbakTools.Handel.Forms
{
    public class ReklamacjeView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.Reklamacja>
    {
        #region Fields
        private System.Windows.Forms.ComboBox agentComboBox;
        private Enova.Business.Old.DB.Web.Kontrahent kontrahent;
        private DateTime dateFrom;
        private DateTime dateTo;
        private ReklamacjaComparer sortComparer;

        #endregion

        #region Properties

        /*
        public override bool IsSorted
        {
            get
            {
                return true;
            }
        }

        public override ListSortDirection SortDirection
        {
            get
            {
                return ListSortDirection.Ascending;
            }
        }

        public override PropertyDescriptor SortProperty
        {
            get
            {
                return TypeDescriptor.GetProperties(typeof(Enova.Business.Old.DB.Web.Reklamacja))["Kolejnosc"];
            }
        }
         */

        /*
        public override bool SupportsSearching
        {
            get
            {
                return false;
            }
        }
         */

        public override string Key
        {
            get
            {
                return "Reklamacje" + (SelectionMode ? "Select" : "") + "View";
            }
        }

        #endregion

        #region Methods

        public ReklamacjeView()
        {
            var n = DateTime.Now;
            this.dateFrom = new DateTime(n.Year, n.Month, 1);
            this.dateTo = this.dateFrom.AddMonths(1).AddDays(-1);
        }

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Reklamacja> CreateTable()
        {
            return new Enova.Business.Old.Web.Reklamacje(DbContext) { DateFrom = this.dateFrom, DateTo = this.dateTo, Kontrahent = this.kontrahent };
        }


        public override object CreateData()
        {
            var data = (Enova.Business.Old.DB.Web.Reklamacja) base.CreateData();
            data.Guid = Guid.NewGuid();
            data.Synchronize = (int)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedNew;
            data.DataDodania = DateTime.Now;
            data.DataModyfikacji = data.DataDodania;
            int opt = (int)AbakTools.Core.OpcjeStatusuDokumentu.Domyslny;
            var status = DbContext.StatusyDokumentow.Where(r => r.Kategoria == "Reklamacje" && (r.OpcjeInt & opt) == opt).FirstOrDefault();
            if (status != null)
                data.ZmienStatus(status);
            data.Numer.Dokument = data;
            data.Numer.NumerPelny = Enova.Business.Old.DB.Web.NumerDokumentu.LiczSymbol(data.Definicja, data);
            data.Numer.Symbol = data.Numer.NumerPelny;

            return data;
        }

        public override IEnumerable<DataContextParam> GetParams()
        {
            return new DataContextParam[]{
                new DataContextParam("okresParam", "Okres:", PropertyPath.Create<Enova.Business.Old.DB.Web.Reklamacja>("DataDodania")){ControlType = typeof(Enova.Business.Old.Controls.DateTimeSpanControl)}
                ,new DataContextParam("przedstawicielParam","Przedstawiciel:", null){ ControlType = typeof(System.Windows.Forms.ComboBox) }
                //,new DataContextParam("kontrahentParam", "Kontrahent:", PropertyPath.Create<Enova.Business.Old.DB.Web.Reklamacja>("Kontrahent"))
            };
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            if (e.Param.Name == "przedstawicielParam")
            {
                agentComboBox = (System.Windows.Forms.ComboBox)e.Control;
                agentComboBox.Items.Add("-----------");
                agentComboBox.SelectedIndex = 0;
                foreach (var pr in DbContext.Kontrahenci.Where(r=>r.CzyAgent==true).OrderBy(r=>r.Kod))
                    agentComboBox.Items.Add(pr.Kod);
                agentComboBox.SelectionChangeCommitted += new EventHandler(agentComboBox_SelectionChangeCommitted);
            }
            base.OnInitParam(e);
        }

        private void agentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Reload();
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            base.OnParamValueChanged(e);

            switch (e.Param.Name)
            {
                case "kontrahentParam":
                    //this.kontrahent = (CRM.Kontrahent)((IValue)e.Control).Value;
                    this.kontrahent = (Enova.Business.Old.DB.Web.Kontrahent)((IValue)e.Control).Value;
                    break;
                case "okresParam":
                    var c = (Enova.Business.Old.Controls.DateTimeSpanControl)e.Control;
                    this.dateFrom = c.DateFrom;
                    this.dateTo = c.DateTo;
                    break;
            }

            this.Reload();
           
        }

        public override void Reload()
        {
            var table = this.Table as Enova.Business.Old.Web.Reklamacje;
            if (table != null)
            {
                table.DateFrom = this.dateFrom;
                table.DateTo = this.dateTo;
                table.Kontrahent = this.kontrahent;
                table.PrzedstawicielKod = agentComboBox.SelectedIndex <= 0 ? null : agentComboBox.SelectedItem.ToString();
                table.Reload();
            }
            base.Reload();
        }

        public override string GetDefaultXmlDefinition()
        {
            return AbakTools.Handel.Forms.Properties.Resources.Reklamacje_grid;
        }

        protected override void OnParentFormChanged(EventArgs e)
        {
            base.OnParentFormChanged(e);
        }

        /*
        protected override IComparer GetSortComparer()
        {
            if (sortComparer == null)
                sortComparer = new ReklamacjaComparer();
            return sortComparer;
        }
         */

        protected override IList GetRows()
        {
            return base.GetRows();
        }

        #endregion

        #region Nested Types

        public class ReklamacjaComparer : IComparer
        {

            public int Compare(object x, object y)
            {
                var c = ((Enova.Business.Old.DB.Web.Reklamacja)x).Kolejnosc.CompareTo(((Enova.Business.Old.DB.Web.Reklamacja)y).Kolejnosc);
                if (c == 0)
                    return ((Enova.Business.Old.DB.Web.Reklamacja)x).Numer.NumerPelny.CompareTo(((Enova.Business.Old.DB.Web.Reklamacja)y).Numer.NumerPelny);
                return c;
            }
        }

        #endregion
    }
}
