using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BAL.Forms.Controls
{
    public class GridViewCheckBoxColumn : System.Windows.Forms.DataGridViewCheckBoxColumn
    {
        #region Forms

        private CheckBox headerCheckBox;
        private bool fireCheckBoxChanged;

        #endregion

        #region Methods

        public GridViewCheckBoxColumn()
        {
        }

        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();
            this.addCheckBoxToHeader();
        }

        public override int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
        {
            return base.GetPreferredWidth(autoSizeColumnMode, fixedHeight);
        }

        private void addCheckBoxToHeader()
        {
            if (this.DataGridView != null && this.headerCheckBox == null && !this.ReadOnly)
            {
                headerCheckBox = new CheckBox();
                headerCheckBox.Size = new System.Drawing.Size(15, 15);
                headerCheckBox.ThreeState = true;
                headerCheckBox.CheckStateChanged+=new EventHandler(headerCheckBox_CheckStateChanged);
                this.DataGridView.Controls.Add(this.headerCheckBox);
                this.DataGridView.CellPainting +=new DataGridViewCellPaintingEventHandler(DataGridView_CellPainting);
                this.DataGridView.SelectionChanged +=new EventHandler(DataGridView_SelectionChanged);
            }
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == this.Index)
            {
                ResetLocation(e.ColumnIndex, e.RowIndex);
            }
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            fireCheckBoxChanged = false;
            CheckState checkState = CheckState.Indeterminate;
            foreach (DataGridViewRow row in this.DataGridView.SelectedRows)
            {
                var val = (bool)row.Cells[this.Index].Value ? CheckState.Checked : CheckState.Unchecked;

                if (checkState == CheckState.Indeterminate)
                    checkState = val;
                else if(checkState != val)
                {
                    checkState = CheckState.Indeterminate;
                    break;
                }
            }
            headerCheckBox.CheckState = checkState;
            fireCheckBoxChanged = true;
        }

        private void headerCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (fireCheckBoxChanged && this.headerCheckBox.CheckState != CheckState.Indeterminate)
            {
                foreach (DataGridViewRow row in this.DataGridView.SelectedRows)
                {
                    row.Cells[this.Index].Value = this.headerCheckBox.CheckState == CheckState.Checked ? true : false;
                }
            }
        }

        private void ResetLocation(int ColumnIndex, int RowIndex)
        {
            Rectangle oRectangle =
              this.DataGridView.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();


            oPoint.X = oRectangle.Location.X + oRectangle.Width - headerCheckBox.Width - 3;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - headerCheckBox.Height) / 2 + 1;

            headerCheckBox.Location = oPoint;
        }

        #endregion
    }
}
