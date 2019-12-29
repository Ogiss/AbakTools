using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AbakTools.Handel.Forms
{
    public class PozycjeZwrotuGrid : Enova.Business.Old.Controls.DataGidViewSelect
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

    }
}
