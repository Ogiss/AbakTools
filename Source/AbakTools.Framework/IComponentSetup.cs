using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace AbakTools.Framework
{
    public interface IComponentSetup
    {
        void Setup(IUnityContainer container);
    }
}
