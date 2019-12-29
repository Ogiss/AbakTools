using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: BAL.Business.AppService(typeof(BAL.Forms.IFormService), typeof(EnovaToolsExplorer.FormService))]

namespace EnovaToolsExplorer
{
    public class FormService : BAL.Forms.FormService
    {
        public static BAL.Business.App.IDatabase CurrentDatabase;

        public override string ApplicationName
        {
            get
            {
                return "AbakTools";
            }
        }

        public override Type MainFormType
        {
            get
            {
                return typeof(MainForm);
            }
        }

        public override void Init()
        {
            try
            {
                base.Init();

                CurrentDatabase = BAL.Business.AppController.Instance["AbakTools"];
                CurrentDatabase.Login(null, null, null);
                BAL.Business.AppController.ExceptionProcess = (ex) =>
                {
                    new AbakTools.Forms.ExceptionForm(ex).ShowDialog();
                };
                
            }
            catch { }
        }

        public override Type GridFormType
        {
            get
            {
                //return Type.GetType("Enova.Forms.DataGridFormWithEnovaAPI, Enova.Forms");
                return Type.GetType("Enova.Forms.DataGridFormWithEnovaAPIOld, Enova.Forms");
            }
        }

        public override bool CheckMenuActionRights(BAL.Forms.IMenuItem menuItem)
        {
            if (menuItem.ActionAttribute.Data != null && typeof(AbakTools.Business.OperatorPrawaDostepu).IsAssignableFrom(menuItem.ActionAttribute.Data.GetType()))
            {
                var rights = (AbakTools.Business.OperatorPrawaDostepu)menuItem.ActionAttribute.Data;
                //if (!AbakTools.Business.Operator.CurrentOperator.CheckPrawaDostepu(rights))
                if(Enova.Business.Old.DB.Web.Operator.CurrentOperator.CheckPrawaDostepu(rights))
                {
                    BAL.Forms.FormManager.Alert("Nie masz wystarczających uprawnień");
                    return false;
                }
            }
            return base.CheckMenuActionRights(menuItem);
        }

        public override Type GetDataEditFormType(Type dataType)
        {
            if (typeof(Enova.API.Business.Row).IsAssignableFrom(dataType))
                return typeof(Enova.Forms.EnovaDataEditForm);
            return base.GetDataEditFormType(dataType);
        }

    }
}
