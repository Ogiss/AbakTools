using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;
using AbakTools.CRM;

[assembly: AbakTools.Business.DefaultView(typeof(Enova.Business.Old.DB.Web.Kontrahent), typeof(AbakTools.CRM.Forms.KontrahenciView))]

namespace AbakTools.CRM.Forms
{
    public class KontrahenciView : AbakTools.Forms.GridViewBaseWithDbContext<Enova.Business.Old.DB.Web.Kontrahent>
    {
        #region Fields

        private System.Windows.Forms.ComboBox przedstawicieleComboBox;
        private bool firePrzedstawicielChange;

        #endregion

        #region Methods

        #endregion

        #region Methods

        public KontrahenciView() { }

        public KontrahenciView(Enova.Business.Old.DB.Web.WebContext dbContext) : base(dbContext) { }

        protected override Enova.Business.Old.Core.TableBase<Enova.Business.Old.DB.Web.Kontrahent> CreateTable()
        {
            string kod = null;
            if (przedstawicieleComboBox != null && przedstawicieleComboBox.SelectedIndex > 0)
            {
                var pr = (Enova.Business.Old.DB.Web.Kontrahent)przedstawicieleComboBox.SelectedItem;
                kod = pr.Kod;
            }
            return new Enova.Business.Old.Web.Kontrahenci(this.DbContext) { PrzedstawicielKod = kod };
        }


        public override string GetDefaultXmlDefinition()
        {
            if (this.SelectionMode)
                return Properties.Resources.KontrahenciSelect_grid;
            return Properties.Resources.Kontrahenci_grid;
        }

        public override IEnumerable<BAL.Business.DataContextParam> GetParams()
        {
            return new DataContextParam[]{
                new DataContextParam("przedstawicielParam","Przedstawiciel:", PropertyPath.Create<Enova.Business.Old.DB.Web.Kontrahent>("Przedstawiciel")){ ControlType = typeof(System.Windows.Forms.ComboBox)}
            };
        }

        protected override void OnInitParam(DataContextParamEventArgs e)
        {
            base.OnInitParam(e);
            switch (e.Param.Name)
            {
                case "przedstawicielParam":

                    this.przedstawicieleComboBox = e.Control as System.Windows.Forms.ComboBox;
                    this.przedstawicieleComboBox.DisplayMember = "Kod";
                    this.przedstawicieleComboBox.ValueMember = "ID";
                    this.przedstawicieleComboBox.Items.Add("Wszyscy");
                    this.przedstawicieleComboBox.SelectedIndex = 0;

                    //foreach (var pr in CRMModule.GetInstance(this).Kontrahenci.WgCzyAgent[true].ToList().OrderBy(r=>r.Kod))
                    foreach (var pr in DbContext.Kontrahenci.Where(r=>r.CzyAgent==true).OrderBy(r=>r.Kod).ToList())
                    {
                        this.przedstawicieleComboBox.Items.Add(pr);
                    }

                    this.przedstawicieleComboBox.SelectionChangeCommitted +=new EventHandler(przedstawicieleComboBox_SelectionChangeCommitted);
                    firePrzedstawicielChange = true;

                    break;
            }
        }

        private void przedstawicieleComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (firePrzedstawicielChange)
                this.Reload();
        }

        public override void Reload()
        {
            if (this.Table != null)
            {
                string kod = null;
                if (przedstawicieleComboBox != null && przedstawicieleComboBox.SelectedIndex > 0)
                {
                    var pr = (Enova.Business.Old.DB.Web.Kontrahent)przedstawicieleComboBox.SelectedItem;
                    kod = pr.Kod;
                }
                ((Enova.Business.Old.Web.Kontrahenci)this.Table).PrzedstawicielKod = kod;
            }
            base.Reload();
        }

        #endregion
    }
}
