using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Printer
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
    public class RowPrintTemplateAttribute : Attribute, IComparable<RowPrintTemplateAttribute>
    {
        private Type rowType;
        private string template;
        private string name;
        private int priority;
        private static Dictionary<Type, SortedSet<RowPrintTemplateAttribute>> __cache__;

        public Type RowType
        {
            get { return rowType; }
        }

        public string Template
        {
            get { return template; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Priority
        {
            get { return priority; }
        }

        public RowPrintTemplateAttribute(Type rowType, string template, string name, int priority = 10000)
        {
            this.rowType = rowType;
            this.template = template;
            this.name = name;
            this.priority = 0;
        }

        public int CompareTo(RowPrintTemplateAttribute other)
        {
            int cmp = this.Priority.CompareTo(other.Priority);
            if (cmp == 0)
                cmp = this.Name.CompareTo(other.Name);
            return cmp;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new Dictionary<Type, SortedSet<RowPrintTemplateAttribute>>();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var asm in assemblies)
                {
                    if (asm.FullName.StartsWith("Enova"))
                    {
                        var attributes = asm.GetCustomAttributes(typeof(RowPrintTemplateAttribute), true);
                        if (attributes != null && attributes.Length > 0)
                        {
                            foreach (RowPrintTemplateAttribute attr in attributes)
                            {
                                if (!__cache__.ContainsKey(attr.RowType))
                                    __cache__.Add(attr.RowType, new SortedSet<RowPrintTemplateAttribute>());
                                __cache__[attr.RowType].Add(attr);
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<RowPrintTemplateAttribute> GetAttributes(object obj)
        {
            var t = obj is Type ? (Type)obj : obj.GetType();
            checkCache();
            if (__cache__.ContainsKey(t))
                return __cache__[t];
            return new RowPrintTemplateAttribute[0];
        }

    }
}
