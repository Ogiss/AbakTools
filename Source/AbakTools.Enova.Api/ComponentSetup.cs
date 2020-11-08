using AbakTools.EnovaApi.Domain.CommercialDocument;
using AbakTools.EnovaApi.Infrastructure.Api;
using AbakTools.EnovaApi.Infrastructure.Repositories;
using AbakTools.EnovaApi.Service;
using AbakTools.Framework;
using Unity;

namespace AbakTools.EnovaApi
{
    public class ComponentSetup : IComponentSetup
    {
        public void Setup(IUnityContainer container)
        {
            container.RegisterType<IEnovaApiClient, EnovaApiClient>();

            container.RegisterType<ICommercialDocumentRepository, CommercialDocumentRepository>();

            container.RegisterType<IEnovaService, EnovaService>();
        }
    }
}
