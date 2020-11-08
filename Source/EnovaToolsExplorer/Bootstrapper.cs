using Unity;
using AbakTools.Framework;

namespace EnovaToolsExplorer
{
    class Bootstrapper
    {
        IUnityContainer unityContainer;

        IComponentSetup[] componentSetups = new IComponentSetup[]
        {
            new AbakTools.Configuration.ComponentSetup(),
            new AbakTools.Infrastructure.ComponentSetup(),
            new AbakTools.Printer.ComponentSetup(),
            new AbakTools.EnovaApi.ComponentSetup(), 
            new Setups.EnovaApiSetup()
        };
        
        public Bootstrapper()
        {
            unityContainer = new UnityContainer();
            DependencyProvider.UnityContainer = unityContainer;
        }

        public void ConfigureComponents()
        {
            foreach(var setup in componentSetups)
            {
                setup.Setup(unityContainer);
            }
        }

        public void RunApplication()
        {

        }
    }
}
