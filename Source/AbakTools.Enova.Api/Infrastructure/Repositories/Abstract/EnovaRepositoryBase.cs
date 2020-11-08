using AbakTools.EnovaApi.Infrastructure.Api;

namespace AbakTools.EnovaApi.Infrastructure.Repositories.Abstract
{
    internal abstract class EnovaRepositoryBase
    {
        protected IEnovaApiClient ApiClient { get; private set; }

        protected EnovaRepositoryBase(IEnovaApiClient apiClient)
        {
            ApiClient = apiClient;
        }
    }
}
