using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public class DataGridViewSelectControl : TextBox , IDataGridViewEditingControl
    {
        #region Fields

        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        object value = null;

        #endregion

        #region Properties

        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        #endregion

        #region IDataGridViewEditingControl Implementation

        public object EditingControlFormattedValue
        {
            get
            {

                return this.Text;
            }
            set
            {
                
                if (value is string)
                {
                    this.Text = (string)value;
                }
                else
                {
                    this.Text = value.ToString();
                }
                
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle style)
        {
            this.Font = style.Font;
            this.ForeColor = style.ForeColor;
            this.BackColor = style.BackColor;
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public bool EditingControlWantsInputKey(
    Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch(keyData & Keys.KeyCode){
                case Keys.Enter:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                return true;
            }
            return !dataGridViewWantsInputKey;
            
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }

      

        #endregion

    }
}
