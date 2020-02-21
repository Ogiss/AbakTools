using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;

namespace BAL.Business
{
    public class AttributesCollection : IEnumerable , IEnumerable<AttributeBase>
    {
        #region Fields

        private List<AttributeBase> list;
        private Dictionary<Type, ICollection<AttributeBase>> byType;

        #endregion

        #region Properties

        public ICollection<AttributeBase> this[Type type]
        {
            get
            {
                if (byType.ContainsKey(type))
                    return byType[type];
                return new AttributeBase[0];
            }
        }

        #endregion

        #region Methods

        public AttributesCollection()
        {
            this.list = new List<AttributeBase>();
            this.byType = new Dictionary<Type, ICollection<AttributeBase>>();
        }

        public void Add(AttributeBase attribute)
        {
            this.list.Add(attribute);

            if (!byType.ContainsKey(attribute.GetType()))
            {
                if (attribute is IPriority || attribute is IComparable)
                {
                    byType.Add(attribute.GetType(), new SortedSet<AttributeBase>(new PriorityAttributeComparer()));
                }
                else
                    byType.Add(attribute.GetType(), new List<AttributeBase>());
            }

            byType[attribute.GetType()].Add(attribute);
        }

        public IEnumerator<AttributeBase> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Nested types

        public class PriorityAttributeComparer : IComparer<AttributeBase>
        {
            public int Compare(AttributeBase x, AttributeBase y)
            {
                if (x.GetType() == y.GetType())
                {
                    if (x is IComparable)
                        return ((IComparable)x).CompareTo(y);

                    return ((IPriority)x).Priority.CompareTo(((IPriority)y).Priority);
                }
                else
                    return x.GetType().FullName.CompareTo(y.GetType().FullName);
            }
        }

        #endregion

    }
}
