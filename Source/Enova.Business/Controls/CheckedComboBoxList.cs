using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Enova.Business.Old.Controls
{
    public partial class CheckedComboBoxList : Form
    {

        #region Internal declarations

        internal class CheckedComboBoxListRow
        {
            internal CheckedComboBoxListRow(bool isChecked, string displayMember, object dataBoundItem)
            {
                this.Checked = isChecked;
                this.DisplayMember = displayMember;
                this._dataBoundItem = dataBoundItem;
            }

            public bool Checked { get; set; }
            public string DisplayMember { get; set; }
            private object _dataBoundItem = null;
            public object DataBoundItem { get { return this._dataBoundItem; } }

        }

        #endregion

        #region Members

        internal CheckedComboBox Parent;
        internal bool DataLoaded = false;
        private DataGridViewCheckBoxColumn checkColumn;
        private DataGridViewColumn displayColumn;
        private bool isLoaded = false;
        private bool checkCellClick = false;
        private bool isCheckSelection = false;

        #endregion

        #region Properties

        internal bool IsCheckSelection
        {
            get { return this.isCheckSelection; }
            set { this.isCheckSelection = value; }
        }

        internal ArrayList CheckedRows
        {
            get
            {
                ArrayList a = new ArrayList();
                foreach (DataGridViewRow row in DataGrid.Rows)
                {
                    if (row.Index == 0 && this.Parent.AllRowItem)
                        continue;
                    CheckedComboBoxListRow crow = (CheckedComboBoxListRow)row.DataBoundItem;
                    if (crow.Checked)
                        a.Add(crow.DataBoundItem);
                }
                return a;
            }
        }

        #endregion

        #region Events

        internal event EventHandler CheckChanged;

        #endregion

        #region Constructor

        public CheckedComboBoxList()
        {
            InitializeComponent();

            this.DataGrid.AutoGenerateColumns = false;
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        #endregion

        #region Methods

        private void loadData()
        {

            if (!this.DesignMode && !this.DataLoaded && Parent.DataSource != null && !string.IsNullOrEmpty(Parent.DisplayMember))
            {
                DataGrid.DataSource = null;
                List<CheckedComboBoxListRow> ds = new List<CheckedComboBoxListRow>();
                if (Parent.AllRowItem)
                {
                    ds.Add(new CheckedComboBoxListRow(true, string.IsNullOrEmpty(Parent.AllRowItemText) ? "All rows" : Parent.AllRowItemText, null));
                }

                if (Parent.DataSource is IEnumerable)
                {


                    foreach (var row in (IEnumerable)Parent.DataSource)
                    {
                        PropertyInfo pinfo = row.GetType().GetProperty(Parent.DisplayMember);
                        string display = "";
                        if (pinfo != null)
                        {
                            display = pinfo.GetValue(row, null).ToString();
                        }

                        CheckedComboBoxListRow listRow = new CheckedComboBoxListRow(true, display, row);

                        ds.Add(listRow);
                    }

                }
                DataGrid.DataSource = ds;
            }
        }

        internal void Refresh()
        {
            this.loadData();
        }

        internal void CheckAll()
        {
            foreach (DataGridViewRow row in DataGrid.Rows)
            {
                ((CheckedComboBoxListRow)row.DataBoundItem).Checked = true;
            }

            if (this.Visible)
                this.DataGrid.Refresh();
        }

        internal void UncheckAll()
        {
            foreach (DataGridViewRow row in DataGrid.Rows)
            {
                ((CheckedComboBoxListRow)row.DataBoundItem).Checked = false;
            }

            if (this.Visible)
                this.DataGrid.Refresh();
        }
        
        #endregion

        #region Event handlers

        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (this.CheckChanged != null)
                this.CheckChanged(this, e);
        }

        #endregion

        #region Event methods

        private void CheckedComboBoxList_Load(object sender, EventArgs e)
        {
            this.Location = Parent.PointToScreen(new Point(0, Parent.Size.Height));

            checkColumn = new DataGridViewCheckBoxColumn()
            {
                Name = "CheckColumn",
                DataPropertyName = "Checked",
                Visible = true,
                Width = 30
            };

            this.DataGrid.Columns.Add(checkColumn);

            displayColumn = new DataGridViewTextBoxColumn()
            {
                Name = "DisplayColumn",
                DataPropertyName = "DisplayMember",
                Visible = true,
                Width = this.Size.Width - checkColumn.Width - 20,
                ReadOnly = true
            };

            this.DataGrid.Columns.Add(displayColumn);

            this.loadData();

            isLoaded = true;

        }

        private void CheckedComboBoxList_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void DataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                if (checkCellClick)
                {
                    checkCellClick = false;
                }
                else
                {
                    if (Parent.AllRowItem && DataGrid.CurrentRow.Index == 0)
                    {
                        this.CheckAll();
                    }
                    else
                    {
                        this.UncheckAll();
                        ((CheckedComboBoxListRow)DataGrid.CurrentRow.DataBoundItem).Checked = true;
                    }
                    isCheckSelection = false;
                    Parent.SelectedIndex = DataGrid.CurrentRow.Index;
                    this.Hide();
                    Parent.OnSelectionChangeCommitedFire();
                }
            }
        }

        private void DataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                isCheckSelection = true;
                DataGrid.EndEdit();

                if (Parent.AllRowItem && e.RowIndex == 0)
                {
                    if (((CheckedComboBoxListRow)DataGrid.Rows[0].DataBoundItem).Checked)
                    {
                        CheckAll();
                        this.isCheckSelection = false;
                        Parent.SelectedIndex = 0;
                        Parent.OnSelectionChangeCommitedFire();
                    }
                    else
                    {
                        UncheckAll();
                        isCheckSelection = true;
                    }
                }
                checkCellClick = false;
                OnCheckChanged(new EventArgs());
            }
            else
            {
                if (Parent.SelectedIndex != e.RowIndex)
                    this.DataGrid_SelectionChanged(null, null);
            }
        }

        private void DataGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
                checkCellClick = true;
            else
                checkCellClick = false;
        }

        #endregion

    }
}
