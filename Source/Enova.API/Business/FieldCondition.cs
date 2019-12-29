using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;

namespace Enova.API.Business
{
    public class FieldCondition : RowCondition
    {
        #region Methods

        public override Type GetEnovaType()
        {
            return Type.GetType("Soneta.Business.FieldCondition, Soneta.Business");
        }

        #endregion

        #region Nested Types

        public class Compare : FieldCondition
        {
        }

        public class Equal : Compare
        {
            public override Type GetEnovaType()
            {
                return base.GetEnovaType().GetNestedType("Equal");
            }

            public Equal(string path, object value)
            {
                EnovaObject = GetEnovaType().GetConstructor(new Type[] { typeof(string), typeof(object) }).Invoke(new object[]{
                        path,
                        value is IObjectBase ? ((IObjectBase)value).EnovaObject : value
                    });
            }
        }

        public class Greater : Compare
        {
            public override Type GetEnovaType()
            {
                return base.GetEnovaType().GetNestedType("Greater");
            }

            public Greater(string path, object value)
            {
                EnovaObject = GetEnovaType().GetConstructor(new Type[] { typeof(string), typeof(object) }).Invoke(new object[]{
                        path,
                        value is IObjectBase ? ((IObjectBase)value).EnovaObject : value
                    });

            }
        }

        public class GreaterEqual : Compare
        {
            public override Type GetEnovaType()
            {
                return base.GetEnovaType().GetNestedType("GreaterEqual");
            }

            public GreaterEqual(string path, object value)
            {
                EnovaObject = GetEnovaType().GetConstructor(new Type[] { typeof(string), typeof(object) }).Invoke(new object[]{
                        path,
                        value is IObjectBase ? ((IObjectBase)value).EnovaObject : value
                    });

            }
        }

        public class Less : Compare
        {
            public override Type GetEnovaType()
            {
                return base.GetEnovaType().GetNestedType("Less");
            }

            public Less(string path, object value)
            {
                EnovaObject = GetEnovaType().GetConstructor(new Type[] { typeof(string), typeof(object) }).Invoke(new object[]{
                        path,
                        value is IObjectBase ? ((IObjectBase)value).EnovaObject : value
                    });

            }
        }

        public class LessEqual : Compare
        {
            public override Type GetEnovaType()
            {
                return base.GetEnovaType().GetNestedType("LessEqual");
            }

            public LessEqual(string path, object value)
            {
                EnovaObject = GetEnovaType().GetConstructor(new Type[] { typeof(string), typeof(object) }).Invoke(new object[]{
                        path,
                        value is IObjectBase ? ((IObjectBase)value).EnovaObject : value
                    });

            }
        }


        #endregion
    }
}
