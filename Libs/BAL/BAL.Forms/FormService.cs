using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;

namespace BAL.Forms
{
    public class FormService : AppServiceBase, IFormService
    {
        #region Properties

        public virtual Type MainFormType
        {
            get { return typeof(StdMainForm); }
        }

        public virtual string ApplicationName
        {
            get { return "BAL.Test"; }
        }

        public virtual Type MenuItemType
        {
            get { return typeof(MenuItem); }
        }

        public virtual Type GridFormType
        {
            get { return typeof(DataGridFormOld); }
        }

        #endregion

        #region Methods

        public virtual void Init() { }
        public virtual void Finish() { }

        public virtual Control GetDataContextParamControl(DataContextParam param)
        {
            var utype = Nullable.GetUnderlyingType(param.PropertyPath.Last.PropertyType);
            var dataType = utype ?? param.PropertyPath.Last.PropertyType;
            if (typeof(Row).IsAssignableFrom(dataType))
                return new Controls.SelectBox();
            return new TextBox();
        }

        public virtual Type GetDataEditFormType(Type dataType)
        {
            var type = DataFormAttribute.GetFormType(dataType);
            if (type != null)
                return type;
            else
                return typeof(DataEditForm);
        }

        public virtual bool CheckMenuActionRights(IMenuItem menuItem)
        {
            return true;
        }

        #endregion
    }
}
