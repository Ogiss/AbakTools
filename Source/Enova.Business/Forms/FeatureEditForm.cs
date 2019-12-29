using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Forms
{
    public partial class FeatureEditForm : Form
    {
        public bool IsSubGroup = false;

        public FeatureEditForm()
        {
            InitializeComponent();
        }

        public FeatureEditForm(bool isSubGroup)
            : this()
        {
            IsSubGroup = isSubGroup;
        }

        private void FeatureEditForm_Load(object sender, EventArgs e)
        {
            if (IsSubGroup)
            {
                label.Text = "Nazwa podgrupy:";
                this.Text = "Nowa podgrupa";
            }
            else
            {
                label.Text = "Nazwa grupy:";
                this.Text = "Nowa grupa";
            }
        }

        public string Value
        {
            get { return this.textBox.Text; }
        }
    }
}
