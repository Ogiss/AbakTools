using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;

[assembly: AppService(typeof(IGridService), typeof(Enova.Forms.EnovaGridService))]

namespace Enova.Forms
{
    public class EnovaGridService : GridService, IGridService
    {
        public override string[] GetAvailableKeys()
        {
            return new string[]{
                "KontrahenciEnovaView",
                "KontrahenciEnovaViewSelect",
            };
        }

        public override Type GetDefaultGridTemplateType(string key)
        {
            switch (key)
            {
                case "KontrahenciEnovaView":
                case "KontrahenciEnovaViewSelect":
                    return typeof(Enova.Forms.CRM.KontrahenciViewGridTemplate);
            }
            return base.GetDefaultGridTemplateType(key);
        }
    }
}
