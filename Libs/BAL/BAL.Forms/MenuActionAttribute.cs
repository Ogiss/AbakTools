using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Forms
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class MenuActionAttribute : BAL.Types.AttributeBase, BAL.Types.IPriority, IComparable
    {
        #region Fields

        private string menuPath;
        private Type formType;
        private MenuActionsType menuAction;
        private int priority;
        private Type dataType;
        private Type viewType;
        private ActionOptions options;
        private object data;

        #endregion

        #region Properties

        public string MenuPath
        {
            get { return this.menuPath; }
            set { this.menuPath = value; }
        }

        public Type FormType
        {
            get { return this.formType; }
            set { this.formType = value; }
        }

        public MenuActionsType MenuAction
        {
            get { return this.menuAction; }
            set { this.menuAction = value; }
        }

        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public Type DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }

        public Type ViewType
        {
            get { return this.viewType; }
            set { this.viewType = value; }
        }

        public ActionOptions Options
        {
            get { return this.options; }
            set { this.options = value; }
        }

        public object Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        #endregion

        #region Methods

        public MenuActionAttribute(string menuPath, MenuActionsType menuAction, Type formType)
        {
            this.menuPath = menuPath;
            this.menuAction = menuAction;
            this.formType = formType;
        }

        public MenuActionAttribute(string menuPath, MenuActionsType menuAction) : this(menuPath, menuAction, null) { }

        public MenuActionAttribute(string menuPath) : this(menuPath, MenuActionsType.None) { }

        public MenuActionAttribute(string menuPath, Type formType) : this(menuPath, MenuActionsType.OpenForm, formType) { }

        public int CompareTo(object obj)
        {
            int comp = this.Priority.CompareTo(((MenuActionAttribute)obj).Priority);
            if (comp == 0)
                comp = this.MenuPath.CompareTo(((MenuActionAttribute)obj).MenuPath);
            return comp;
        }

        #endregion

    }
}
