using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AbakTools.Handel.Forms
{
    public class PozycjeZamowieniaGrid : Enova.Business.Old.Controls.DataGidViewSelect
    {

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.Alt == false && e.Control == false && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        this.EndEdit();
                        return true;
                    case Keys.Down:
                        if (this.CurrentRow != null && this.CurrentCell != null && this.CurrentRow.Index == this.Rows.Count - 1 && this.CurrentCell.IsInEditMode)
                        {
                            this.EndEdit();
                            this.OnKeyDown(e);
                            return true;

                        }
                       break;
                }
                
            }
            
            
            return base.ProcessDataGridViewKey(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Enter)
            {
                DataGridViewCell currentCell = this.CurrentCell;
                this.EndEdit();
                this.CurrentCell = null;
                this.CurrentCell = currentCell;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);

            int RowNumWidth = this.RowCount.ToString().Length;
            StringBuilder RowNumber = new StringBuilder(RowNumWidth);
            RowNumber.Append(e.RowIndex + 1);
            while (RowNumber.Length < RowNumWidth)
                RowNumber.Insert(0, " ");

            SizeF Sz = e.Graphics.MeasureString(RowNumber.ToString(), this.Font);

            if (this.RowHeadersWidth < (int)(Sz.Width + 20))
                this.RowHeadersWidth = (int)(Sz.Width + 20);

            e.Graphics.DrawString(
                RowNumber.ToString(),
                this.Font,
                SystemBrushes.ControlText,
                e.RowBounds.Location.X + 15,
                e.RowBounds.Location.Y + ((e.RowBounds.Height - Sz.Height) / 2));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

    }
}
