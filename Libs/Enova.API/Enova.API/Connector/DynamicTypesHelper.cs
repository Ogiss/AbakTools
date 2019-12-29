using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Enova.API.Connector
{
    public static class DynamicTypesHelper
    {

        private const string assemblyName = "DynamicConnectorAssembly";

        private static AssemblyBuilder assemblyBuilder;
        private static ModuleBuilder moduleBuilder;

        public static AssemblyBuilder AssemblyBuilder
        {
            get
            {
                if (assemblyBuilder == null)
                    assemblyBuilder = GetAssemblyBuilder(assemblyName);
                return assemblyBuilder;
            }
        }

        public static ModuleBuilder ModuleBuilder
        {
            get
            {
                if (moduleBuilder == null)
                    moduleBuilder = AssemblyBuilder.DefineDynamicModule(assemblyName);
                return moduleBuilder;
            }
        }

        public static AssemblyBuilder GetAssemblyBuilder(string assemblyName)
        {
            AssemblyName aname = new AssemblyName(assemblyName);
            AppDomain currentDomain = AppDomain.CurrentDomain; // Thread.GetDomain();
            AssemblyBuilder builder = currentDomain.DefineDynamicAssembly(aname,
                                       AssemblyBuilderAccess.Run);
            return builder;
        }

        public static TypeBuilder GetTypeBuilder(string className, Type baseType,  bool createdefaultConstructor = true)
        {
            TypeBuilder builder = ModuleBuilder.DefineType(className, TypeAttributes.Class | TypeAttributes.Public, baseType);
            if(createdefaultConstructor)
            {
                var baseConstructor = baseType.GetConstructor(new Type[0]);
                var constructor = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
                var il = constructor.GetILGenerator();

                //Generate IL code
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Call, baseConstructor);
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ret);
            }
            return builder;
        }


    }
}
