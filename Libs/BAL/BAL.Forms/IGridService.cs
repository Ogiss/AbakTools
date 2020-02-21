using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Forms
{
    public interface IGridService : IAppService
    {
        string[] GetAvailableKeys();
        Type GetDefaultGridTemplateType(string key);
    }
}
