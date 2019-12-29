using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Forms;

[assembly: DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.Magazyn_StanMagazynowy), typeof(AbakTools.Magazyn.Forms.StanMagazynowyOgolnePanel))]

namespace AbakTools.Magazyn.Forms
{
    public partial class StanMagazynowyOgolnePanel : BAL.Forms.DataPanel
    {
        #region Fields

        #endregion

        #region Properties

        new public StanMagazynowyContext DataContext
        {
            get { return (StanMagazynowyContext)base.DataContext; }
        }

        #endregion

        #region Methods

        public StanMagazynowyOgolnePanel()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ActiveControl = this.stanMagTextBox;
            this.stanMagTextBox.Focus();
        }

        private void typOprComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.DataContext.TypOpr = typOprComboBox.SelectedIndex;
            if (this.DataContext.TypOpr > 0)
                this.stanMagTextBox.Text = "0";
            else
                this.stanMagTextBox.Text = this.DataContext.StanMag.ToString();

        }

        protected override void OnBeforeBinding(EventArgs e)
        {
            ((StanMagazynowyContext)((BAL.Types.DataContextEventArgs)e).DataContext).Refresh();

            base.OnBeforeBinding(e);
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);
            if (this.DataContext.ID == 0)
            {
                this.typOprComboBox.SelectedIndex = 0;
                this.typOprComboBox.Enabled = false;
            }
            else
            {
                this.typOprComboBox.SelectedIndex = 1;
                this.typOprComboBox.Enabled = true;
            }
        }

        private bool ended = false;
        public override void EndEdit()
        {
            if (!this.ended)
            {
                base.EndEdit();
                double d = 0;
                if (double.TryParse(stanMagTextBox.Text, out d))
                {
                    if (DataContext.StanMag != d)
                        DataContext.StanMag = d;
                    if (typOprComboBox.SelectedIndex > 0)
                        DataContext.TypOpr = typOprComboBox.SelectedIndex;
                }
                this.ended = true;
            }
        }

        #endregion


    }
}
