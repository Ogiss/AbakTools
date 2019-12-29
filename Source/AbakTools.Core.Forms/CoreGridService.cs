using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;

[assembly: AppService(typeof(IGridService), typeof(AbakTools.Core.Forms.CoreGridService))]

namespace AbakTools.Core.Forms
{
    public class CoreGridService : GridService, IGridService
    {
        public override string[] GetAvailableKeys()
        {
            return new string[]{
                "StatusyDokumentowView",
            };
        }

        public override Type GetDefaultGridTemplateType(string key)
        {
            switch (key)
            {
                case "StatusyDokumentowView":
                    return typeof(AbakTools.Core.Forms.StatusyDokumentowGridTemplate);
            }
            return base.GetDefaultGridTemplateType(key);
        }
    }
}
