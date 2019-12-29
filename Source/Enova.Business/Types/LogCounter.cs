using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    public abstract class LogCounter : IEnumerable
    {
        // Fields
        private string name;
        private static WeakCollection<LogCounter> regColls = new WeakCollection<LogCounter>();

        // Methods
        protected LogCounter()
        {
        }

        public virtual string GetObjectInfo(object obj)
        {
            if (obj == null)
            {
                return "NULL";
            }
            return obj.ToString();
        }

        public static IEnumerable<LogCounter> GetRegistered()
        {
            return regColls;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        // Properties
        public abstract int Count { get; }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                regColls.Add(this);
            }
        }
    }

}
