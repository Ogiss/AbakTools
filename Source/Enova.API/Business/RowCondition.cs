using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Enova.API.Types;

namespace Enova.API.Business
{
    public class RowCondition : API.Types.ObjectBase
    {
        #region Fields

        public static RowCondition Empty;

        #endregion

        #region Properties
        #endregion

        #region Methods

        static RowCondition()
        {
            Empty = new RowCondition();
            Empty.EnovaObject = Empty.GetEnovaType().GetProperty("Empty");
        }

        public override Type GetEnovaType()
        {
            return Type.GetType("Soneta.Business.RowCondition,Soneta.Business");
        }

        public static RowCondition operator &(RowCondition con1, RowCondition con2)
        {
            return new And()
            {
                EnovaObject = Type.GetType("Soneta.Business.RowCondition,Soneta.Business").GetMethod("op_BitwiseAnd", new Type[]{
                    Type.GetType("Soneta.Business.RowCondition,Soneta.Business"),
                    Type.GetType("Soneta.Business.RowCondition,Soneta.Business")
            }).Invoke(null, new object[]{
                con1.EnovaObject,
                con2.EnovaObject
            })
            };
        }

        public override bool Equals(object obj)
        {
            return (bool)CallMethodFull("Equals", new Type[] { typeof(object) }, new object[] { ((IObjectBase)obj).EnovaObject });
        }

        public override int GetHashCode()
        {
            return (int)CallMethod("GetHashCode");
        }

        #endregion

        #region Nested Types

        public class CompoundCondition : RowCondition
        {
        }

        public class And : CompoundCondition
        {
        }

        public class Or : CompoundCondition
        {
        }



        #endregion
    }
}
