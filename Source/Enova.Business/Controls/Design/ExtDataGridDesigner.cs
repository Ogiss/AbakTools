using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enova.Business.Old.Controls.Design
{
    public class ExtDataGridDesigner : ControlDesigner
    {
        internal static void ShowErrorDialog(IUIService uiService, Exception ex, Control dataGridView)
        {
            if (uiService != null)
            {
                uiService.ShowError(ex);
            }
            else
            {
                string message = ex.Message;
                if ((message == null) || (message.Length == 0))
                {
                    message = ex.ToString();
                }
                RTLAwareMessageBox.Show(dataGridView, message, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 0);
            }
        }

        internal static void ShowErrorDialog(IUIService uiService, string errorString, Control dataGridView)
        {
            if (uiService != null)
            {
                uiService.ShowError(errorString);
            }
            else
            {
                RTLAwareMessageBox.Show(dataGridView, errorString, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 0);
            }
        }

 

 

    }

    internal static class RTLAwareMessageBox
    {
        // Methods
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            if (IsRTLResources)
            {
                options |= MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
            }
            return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
        }

        // Properties
        public static bool IsRTLResources
        {
            get
            {
                return true; // (SR.GetString("RTL") != "RTL_False");
            }
        }
    }


}
