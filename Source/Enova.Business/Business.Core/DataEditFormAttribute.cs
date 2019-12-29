using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataEditFormAttribute : Attribute
    {
        private Type type = null;
        private string typeName = "";

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        public DataEditFormAttribute(string TypeName)
        {
            this.typeName = TypeName;
        }

        public DataEditFormAttribute(Type type)
        {
            this.type = type;
            this.typeName = type.FullName;
        }

    }
}
