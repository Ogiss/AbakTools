using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls
{
    public class DataGridViewSelectCell<TDataObject> : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewSelectControl ctl = this.DataGridView.EditingControl as DataGridViewSelectControl;
            if (this.Value == null)
            {
                ctl.Value = this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = this.Value;
            }

        }

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewSelectControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(TDataObject);
            }
        }
        
    }
}
