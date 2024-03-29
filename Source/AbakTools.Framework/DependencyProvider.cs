﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AbakTools.Framework
{
    public class DependencyProvider
    {
        public static IUnityContainer UnityContainer { get; set; }

        public static T Resolve<T>() where T : class
        {
            return UnityContainer.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return UnityContainer.Resolve(type);
        }

        public static IEnumerable<T> ResolveAll<T>() where T : class
        {
            return UnityContainer.ResolveAll<T>();
        }
    }
}
