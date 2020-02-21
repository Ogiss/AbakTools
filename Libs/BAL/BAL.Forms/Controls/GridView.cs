using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Types;

namespace BAL.Forms.Controls
{
    [Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
    public class GridView : DataGridView
    {
        #region Properties
        #endregion

        #region Methods

        public GridView()
        {
            this.AutoGenerateColumns = false;
            this.VirtualMode = true;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = this.Rows[e.RowIndex];
                    DataGridViewColumn column = this.Columns[e.ColumnIndex];
                    PropertyDescriptorPath path = new PropertyDescriptorPath(row.DataBoundItem.GetType(), column.DataPropertyName);
                    e.Value = path.GetValue(row.DataBoundItem);
                    /*
                    PropertyPath path = new PropertyPath(row.DataBoundItem.GetType(), column.DataPropertyName);
                    if (path != null)
                        e.Value = path.GetValue(row.DataBoundItem);
                     */
                }
                catch
                {
                    e.Value = null;
                }
            }
            base.OnCellValueNeeded(e);
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            //base.OnDataError(displayErrorDialogIfNoHandler, e);
            //e.Cancel = true;
            e.ThrowException = false;
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (!e.Alt && !e.Control && !e.Shift && e.KeyCode == Keys.Enter)
            {
                return true;
            }
            return base.ProcessDataGridViewKey(e);
        }

        #endregion
    }
}
