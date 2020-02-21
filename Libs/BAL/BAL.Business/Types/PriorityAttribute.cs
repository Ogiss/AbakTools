using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    
    [AttributeUsage(AttributeTargets.All, AllowMultiple=true, Inherited = true)]
    public class PriorityAttribute : AttributeBase
    {
        #region Fields

        private int priority;

        #endregion

        #region Properties

        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        #endregion

        #region Methods

        public PriorityAttribute(int priority)
        {
            this.priority = priority;
        }

        public PriorityAttribute()
            : this(1000)
        {
        }

        public static int GetPriority(object obj, int defaultPriority = 10000)
        {
            Type type = (obj is Type) ? (Type)obj : obj.GetType();
            var attributes = type.GetCustomAttributes(typeof(PriorityAttribute), true);
            if (attributes.Length > 0)
                return ((PriorityAttribute)attributes[0]).Priority;
            return defaultPriority;
        }

        #endregion

    }
}
