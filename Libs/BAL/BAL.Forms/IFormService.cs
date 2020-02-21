using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;

namespace BAL.Forms
{
    public interface IFormService : IAppService
    {
        #region Properties

        Type MainFormType { get; }
        Type MenuItemType { get; }
        Type GridFormType { get; }
        Control GetDataContextParamControl(DataContextParam param);
        Type GetDataEditFormType(Type dataType);
        string ApplicationName { get; }
        bool CheckMenuActionRights(IMenuItem menuItem);
        void Init();
        void Finish();

        #endregion

        #region Methods

        #endregion

    }
}
