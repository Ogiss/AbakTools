using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public partial class GridConfigForm : Form
    {
        public BAL.Business.View View;
        private Control parent;

        public GridConfigForm(Control parent)
        {
            InitializeComponent();
            this.parent = parent;
            if (this.parent != null)
            {
                this.parent.Enter += new EventHandler(Parent_Enter);
                this.parent.Leave += new EventHandler(Parent_Leave);
            }
        }


        private void Parent_Enter(object sender, EventArgs e)
        {
            if (!this.Visible)
                this.Show();
        }

        private void Parent_Leave(object sender, EventArgs e)
        {
            if (this.Visible)
                this.Hide();
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.View != null)
            {
                using (new WaitCursor())
                    this.View.Save();
            }
        }

        private void GridConfigForm_Load(object sender, EventArgs e)
        {
            this.loadColumns();
        }

        private void loadColumns()
        {
            var ds = this.View.VisibleColumns.GetVisible().OrderBy(c=>c.Order).ToList();
            this.columsBindingSource.DataSource = ds;
        }

        private void GridConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.parent != null)
            {
                this.parent.Enter -= new EventHandler(Parent_Enter);
                this.parent.Leave -= new EventHandler(Parent_Leave);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (FormManager.Confirm("Czy napewno chcesz usunąć kolumnę ?", true))
            {
                var row = this.columnsDataGridView.CurrentRow;
                var column = (BAL.Types.Column)row.DataBoundItem;
                column.Visible = false;
                this.columnsDataGridView.Rows.Remove(row);
            }
        }

        private void bindingNavigatorUpItem_Click(object sender, EventArgs e)
        {
            if (this.columnsDataGridView.SelectedRows.Count > 0)
            {
                var row = this.columnsDataGridView.SelectedRows[0];
                if (row.Index > 0 && row.Index < this.columnsDataGridView.Rows.Count)
                {
                    int idx = row.Index;
                    var c1 = (Types.Column)row.DataBoundItem;
                    var c2 = (Types.Column)this.columnsDataGridView.Rows[idx - 1].DataBoundItem;
                    c1.BeginInit();
                    c1.Order--;
                    c1.EndInit();
                    c2.Order++;
                    this.loadColumns();
                    this.columsBindingSource.MovePrevious();
                }
            }

        }

        private void bindingNavigatorDownItem_Click(object sender, EventArgs e)
        {
            if (this.columnsDataGridView.SelectedRows.Count > 0)
            {
                var row = this.columnsDataGridView.SelectedRows[0];
                if (row.Index >= 0 && row.Index < this.columnsDataGridView.Rows.Count - 1)
                {
                    int idx = row.Index;
                    var c1 = (Types.Column)row.DataBoundItem;
                    var c2 = (Types.Column)this.columnsDataGridView.Rows[idx + 1].DataBoundItem;
                    c1.BeginInit();
                    c1.Order++;
                    c1.EndInit();
                    c2.Order--;
                    this.loadColumns();
                    this.columsBindingSource.MoveNext();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Types.Column)this.columsBindingSource.Current).TextAlign = (Types.TextAlign)this.comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Types.Column)this.columsBindingSource.Current).HeaderTextAlign = (Types.TextAlign)this.comboBox2.SelectedIndex;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            var form = new ColumnSelectForm(this.View.GetDataType());
            form.TopMost = true;
            form.ShowDialog();
            if (form.SelectedColumn != null)
            {
                var col = form.SelectedColumn.Clone();
                int order = this.View.VisibleColumns.Count() == 0 ? 0 : this.View.VisibleColumns.Max(c => c.Order);
                col.Order = order + 1;
                col.Visible = true;
                this.View.VisibleColumns.Add(col);
                this.loadColumns();
                this.columsBindingSource.MoveLast();
            }
        }


        #region Nested Types

        public class ColumnSortComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                var c1 = (Types.Column)((DataGridViewRow)x).DataBoundItem;
                var c2 = (Types.Column)((DataGridViewRow)y).DataBoundItem;

                return c1.Order.CompareTo(c2.Order);
            }
        }

        #endregion

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift && e.KeyCode == Keys.Enter)
            {
                Control next = (Control)sender;
                do
                {
                    next = this.GetNextControl(next, true);
                    if (next == null)
                        next = this.Controls[0];
                    if (!typeof(Label).IsAssignableFrom(next.GetType()))
                        break;
                } while (next != (Control)sender);
                next.Select();
                //e.SuppressKeyPress = false;
                e.Handled = true;
            }
        }



    }
}
