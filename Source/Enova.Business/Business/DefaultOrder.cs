using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple= true, Inherited=true)]
    public class DefaultOrder : Attribute
    {
        #region Fields

        private string propertyName;
        private ListSortDirection direction = ListSortDirection.Ascending;

        #endregion

        #region Properties

        public string PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public ListSortDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        #endregion

        #region Methods

        public DefaultOrder(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public static DefaultOrder GetAttribute(object obj)
        {
            var attributes = obj.GetType().GetCustomAttributes(typeof(DefaultOrder), true);
            if (attributes.Length > 0)
                return (DefaultOrder)attributes[0];
            return null;
        }

        public static string GetPropertyName(object obj)
        {
            var attr = GetAttribute(obj);
            if (attr != null)
                return attr.PropertyName;
            return null;
        }

        public static ListSortDirection GetDirection(object obj)
        {
            var attr = GetAttribute(obj);
            if (attr != null)
                return attr.Direction;
            return ListSortDirection.Ascending;
        }

        #endregion
    }
}
