using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.API.Types
{
    public interface IObjectBase
    {
        object EnovaObject { get; set; }
        object GetValue(string name, object[] indexes);
        object GetObjValue(object obj, string name, Type[] types = null, object[] index = null, Type fromType = null);
        void SetValue(string name, object value);
        void SetObjValue(object obj, string name, object value);
        object CallMethod(string name, params object[] args);
        object CallObjMethod(object obj, string name, Type[] paramTypes, object[] parameters);
        string ToString();
        bool Is<T>();
        T As<T>();
        T FromEnova<T>(string name, Type fromType = null);
    }
}
