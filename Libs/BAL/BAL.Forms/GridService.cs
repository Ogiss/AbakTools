using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Forms
{
    public class GridService : AppServiceBase , IGridService
    {
        #region Methods

        public virtual string[] GetAvailableKeys()
        {
            return null;
        }

        public virtual Type GetDefaultGridTemplateType(string key)
        {
            return typeof(GridTemplate);
        }

        #endregion
    }
}
