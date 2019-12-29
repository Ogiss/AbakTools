using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls.Design
{
    class ExtDataGridColumnsEditor : UITypeEditor
    {
        private ExtDataGridColumnCollectionDialog extDataGridColumnCollectionDialog;
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if ((service == null) || (context.Instance == null))
                {
                    return value;
                }
                IDesignerHost host = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
                if (host == null)
                {
                    return value;
                }
                if (this.extDataGridColumnCollectionDialog == null)
                {
                    this.extDataGridColumnCollectionDialog = new ExtDataGridColumnCollectionDialog();
                }
                //this.extDataGridColumnCollectionDialog.SetDataGrid((ExtDataGrid)context.Instance);
                using (DesignerTransaction transaction = host.CreateTransaction("ExtDataGridColumnCollectionTransaction"))
                {
                    if (service.ShowDialog(this.extDataGridColumnCollectionDialog) == DialogResult.OK)
                    {
                        transaction.Commit();
                        return value;
                    }
                    transaction.Cancel();
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
