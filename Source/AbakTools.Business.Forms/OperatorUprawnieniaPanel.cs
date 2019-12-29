using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BAL.Types;
using BAL.Forms;

//[assembly: DataPanel("Uprawnienia", typeof(AbakTools.Business.Operator), typeof(AbakTools.Business.Forms.OperatorUprawnieniaPanel))]
[assembly: DataPanel("Uprawnienia", typeof(Enova.Business.Old.DB.Web.Operator), typeof(AbakTools.Business.Forms.OperatorUprawnieniaPanel))]

namespace AbakTools.Business.Forms
{
    [Priority(20)]
    public partial class OperatorUprawnieniaPanel : BAL.Forms.DataPanel
    {
        public Enova.Business.Old.DB.Web.Operator Operator
        {
            get { return (Enova.Business.Old.DB.Web.Operator)base.DataContext.GetData(); }
        }

        public OperatorUprawnieniaPanel()
        {
            InitializeComponent();
        }

        protected override void OnBindingComplete(EventArgs e)
        {
            base.OnBindingComplete(e);

            if ((this.Operator.PrawaDostepu & OperatorPrawaDostepu.Przedstawiciel) == OperatorPrawaDostepu.Przedstawiciel)
                przedstawicielCheckBox.Checked = true;

            if ((this.Operator.PrawaDostepu & OperatorPrawaDostepu.Magazynier) == OperatorPrawaDostepu.Magazynier)
                magazynierCheckBox.Checked = true;

            if ((this.Operator.PrawaDostepu & OperatorPrawaDostepu.Pakowacz) == OperatorPrawaDostepu.Pakowacz)
                pakowaczCheckBox.Checked = true;

            if ((this.Operator.PrawaDostepu & OperatorPrawaDostepu.Kierownik) == OperatorPrawaDostepu.Kierownik)
                kierownikCheckBox.Checked = true;

            if ((this.Operator.PrawaDostepu & OperatorPrawaDostepu.Administrator) == OperatorPrawaDostepu.Administrator)
                adminCheckBox.Checked = true;

            if ((this.Operator.PrawaDostepu & OperatorPrawaDostepu.SuperAdmin) == OperatorPrawaDostepu.SuperAdmin)
                superAdminCheckBox.Checked = true;
        }

        public override bool IsValid(out string msg)
        {
            msg = null;
            OperatorPrawaDostepu access = OperatorPrawaDostepu.Brak;

            if (przedstawicielCheckBox.Checked) access |= OperatorPrawaDostepu.Przedstawiciel;
            if (magazynierCheckBox.Checked) access |= OperatorPrawaDostepu.Magazynier;
            if (pakowaczCheckBox.Checked) access |= OperatorPrawaDostepu.Pakowacz;
            if (kierownikCheckBox.Checked) access |= OperatorPrawaDostepu.Kierownik;
            if (adminCheckBox.Checked) access |= OperatorPrawaDostepu.Administrator;
            if (superAdminCheckBox.Checked) access |= OperatorPrawaDostepu.SuperAdmin;

            if (access == OperatorPrawaDostepu.Brak)
            {
                if (!FormManager.Confirm("Operator nie posiada skonfigurowanych uprawnień\r\nCzy napewno chcesz zamknąć formularz?"))
                    return false;
            }

            this.Operator.SetPrawaDostepu(access);

            return base.IsValid(out msg);
        }
    }
}
