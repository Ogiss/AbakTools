using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AbakTools.Handel.Forms.Zwroty
{
    public class TowaryGrid : System.Windows.Forms.DataGridView
    {

        public TowaryGrid()
        {
        }

        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            /*
            if (!e.Alt && !e.Control && !e.Shift && e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.OnKeyDown(e);
                return true;
            }
             */
            return base.ProcessDataGridViewKey(e);
        }
    }
}
