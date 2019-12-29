using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbakTools.Configuration;
using AbakTools.Framework;
using Unity;
using Enova.API;

namespace EnovaToolsExplorer.Setups
{
    class EnovaApiSetup : IComponentSetup
    {
        public void Setup(IUnityContainer container)
        {
            var logger = container.Resolve<AbakTools.Framework.Logging.ILogger>(AbakTools.Framework.Logging.LogNames.EnovaApi);
            container.RegisterInstance<ILogger>(new EnovaApiLogger(logger));

            Enova.API.EnovaService.Initialize(AppSettings.EnovaPath, container.Resolve<ILogger>());
        }

        public class EnovaApiLogger : MarshalByRefObject, ILogger
        {
            public AbakTools.Framework.Logging.ILogger Logger { private get; set; }

            public EnovaApiLogger(AbakTools.Framework.Logging.ILogger logger)
            {
                Logger = logger;
            }

            public void Debug(object message)
            {
                Logger.Debug(message);
            }

            public void Error(object message)
            {
                Logger.Error(message);
            }

            public void Fatal(object message)
            {
                Logger.Fatal(message);
            }

            public void Info(object message)
            {
                Logger.Info(message);
            }

            public void Warning(object message)
            {
                Logger.Warning(message);
            }
        }
    }
}
