using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//[assembly: BAL.Forms.MenuAction("Formularze\\Formularz zwrotów", typeof(AbakTools.Analizy.Forms.FormularzZwrotow_I_Form), Priority = 130)]

namespace AbakTools.Analizy.Forms
{
    public partial class FormularzZwrotow_I_Form : AbakTools.Analizy.Forms.FormularzWgGrupForm
    {
        public FormularzZwrotow_I_Form()
        {
            InitializeComponent();
            this.FormularzZwrotów = true;
        }
    }
}
