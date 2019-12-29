using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public class DataGidViewSelect : DataGrid
    {

        protected override void OnCellValidating(System.Windows.Forms.DataGridViewCellValidatingEventArgs e)
        {
            var col = this.Columns[e.ColumnIndex];
            if (col is ISelectColumn && !((ISelectColumn)col).FormatedValueIsValid(e.FormattedValue))
            {
                e.Cancel = true;
                if (((ISelectColumn)col).SelectFormType != null)
                {
                    var form = Activator.CreateInstance(((ISelectColumn)col).SelectFormType);
                    if (form != null)
                    {
                        DialogResult result = ((Form)form).ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            if (form is ISelectForm)
                            {
                                //this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((ISelectForm)form).SelectedItem;
                                DataGridViewRow row = this.Rows[e.RowIndex];
                                PropertyDescriptor pd = TypeDescriptor.GetProperties(row.DataBoundItem)[col.DataPropertyName];
                                if (pd != null)
                                {
                                    pd.SetValue(row.DataBoundItem, ((ISelectForm)form).SelectedItem);
                                }
                                
                            }
                        }
                    }
                }
            }
            base.OnCellValidating(e);
        }
    }
}
