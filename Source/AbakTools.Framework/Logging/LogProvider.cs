using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AbakTools.Framework.Logging
{
    public class LogProvider
    {
        public static IUnityContainer UnityContainer { get; set; }

        public static ILogger GetLogger(string name)
        {
            return UnityContainer.Resolve<ILogger>(name);
        }
    }
}
