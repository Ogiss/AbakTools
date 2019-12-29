using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbakTools.Framework;
using AbakTools.Framework.Logging;
using AbakTools.Infrastructure.Logging;
using Unity;

namespace AbakTools.Infrastructure
{
    public class ComponentSetup : IComponentSetup
    {
        public void Setup(IUnityContainer container)
        {
            // Logging

            container.RegisterInstance<ILogger>(LogNames.Application, new Logger(LogNames.Application));
            container.RegisterInstance<ILogger>(LogNames.EnovaApi, new Logger(LogNames.EnovaApi));
            LogProvider.UnityContainer = container;
        }
    }
}
