using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms.Controls
{
    public partial class ComboBox : BAL.Forms.Controls.BALControl
    {
        #region Fields

        #endregion

        #region Properties

        [Browsable(true)]
        public string DisplayMember
        {
            get { return this.comboBoxControl.DisplayMember; }
            set { this.comboBoxControl.DisplayMember = value; }
        }

        [Browsable(true)]
        public string ValueMember
        {
            get { return this.comboBoxControl.ValueMember; }
            set { this.comboBoxControl.ValueMember = value; }
        }

        [Browsable(false)]
        public override object Value
        {
            get
            {
                if (this.comboBoxControl.SelectedIndex == 0)
                    return null;
                return this.comboBoxControl.SelectedItem;
            }
        }

        public int SelectedIndex
        {
            get { return this.comboBoxControl.SelectedIndex; }
            set { this.comboBoxControl.SelectedIndex = value; }
        }

        #endregion

        #region Methods

        public ComboBox()
        {
            InitializeComponent();
        }

        protected override void OnBeforeBinding(EventArgs e)
        {
            base.OnBeforeBinding(e);
        }


        protected override void OnAfterBinding(EventArgs e)
        {
            base.OnAfterBinding(e);
            if (this.DataContext != null)
            {
                if (string.IsNullOrEmpty(this.DisplayMember))
                {
                    var attrs = this.DataContext.GetDataType().GetCustomAttributes(typeof(DefaultPropertyAttribute),true);
                    if (attrs != null && attrs.Length > 0)
                        this.DisplayMember = ((DefaultPropertyAttribute)attrs[0]).Name;
                }

                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    if (typeof(BAL.Business.IRow).IsAssignableFrom(this.DataContext.GetDataType()))
                    {
                        this.ValueMember = "ID";
                    }
                }

                this.comboBoxControl.Items.Add("- - - - - - - - - - -");

                if (this.DataContext is IEnumerable)
                {
                    foreach (var row in ((IEnumerable)this.DataContext))
                        this.comboBoxControl.Items.Add(row);
                }

                this.comboBoxControl.SelectedIndex = 0;
            }
        }

        private void comboBoxControl_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        #endregion

    }
}
