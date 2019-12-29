using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API;

[assembly: TypeMap("Soneta.Business.MemoText, Soneta.Business", null, typeof(Enova.API.Business.MemoText))]

namespace Enova.API.Business
{
    public class MemoText : Types.ObjectBase, IList
    {
        #region Fields

        public static MemoText Empty
        {
            get
            {
                return (MemoText)Enova.API.EnovaService.Instance.GetStaticValue("Soneta.Business.MemoText, Soneta.Business", "Empty");
            }
        }

        #endregion

        #region Properties

        public int Count
        {
            get { return FromEnova<int>("Count"); }
        }

        public bool IsChanged
        {
            get { return FromEnova<bool>("IsChanged"); }
        }

        public bool IsEmpty
        {
            get { return FromEnova<bool>("IsEmpty"); }
        }

        public bool IsFixedSize
        {
            get { return FromEnova<bool>("IsFixedSize"); }
        }

        public bool IsReadOnly
        {
            get { return FromEnova<bool>("IsReadOnly"); }
        }

        public string this[int index]
        {
            get
            {
                return (string)GetValue("Item", new object[] { index });
            }
            set
            {
                SetValue("Item", value, new object[] { index });
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = value.ToString();
            }
        }


        public bool IsSynchronized
        {
            get { return FromEnova<bool>("IsSynchronized"); }
        }

        public object SyncRoot
        {
            get { return GetValue("SyncRoot"); }
        }

        #endregion

        #region Methods

        public int Add(object value)
        {
            return (int)CallMethod("Add", ToEnova(value));
        }

        public void Clear()
        {
            CallMethod("Clear");
        }

        public bool Contains(object value)
        {
            return (bool)CallMethod("Contains", FromEnova(value));
        }

        public int IndexOf(object value)
        {
            return (int)CallMethod("IndexOf", ToEnova(value));
        }

        public void Insert(int index, object value)
        {
            CallMethod("Insert", index, ToEnova(value));
        }

        public void Remove(object value)
        {
            CallMethod("Remove", ToEnova(value));
        }

        public void RemoveAt(int index)
        {
            CallMethod("removeAt", index);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return new Connector.Business.EnovaEnumerator() { EnovaObject = ((IEnumerable)EnovaObject).GetEnumerator() };
        }


        /*
        public MemoText Clone();
        public TextReader CreateReader();
        public IEnumerator GetEnumerator();
        public static implicit operator string(MemoText memo);
        public static implicit operator MemoText(string v);
        public void SetArray(string[] t);
        int IList.Add(object value);
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        object ICloneable.Clone();
        public string[] ToArray();
        public override string ToString();
        */

        #endregion
    }
}
