using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using BAL.Types;

namespace BAL.Business
{
    public class SubRow : RowBase
    {
        #region Fields

        private string name;
        private IRow parent;
        private Row root;

        #endregion

        #region Properties

        [NotMapped, Hidden]
        public Row Root
        {
            get { return this.root; }
        }

        [NotMapped, Hidden]
        public IRow Parent
        {
            get { return this.parent; }
        }

        [NotMapped, Hidden]
        public string Name
        {
            get { return this.name; }
        }

        #endregion

        #region Methods

        public void AssignParent(IRow parent, string name)
        {
            this.root = parent.Root;
            this.parent = parent;
            this.name = name;
        }

        protected virtual void SetValue(Row.MethodDelegate setter, params string[] propertyName)
        {
            if (root != null)
                root.SetValue(setter, this.GetName() + "." + propertyName);
            else
                setter();

        }

        protected virtual string GetName()
        {
          //  return root.GetType().Name + "." + this.name;
            return this.name;
        }

        #endregion
    }
}
