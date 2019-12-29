using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Business
{
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
public class RequiredAttribute : Attribute
{
    // Methods
    //public static bool Get(MemberInfo mi) => 
    //    Attribute.IsDefined(mi, typeof(RequiredAttribute))
}

}
