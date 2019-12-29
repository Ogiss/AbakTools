using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using AbakTools.Framework;

namespace AbakTools.Configuration
{
    public class ComponentSetup : IComponentSetup
    {
        public void Setup(IUnityContainer container)
        {
            AppSettings.Load();
        }
    }
}
