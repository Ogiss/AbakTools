using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace BAL.Forms.Controls
{
    [DefaultBindingProperty("SelectedItem")]
    public partial class SelectBox : BAL.Forms.Controls.BALControl
    {
        #region Fields

        private object selectedItem;
        private string displayMember;
        private string valueMember;

        #endregion


        #region Properties

        [Browsable(true), Category("Data")]
        public virtual string DisplayMember
        {
            get { return this.displayMember; }
            set { this.displayMember = value; }
        }

        [Browsable(true), Category("Data")]
        public virtual string ValueMember
        {
            get { return this.valueMember; }
            set { this.valueMember = value; }
        }

        [Bindable(true)]
        public virtual object SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                    this.setSelectedItem(value);
            }
        }

        [Browsable(false)]
        public override object Value
        {
            get
            {
                return this.SelectedItem;
            }
        }

        #endregion

        #region Methods

        public SelectBox()
        {
            InitializeComponent();
        }

        protected override void OnAfterBinding(EventArgs e)
        {
            base.OnAfterBinding(e);
            if (this.DataContext != null && typeof(BAL.Business.View).IsAssignableFrom(this.DataContext.GetType()))
            {
                ((BAL.Business.View)this.DataContext).SelectionMode = true;
                if (string.IsNullOrEmpty(this.displayMember))
                {
                    /*
                    var attr = (System.ComponentModel.DefaultPropertyAttribute)this.DataContext.GetDataType()
                        .GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true).FirstOrDefault();
                    if (attr != null)
                    {
                        this.displayMember = attr.Name;
                    }
                     */
                }

                if (string.IsNullOrEmpty(this.valueMember))
                {
                    if (typeof(BAL.Business.IRow).IsAssignableFrom(this.DataContext.GetDataType()))
                        this.valueMember = "ID";
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (this.DataContext == null)
            {
                this.DataContext = this.CreateDataContext();
            }

            if (this.DataContext != null && typeof(BAL.Business.View).IsAssignableFrom(this.DataContext.GetType()))
            {
                if (FormManager.Instance.ShowGridFormDialog((BAL.Business.View)this.DataContext) == DialogResult.OK)
                {
                    var arg = new CancelWithDataEventArgs(this.DataContext.Current);
                    OnValueChanging(arg);
                    if (!arg.Cancel)
                    {
                        this.setSelectedItem(this.DataContext.Current);
                        this.OnValueChanged(new EventArgs());
                        return;
                    }
                }
                this.setSelectedItem(null);
            }

        }

        protected virtual Business.DataContext CreateDataContext()
        {
            if (this.DataContext == null)
            {
                var binding = this.DataBindings["SelectedItem"];
                if (binding != null)
                {
                    var bm = binding.BindingManagerBase;
                    var property = bm.GetItemProperties()[binding.BindingMemberInfo.BindingMember];
                    if (property != null)
                    {
                        var session = this.GetSession();
                        if (session != null)
                        {
                            var table = session.Tables[property.PropertyType];
                            if (table != null)
                                return table.CreateView();
                        }
                    }
                }
            }
            return null;
        }

        private void setSelectedItem(object value)
        {
            this.selectedItem = value;
            if (value != null)
            {
                if (!string.IsNullOrEmpty(this.displayMember))
                {
                    this.textBox.Text = value.GetType().GetProperty(this.displayMember).GetValue(value, null).ToString();
                }
                else
                    this.textBox.Text = value.ToString();
            }
            else if (!string.IsNullOrEmpty(this.textBox.Text))
                this.textBox.Text = null;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox.Text))
            {
                if (this.selectedItem != null)
                {
                    this.selectedItem = null;
                    this.OnValueChanged(new EventArgs());
                }
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = false;
                this.button.Select();
            }
        }

        protected virtual void OnBeforeSelect(CancelWithDataEventArgs e)
        {
            if (this.BeforeSelect != null)
                this.BeforeSelect(this, e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            this.textBox.ReadOnly = this.ReadOnly;
            this.button.Enabled = !this.ReadOnly;
            base.OnReadOnlyChanged(e);
        }

        #endregion

        #region Events

        public event EventHandler<CancelWithDataEventArgs> BeforeSelect;

        #endregion

        #region Nested Types
        #endregion
    }
}
