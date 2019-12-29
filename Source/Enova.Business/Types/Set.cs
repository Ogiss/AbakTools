using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Types
{
    public class Set<T> : Dictionary<T, bool>, ICollection<T>, IEnumerable<T>, IEnumerable, IEquatable<Set<T>>
    {
        // Methods
        public Set()
        {
        }

        public Set(IEnumerable<T> en)
        {
            foreach (T local in en)
            {
                this.Add(local);
            }
        }

        public Set(int capacity)
            : base(capacity)
        {
        }

        public void Add(T value)
        {
            base[value] = false;
        }

        public void AddRange(IEnumerable<T> en)
        {
            foreach (T local in en)
            {
                base[local] = false;
            }
        }

        public bool Contains(T item)
        {
            return base.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            base.Keys.CopyTo(array, arrayIndex);
        }

        public override bool Equals(object obj)
        {
            Set<T> other = obj as Set<T>;
            if (other == null)
            {
                return false;
            }
            return this.Equals(other);
        }

        public bool Equals(Set<T> other)
        {
            if (base.Count != other.Count)
            {
                return false;
            }
            foreach (T local in other.Keys)
            {
                if (!this.Contains(local))
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return base.Keys.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return base.Count;
        }

        public static Set<T> operator +(Set<T> s, T value)
        {
            s.Add(value);
            return s;
        }

        public static Set<T> operator -(Set<T> s, T value)
        {
            s.Remove(value);
            return s;
        }

        public T[] ToArray()
        {
            T[] array = new T[base.Count];
            base.Keys.CopyTo(array, 0);
            return array;
        }

        // Properties
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }
    }
}
