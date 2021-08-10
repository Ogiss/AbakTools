using System;
using System.Reflection;

namespace BAL.Types
{
    public class Action : BAL.Business.IDataContexable
    {
        #region Fields

        private ActionAttribute actionAttribute;
        private object actionObject;
        private BAL.Business.DataContext dataContext;
        private MethodInfo actionMethod;
        private PropertyInfo visibleProperty;

        #endregion

        #region Properties

        public BAL.Business.DataContext DataContext
        {
            get { return this.dataContext; }
            set { this.dataContext = value; }
        }

        public ActionAttribute ActionAttribute
        {
            get { return this.actionAttribute; }
        }

        public bool Visible
        {
            get
            {
                if (this.visibleProperty != null)
                    return (bool)this.visibleProperty.GetValue(this.actionObject, null);
                return true;
            }
        }
    

        #endregion

        #region Methods

        public Action(ActionAttribute actionAttribute, BAL.Business.DataContext dataContext)
        {
            this.actionAttribute = actionAttribute;
            this.dataContext = dataContext;
            this.init();
        }

        private void init()
        {
            if (this.actionObject == null)
            {
                var cinfo = this.actionAttribute.ActionType.GetConstructor(new Type[0]);
                this.actionObject = cinfo.Invoke(new object[0]);
                if (this.actionObject is BAL.Business.IDataContexable)
                    ((BAL.Business.IDataContexable)this.actionObject).DataContext = this.DataContext;
                this.actionMethod = this.ActionAttribute.ActionType.GetMethod("Action");
                var actionObiectProperty = this.ActionAttribute.ActionType.GetProperty("ActionObject");
                if (actionObiectProperty != null)
                    actionObiectProperty.SetValue(this.actionObject, this, null);

                var actionDataProperty = this.ActionAttribute.ActionType.GetProperty("ActionData");
                if (actionDataProperty != null)
                    actionDataProperty.SetValue(this.actionObject, this.ActionAttribute.ActionData, null);

                this.visibleProperty = this.ActionAttribute.ActionType.GetProperty("Visible");
            }
        }

        public void Run()
        {
            if (this.actionMethod != null)
            {
                this.actionMethod.Invoke(this.actionObject, new object[0]);
            }
        }

        #endregion
    }
}
