using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BAL.Forms
{
    public class MenuItem : ToolStripMenuItem, IMenuItem
    {
        #region Fields

        private IMenuItem parent;
        private MenuActionsType menuAction;
        private Type formType;

        #endregion

        #region Properties

        public IMenuItem Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public MenuActionsType MenuAction
        {
            get { return this.menuAction; }
            set { this.menuAction = value; }
        }

        public Type FormType
        {
            get { return this.formType; }
            set { this.formType = value; }
        }

        public IList SubMenu
        {
            get { return this.DropDownItems; }
        }

        public MenuActionAttribute ActionAttribute { get; set; }
    

        #endregion

        #region Methods

        public int CompareTo(IMenuItem item)
        {
            return item.Text.CompareTo(this.Text);
        }

        int IComparable.CompareTo(object obj)
        {
            return this.CompareTo((IMenuItem)obj);
        }

        public string GetMenuPath()
        {
            string s = parent != null ? ((MenuItem)parent).GetMenuPath() + "\\" : "";
            return s + this.Text;
        }

        #endregion
    }
}
