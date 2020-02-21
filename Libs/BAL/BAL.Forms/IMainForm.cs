using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    public interface IMainForm : IFormWithMenu
    {
        #region Properties

        string Title { get; set; }
        string StatusLineText { get; set; }
        void OpenForm(Type formType, string title);
        

        #endregion
    }
}
