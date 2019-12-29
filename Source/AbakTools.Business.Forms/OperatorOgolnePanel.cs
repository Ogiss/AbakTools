using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;

//[assembly: DataPanel("Ogólne", typeof(AbakTools.Business.Operator), typeof(AbakTools.Business.Forms.OperatorOgolnePanel))]
[assembly: DataPanel("Ogólne", typeof(Enova.Business.Old.DB.Web.Operator), typeof(AbakTools.Business.Forms.OperatorOgolnePanel))]

namespace AbakTools.Business.Forms
{
    [Priority(10)]
    public partial class OperatorOgolnePanel : BAL.Forms.DataPanel
    {

        #region Properties

        public Enova.Business.Old.DB.Web.Operator Operator
        {
            get { return (Enova.Business.Old.DB.Web.Operator)base.DataContext.GetData(); }
        }

        #endregion

        #region Methods

        public OperatorOgolnePanel()
        {
            InitializeComponent();
        }

        public override bool IsValid(out string msg)
        {
            msg = null;
            bool ret = false;

            if (string.IsNullOrEmpty(nazwaTextBox.Text))
                msg = "Wartość pola Nazwa nie może być pusta";
            else if (!bezHaslaCheckBox.Checked && string.IsNullOrEmpty(this.Operator.Haslo) && string.IsNullOrEmpty(hasloTextBox.Text))
                msg = "Wartość pola Hasło nie może być pusta";
            else if (!bezHaslaCheckBox.Checked && !string.Equals(hasloTextBox.Text, powtorzHasloTextBox.Text))
                msg = "Pola hasło i powtórz hasło nie mogą być różne";
            else
                ret = true;

            if (ret)
            {
                if (this.bezHaslaCheckBox.Checked)
                {
                    this.Operator.SetPassword("");
                }
                else if (!string.IsNullOrEmpty(hasloTextBox.Text))
                    this.Operator.SetPassword(hasloTextBox.Text);
            }

            return ret;
        }

        #endregion

        private void bezHaslaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (bezHaslaCheckBox.Checked)
            {
                hasloTextBox.Enabled = false;
                powtorzHasloTextBox.Enabled = false;
            }
            else
            {
                hasloTextBox.Enabled = true;
                powtorzHasloTextBox.Enabled = true;
            }
        }


    }
}
