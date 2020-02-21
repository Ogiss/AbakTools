using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited=true)]
    public class DataFormAttribute : BAL.Types.AttributeBase
    {
        #region Fields

        private Type formType;

        #endregion

        #region Properties


        public Type FormType
        {
            get { return this.formType; }
        }

        #endregion

        #region Methods

        public DataFormAttribute(Type formType)
        {
            this.formType = formType;
        }

        public DataFormAttribute(string formTypeName) : this(Type.GetType(formTypeName)) { }

        public static Type GetFormType(object obj)
        {
            Type type = obj is Type ? (Type)obj : obj.GetType();
            var attrs = type.GetCustomAttributes(typeof(DataFormAttribute), true);
            if (attrs != null && attrs.Length > 0)
                return ((DataFormAttribute)attrs[0]).FormType;
            return null;
        }

        #endregion
    }
}
