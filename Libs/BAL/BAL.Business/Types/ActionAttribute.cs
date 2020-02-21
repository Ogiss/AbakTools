using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Types
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class ActionAttribute : PriorityAttribute
    {
        #region Fields

        private static List<ActionAttribute> __cache__;
        private static Dictionary<Type, List<ActionAttribute>> __cache__byDataType;

        private string path;
        private string description;
        private ActionTarget target;
        private Type actionType;
        private Type dataType;
        private object[] actionData;

        #endregion

        #region Properties

        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public ActionTarget Target
        {
            get { return this.target; }
            set { this.target = value; }
        }

        public Type ActionType
        {
            get { return this.actionType; }
            set { this.actionType = value; }
        }

        public Type DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }

        public object[] ActionData
        {
            get { return this.actionData; }
            set { this.actionData = value; }
        }

        #endregion

        #region Methods

        public ActionAttribute(Type actionType, Type dataType, string path)
        {
        }

        public ActionAttribute(Type actionType, string path) : this(actionType, null, path) { }

        public ActionAttribute(Type actionType, Type dataType) : this(actionType, dataType, null) { }

        public ActionAttribute(Type actionType) : this(actionType, null, null) { }

        public ActionAttribute() : this(null, null, null) { }

        private static void checkCache()
        {
            if (__cache__ == null)
            {
                __cache__ = new List<ActionAttribute>();
                __cache__byDataType = new Dictionary<Type, List<ActionAttribute>>();
                var attrs = BAL.Business.AppController.Instance.AssemblyAttributes[typeof(ActionAttribute)];
                if (attrs != null)
                {
                    foreach (ActionAttribute attr in attrs)
                    {
                        __cache__.Add(attr);
                        if (attr.DataType != null)
                        {
                            if (!__cache__byDataType.ContainsKey(attr.DataType))
                                __cache__byDataType.Add(attr.DataType, new List<ActionAttribute>());
                            __cache__byDataType[attr.DataType].Add(attr);
                        }
                    }
                }
            }
        }

        public static IEnumerable<ActionAttribute> GetActions()
        {
            checkCache();
            return __cache__.OrderBy(a => a.Priority).ThenBy(a => a.Path); ;
        }

        public static IEnumerable<ActionAttribute> GetActions(Type dataType)
        {
            checkCache();
            var list = __cache__byDataType.ContainsKey(dataType) ? __cache__byDataType[dataType] : new List<ActionAttribute>();
            return list.OrderBy(a=>a.Priority).ThenBy(a=>a.Path);
        }

        public static IEnumerable<ActionAttribute> GetActions(Type dataType, ActionTarget target)
        {
            var list = new List<ActionAttribute>();
            foreach (var attr in GetActions(dataType))
            {
                if ((attr.Target & target) == ActionTarget.None)
                    continue;
                list.Add(attr);
            }
            return list;
        }

        #endregion
    }
}
