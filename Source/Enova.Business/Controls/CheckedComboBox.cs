using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public partial class CheckedComboBox : UserControl
    {

        #region Internal declarations
        #endregion

        #region Members

        CheckedComboBoxList listForm = null;
        private object _dataSource = null;
        private string _displayMember = null;
        private bool _allRowItem = false;
        private string _allRowItemText = string.Empty;
        private string _valueMember = null;
        private int _selectedIndex = -1;
        private object _selectedItem;
        private bool _fireSelectedOnCheckChanged = false;

        #endregion

        #region Properties

        [Browsable(true), Category("Data")]
        public object DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
                if (listForm != null)
                    listForm.Refresh();
            }
        }

        [Browsable(true), Category("Behavior")]
        public bool FireSelectedOnCheckChanged
        {
            get { return this._fireSelectedOnCheckChanged; }
            set { this._fireSelectedOnCheckChanged = value; }
        }

        [Browsable(true), Category("Data")]
        public string DisplayMember
        {
            get
            {
                return this._displayMember;
            }
            set
            {
                this._displayMember = value;
            }
        }

        [Browsable(true), Category("Data")]
        public bool AllRowItem
        {
            get { return this._allRowItem; }
            set { this._allRowItem = value; }
        }

        [Browsable(true), Category("Data")]
        public string AllRowItemText
        {
            get { return this._allRowItemText; }
            set { this._allRowItemText = value; }
        }

        [Browsable(true), Category("Data")]
        public string ValueMember
        {
            get
            {
                return this._valueMember;
            }
            set
            {
                this._valueMember = value;
            }
        }

        [Browsable(false)]
        public int SelectedIndex
        {
            get
            {
                return this._selectedIndex;
            }
            set
            {
                if (value == -1)
                {
                    this._selectedIndex = -1;
                    this.SelectedItem = null;
                }
                else
                {

                    if (this.DataSource != null)
                    {
                        if (this.DataSource is IList)
                        {
                            int count = ((IList)this.DataSource).Count;
                            if (this.AllRowItem)
                                count++;
                            if (value < count)
                            {
                                this._selectedIndex = value;
                                if (AllRowItem)
                                {
                                    if (value == 0)
                                    {
                                        this._selectedItem = null;
                                        this.TextBox.Text = string.IsNullOrEmpty(this.AllRowItemText) ? "All rows" : this.AllRowItemText;
                                    }
                                    else
                                    {
                                        this.SelectedItem = ((IList)this.DataSource)[value - 1];
                                    }
                                }
                                else
                                {
                                    this.SelectedItem = ((IList)this.DataSource)[value];
                                }
                            }
                        }
                    }
                }
            }
        }

        [Browsable(false)]
        public object SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                this._selectedItem = value;
                if (value == null)
                {
                    this.TextBox.Text = null;
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.DisplayMember))
                    {
                        PropertyInfo p = value.GetType().GetProperty(this.DisplayMember);
                        if (p != null)
                        {
                            this.TextBox.Text = p.GetValue(value, null).ToString();
                        }
                    }
                }
            }
        }

        [Browsable(false)]
        public ArrayList SelectedValues
        {
            get
            {
                if (this.listForm!=null && !string.IsNullOrEmpty(this._valueMember))
                {
                    ArrayList a = new ArrayList();
                    ArrayList checkedRows = this.listForm.CheckedRows;
                    if (checkedRows.Count > 0)
                    {
                        PropertyInfo pinfo = checkedRows[0].GetType().GetProperty(this._valueMember);
                        if (pinfo != null)
                        {
                            foreach (var row in checkedRows)
                                a.Add(pinfo.GetValue(row, null));
                            
                        }
                    }
                    return a;
                }
                return null;
            }
        }

        #endregion

        #region Events

        [Browsable(true),Category("Behavior")]
        public event EventHandler SelectionChangeCommitted;

        [Browsable(true), Category("Behavior")]
        public event EventHandler CheckChanged;

        #endregion

        #region Constructor

        public CheckedComboBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods
        #endregion

        #region Event handlers

        internal void OnSelectionChangeCommitedFire()
        {
            this.OnSelectionChangeCommitted(new EventArgs());
        }

        protected virtual void OnSelectionChangeCommitted(EventArgs e)
        {
            if (SelectionChangeCommitted != null)
                SelectionChangeCommitted(this, e);
        }

        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (CheckChanged != null)
                CheckChanged(this, e);
        }

        #endregion

        #region Event methods

        private void Button_Click(object sender, EventArgs e)
        {
            if (this.listForm == null)
            {
                this.listForm = new CheckedComboBoxList() { Parent = this };
                this.listForm.Size = new Size(this.Width, listForm.Size.Height);
                this.listForm.CheckChanged += new EventHandler(listForm_CheckChanged);
            }

            if (listForm.Visible)
            {
                this.listForm.Hide();
            }
            else
            {
                Point p = this.PointToScreen(new Point(0, this.Size.Height));
                this.listForm.Location = p;
                this.listForm.Show();
            }


        }

        private void listForm_CheckChanged(object sender, EventArgs e)
        {
            this.OnCheckChanged(e);
            if (this._fireSelectedOnCheckChanged)
                this.OnSelectionChangeCommitted(e);
        }

        private void CheckedComboBox_Load(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
