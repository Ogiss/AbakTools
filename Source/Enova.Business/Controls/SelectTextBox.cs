using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Data.Entity;

using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class SelectTextBox : UserControl
    {
        #region Fields

        private Type selectFormType;
        private Form selectForm = null;
        private object dataSource = null;
        private string displayMember;
        private string valueMember;
        private object selectedItem = null;
        private bool fireTextChangedEvent = true;
        private object foundItem = null;


        #endregion

        #region Properties

        public virtual Type SelectFormType
        {
            get { return selectFormType; }
            set { selectFormType = value; }
        }

        protected Form SelectForm
        {
            get
            {
                if (selectForm == null && this.SelectFormType!=null)
                {
                    selectForm = (Form)Activator.CreateInstance(this.SelectFormType);
                    ((ISelectForm)selectForm).SelectMode = true;
                }
                return selectForm;
            }
        }

        public virtual object DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
            }
        }

        public virtual string DisplayMember
        {
            get
            {
                return displayMember;
            }
            set
            {
                displayMember = value;
            }
        }

        public virtual string ValueMember
        {
            get
            {
                return valueMember;
            }
            set
            {
                valueMember = value;
            }
        }

        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                var e = new SelectionChangingEventArgs(value);
                OnSelectionChanging(e);
                if (!e.Cancel)
                {
                    fireTextChangedEvent = false;
                    this.selectedItem = value;
                    if (value == null)
                    {
                        textBox.Text = "";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(displayMember))
                        {
                            PropertyDescriptor displayProperty = TypeDescriptor.GetProperties(value)[displayMember];
                            if (displayProperty != null)
                            {
                                this.textBox.Text = displayProperty.GetValue(value).ToString();
                                this.textBox.Focus();
                                this.textBox.SelectAll();
                            }
                        }
                    }
                    fireTextChangedEvent = true;
                    foundItem = selectedItem;
                    OnSelectionChanged(new EventArgs());
                }
            }
        }

        public string SelectedText
        {
            get { return textBox.Text; }
        }

        #endregion

        #region Constructors

        public SelectTextBox()
        {
            InitializeComponent();
        }

        #endregion


        #region Methods

        private void ShowSelectForm()
        {
            if (this.SelectForm != null)
            {
                DialogResult result = this.SelectForm.ShowDialog();
                if (SelectForm is ISelectForm)
                {
                    this.SelectedItem = ((ISelectForm)this.SelectForm).SelectedItem;
                }
            }

        }

        protected virtual object FindItem(string findString)
        {
            return null;
        }

        protected virtual object FindItems(string findString)
        {
            return null;
        }

        private void setText(object item)
        {
            fireTextChangedEvent = false;
            if (item == null)
            {
                textBox.Text = "";
            }
            else
            {
                if (!string.IsNullOrEmpty(displayMember))
                {
                    PropertyDescriptor displayProperty = TypeDescriptor.GetProperties(item)[displayMember];
                    if (displayProperty != null)
                    {
                        this.textBox.Text = displayProperty.GetValue(item).ToString();
                    }
                }
            }
            fireTextChangedEvent = true;
        }

        #endregion

        #region Events

        [Browsable(true)]
        public event EventHandler<SelectionChangingEventArgs> SelectionChanging;

        [Browsable(true)]
        public event EventHandler SelectionChanged;

        #endregion

        #region Events methods

        protected virtual void OnSelectionChanging(SelectionChangingEventArgs e)
        {
            if (SelectionChanging != null)
                SelectionChanging(this, e);
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        #endregion

        #region Events handlers

        private void button_Click(object sender, EventArgs e)
        {
            ShowSelectForm();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (fireTextChangedEvent && !string.IsNullOrEmpty(displayMember))
            {
                string text = textBox.Text;
                foundItem = FindItem(text);
                if (foundItem != null)
                {
                    selectedItem = foundItem;
                    setText(foundItem);
                    textBox.SelectionStart = text.Length;
                    textBox.SelectionLength = textBox.Text.Length - text.Length;
                    OnSelectionChanged(new EventArgs());
                }
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.Alt == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (foundItem != null)
                    {
                        if (foundItem != selectedItem)
                            SelectedItem = foundItem;
                       this.SelectNextControl(this, true, true, true, true);
                    }
                    else
                    {
                        ShowSelectForm();
                    }
                    e.Handled = true;
                }
                else if(e.KeyCode == Keys.Escape)
                {
                    setText(SelectedItem);
                }
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            foundItem = selectedItem;
        }


        #endregion

        #region Classes

        public class SelectionChangingEventArgs : CancelEventArgs
        {
            private object value;

            public object Value
            {
                get { return this.value; }
            }

            public SelectionChangingEventArgs(object value)
                : base(false)
            {
                this.value = value;
            }
        }

        #endregion
    }
}
