using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Enova.Old.Tools
{
    [Serializable]
    public class PriorityAttribute : AssemblyAttribute, IComparable, IComparable<PriorityAttribute>
    {
        // Fields
        public const int DefaultPriorityValue = 100;
        public static readonly PriorityAttribute Hunderd = new PriorityAttribute();
        public int MasterPriority;
        public int Priority;
        public static readonly PropertyDescriptorComparerImpl PropertyDescriptorComparer = new PropertyDescriptorComparerImpl();

        // Methods
        public PriorityAttribute()
        {
            this.Priority = 100;
            this.MasterPriority = 100;
        }

        public PriorityAttribute(int priority)
        {
            this.Priority = 100;
            this.MasterPriority = 100;
            this.Priority = priority;
        }

        int IComparable.CompareTo(object o)
        {
            int num = this.MasterPriority - ((PriorityAttribute)o).MasterPriority;
            if (num != 0)
            {
                return num;
            }
            return (this.Priority - ((PriorityAttribute)o).Priority);
        }

        int IComparable<PriorityAttribute>.CompareTo(PriorityAttribute other)
        {
            int num = this.MasterPriority - other.MasterPriority;
            if (num != 0)
            {
                return num;
            }
            return (this.Priority - other.Priority);
        }

        // Nested Types
        public class PropertyDescriptorComparerImpl : IComparer, IComparer<PropertyDescriptor>
        {
            // Methods
            public int Compare(PropertyDescriptor pd1, PropertyDescriptor pd2)
            {
                PriorityAttribute attribute = (PriorityAttribute)pd1.Attributes[typeof(PriorityAttribute)];
                PriorityAttribute attribute2 = (PriorityAttribute)pd2.Attributes[typeof(PriorityAttribute)];
                int num = ((attribute == null) ? 100 : attribute.Priority) - ((attribute2 == null) ? 100 : attribute2.Priority);
                if (num != 0)
                {
                    return num;
                }
                return string.Compare(pd1.Name, pd2.Name, true);
            }

            public int Compare(object o1, object o2)
            {
                return this.Compare((PropertyDescriptor)o1, (PropertyDescriptor)o2);
            }
        }
    }
}
