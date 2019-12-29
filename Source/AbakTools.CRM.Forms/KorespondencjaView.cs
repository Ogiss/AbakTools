using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using BAL.Forms;
using DBWeb = Enova.Business.Old.DB.Web;
using Enova.Business.Old.Types;

[assembly: MenuAction("Narzędzia\\Korespondencja seryjna", MenuAction = MenuActionsType.OpenView, ViewType = typeof(AbakTools.CRM.Forms.KorespondencjaView), Options = ActionOptions.WithoutSession, Priority = 610)]

namespace AbakTools.CRM.Forms
{
    public class KorespondencjaView : AbakTools.Forms.GridViewBase
    {
        #region Fields

        Enova.Business.Old.Web.RejestrKorespondencji table;
        BAL.Forms.Controls.ComboBox kontrachentSelect;
        private object reloadLock = new object();

        #endregion

        #region Properties
        #endregion

        #region Methods

        protected override System.Collections.IList GetRows()
        {
            if (table == null)
                initTable();
            return table;
        }

        private void initTable()
        {
            string kod = null;
            if (kontrachentSelect != null && kontrachentSelect.Value != null)
                kod = ((Enova.API.CRM.Kontrahent)kontrachentSelect.Value).Kod;

            table = new Enova.Business.Old.Web.RejestrKorespondencji(kod);
            if (this.SupportsSorting && IsSorted)
                this.table.Sort(this.GetSortComparer());
        }

        public override Type GetDataType()
        {
            return typeof(DBWeb.Korespondencja);
        }

        public override object CreateData()
        {
            var row = (DBWeb.Korespondencja) base.CreateData();

            row.Data = DateTime.Now;
            row.DataWysylki = DateTime.Now;
            row.Uzytkownik = DBWeb.User.LoginedUser;
            return row;
        }

        public override IEnumerable<DataContextParam> GetParams()
        {
            return new DataContextParam[] { 
                new DataContextParam("kontrahentSelectParam", "Kontrahent", null){ ControlType = typeof(BAL.Forms.Controls.ComboBox) }
            };
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            switch (e.Param.Name)
            {
                case "kontrahentSelectParam":
                    kontrachentSelect = e.Control as BAL.Forms.Controls.ComboBox;
                    kontrachentSelect.Width = 300;
                    kontrachentSelect.DisplayMember = "Kod";
                    //kontrachentSelect.DataContext = new KontrahenciEnovaView();
                    kontrachentSelect.DataContext = new Enova.Forms.CRM.KontrahenciView();
                    break;
            }
            base.OnInitParam(e);
        }

        protected override void OnParamValueChanged(DataContextParamEventArgs e)
        {
            this.table = null;
            this.Reload();
            /*
            this.initTable();
            this.Reset();
             */
            base.OnParamValueChanged(e);
        }

        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.KorespondencjaView_grid;
        }

        public override void Remove(object obj)
        {
            if (obj is Enova.Business.Old.Core.IDeleteRecord)
                ((Enova.Business.Old.Core.IDeleteRecord)obj).DeleteRecord();
            this.table = null;
            Reload();
        }

        public override void Reload()
        {
            lock (reloadLock)
            {
                if (this.table != null)
                {
                    this.table.Reload();
                    if (SupportsSorting && IsSorted)
                        this.table.Sort(GetSortComparer());
                }
                base.Reload();
            }
        }

        public override void EndEdit()
        {
            base.EndEdit();
            this.table = null;
            this.Reload();
        }

        #endregion
    }
}
