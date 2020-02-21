using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using BAL.Types;

namespace BAL.Business
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true, Inherited=true)]
    public class RowInvokerAttribute : AttributeBase
    {
        #region Fields

        private Type rowType;
        private Type invokerType;
        private static Dictionary<Type, SortedSet<RowInvokerInfo>> __cache__;

        #endregion

        #region Properties

        public Type RowType
        {
            get { return this.rowType; }
        }

        public Type InvokerType
        {
            get { return this.invokerType; }
        }

        #endregion

        #region Methods

        public RowInvokerAttribute(Type rowType, Type invokerType)
        {
            this.rowType = rowType;
            this.invokerType = invokerType;
        }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new Dictionary<Type, SortedSet<RowInvokerInfo>>();
                var attrs = AppController.Instance.AssemblyAttributes[typeof(RowInvokerAttribute)];
                if (attrs != null && attrs.Count > 0)
                {
                    foreach (RowInvokerAttribute attr in attrs)
                    {
                        if (!__cache__.ContainsKey(attr.RowType))
                            __cache__.Add(attr.RowType, new SortedSet<RowInvokerInfo>());

                        __cache__[attr.RowType].Add(new RowInvokerInfo(attr.InvokerType));
                    }
                }
            }
        }

        private static IEnumerable<RowInvokerInfo> GetInvokers(Type rowType)
        {
            checkCache();
            if (__cache__.ContainsKey(rowType))
                return __cache__[rowType];
            return new RowInvokerInfo[0];
        }

        private static Type[] GetBaseTypes(Type type)
        {
            List<Type> list = new List<Type>();
            Type t = type;
            do
            {
                list.Add(t);
                t = t.BaseType;
            } while (t != typeof(RowBase));
            list.Add(typeof(RowBase));
            return list.ToArray();
        }

        public static void Invoke(RowInvokeType type, object sender, EventArgs e)
        {
            var t = sender.GetType();
            if (typeof(RowBase).IsAssignableFrom(t))
            {
                checkCache();
                var types = GetBaseTypes(t);
                for (int i = types.Length - 1; i >= 0; i--)
                {
                    var t2 = types[i];
                    if (__cache__.ContainsKey(t2))
                    {
                        foreach (var info in __cache__[t2])
                        {
                            info.Invoke(type, sender, e);
                        }
                    }
                }
            }
        }

        #endregion

        #region Nested Types

        public class RowInvokerInfo: IComparable<RowInvokerInfo>
        {
            #region Fields

            private int priority;
            private object invoker;
            private string invokerName;
            //private MethodInfo onCreated;
            private MethodInfo onAdded;
            private MethodInfo onDeleting;
            private MethodInfo onDeleted;
            private MethodInfo onEdited;
            private MethodInfo onLoaded;
            private MethodInfo onSaved;

            #endregion

            #region Methods

            public RowInvokerInfo(Type invokerType)
            {
                this.invoker = invokerType.GetConstructor(new Type[0]).Invoke(new object[0]);

                var attrs = invokerType.GetCustomAttributes(typeof(PriorityAttribute), true);
                if (attrs.Length > 0)
                    this.priority = ((PriorityAttribute)attrs[0]).Priority;
                else
                    this.priority = 1000;

                this.invokerName = invokerType.FullName;

                //this.onCreated = invokerType.GetMethod("OnCreated", new Type[] { typeof(object), typeof(EventArgs) });
                this.onAdded = invokerType.GetMethod("OnAdded", new Type[] { typeof(object), typeof(EventArgs) });
                this.onDeleting = invokerType.GetMethod("OnDeleting", new Type[] { typeof(object), typeof(EventArgs) });
                this.onDeleted = invokerType.GetMethod("OnDeleted", new Type[] { typeof(object), typeof(EventArgs) });
                this.onEdited = invokerType.GetMethod("OnEdited", new Type[] { typeof(object), typeof(EventArgs) });
                this.onLoaded = invokerType.GetMethod("OnLoaded", new Type[] { typeof(object), typeof(EventArgs) });
                this.onSaved = invokerType.GetMethod("OnSaved", new Type[] { typeof(object), typeof(EventArgs) });
            }

            public int CompareTo(RowInvokerInfo other)
            {
                if (this.priority != other.priority)
                    return this.priority.CompareTo(other.priority);
                return this.invokerName.CompareTo(other.invokerName);
            }

            public void Invoke(RowInvokeType invokeType, object sender, EventArgs e)
            {
                switch (invokeType)
                {
                   // case RowInvokeType.Created:
                   //     if(this.onCreated != null)
                  //          this.onCreated.Invoke(this.invoker, new object[] { sender, e });
                  //      break;
                    case RowInvokeType.Added:
                        if (this.onAdded != null)
                            this.onAdded.Invoke(this.invoker, new object[] { sender, e });
                        break;
                    case RowInvokeType.Deleting:
                        if (this.onDeleting != null)
                            this.onDeleting.Invoke(this.invoker, new object[] { sender, e });
                        break;
                    case RowInvokeType.Deleted:
                        if(this.onDeleted!=null)
                            this.onDeleted.Invoke(this.invoker, new object[] { sender, e });
                        break;
                    case RowInvokeType.Edited:
                        if(this.onEdited!=null)
                            this.onEdited.Invoke(this.invoker, new object[] { sender, e });
                        break;
                    case RowInvokeType.Loaded:
                        if(this.onLoaded!=null)
                            this.onLoaded.Invoke(this.invoker, new object[] { sender, e });
                        break;
                    case RowInvokeType.Saved:
                        if(this.onSaved!=null)
                            this.onSaved.Invoke(this.invoker, new object[] { sender, e });
                        break;

                }
            }

            #endregion

        }

        #endregion

    }
}
