using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: BAL.Business.AppService(typeof(BAL.Forms.IFormService), typeof(BAL.Test.FormService))]

namespace BAL.Test
{
    public class FormService : BAL.Forms.FormService
    {


        #region Properties

        public override void Init()
        {
            base.Init();
            var db = BAL.Business.AppController.Instance["BALTEST"];
            db.Login(null, null, null);
        }

        public override Type MainFormType
        {
            get
            {
                return typeof(MainForm);
            }
        }

        public override string ApplicationName
        {
            get
            {
                return "Test application";
            }
        }

        #endregion
    }
}
