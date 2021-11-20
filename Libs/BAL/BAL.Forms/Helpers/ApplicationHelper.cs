using System.Windows.Forms;

namespace BAL.Forms.Helpers
{
    public static class ApplicationHelper
    {
        public static Form GetLastOpenedForm()
        {
            if (Application.OpenForms.Count > 0)
            {
                return Application.OpenForms[Application.OpenForms.Count - 1];
            }

            return null;
        }
    }
}
