using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class RowActionAttribute : BAL.Types.AttributeBase
    {
        #region Fields

        private Type rowType;
        private Type actionType;
        private string dataContextKey;
        private ActionTarget target;

        #endregion

        #region Properties

        public Type ActionType
        {
            get { return this.actionType; }
        }

        public Type RowType
        {
            get { return this.rowType; }
            set { this.rowType = value; }
        }

        public string DataContextKey
        {
            get { return this.dataContextKey; }
            set { this.dataContextKey = value; }
        }

        public ActionTarget Target
        {
            get { return this.target; }
            set { this.target = value; }
        }
        #endregion

        #region Methods

        public RowActionAttribute(Type rowType, Type actionType)
        {
            this.actionType = actionType;
            this.rowType = rowType;
        }

        public RowActionAttribute(Type actionType)
        {
            this.actionType = actionType;
        }

        public static IEnumerable<Type> GetActionsTypes(DataContext context)
        {
            if (context != null)
            {
                List<Type> types = new List<Type>();
                var attrs = AppController.Instance.AssemblyAttributes[typeof(RowActionAttribute)];
                if (attrs != null && attrs.Count > 0)
                {
                    var rowType = context.GetDataType();
                    foreach (var attr in attrs.Cast<RowActionAttribute>())
                    {
                        if (attr.RowType != null && attr.RowType != rowType || !string.IsNullOrEmpty(attr.DataContextKey) && attr.DataContextKey != context.Key
                            || attr.RowType == null && string.IsNullOrEmpty(attr.DataContextKey))
                            continue;
                        types.Add(attr.ActionType);
                    }

                    return types;
                }
            }
            return null;
        }

        #endregion
    }
}
