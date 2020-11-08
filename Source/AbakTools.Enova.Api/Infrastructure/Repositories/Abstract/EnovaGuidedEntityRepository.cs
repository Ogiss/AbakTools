using System;
using AbakTools.EnovaApi.Domain.Common;
using AbakTools.EnovaApi.Infrastructure.Api;

namespace AbakTools.EnovaApi.Infrastructure.Repositories.Abstract
{
    internal class EnovaGuidedEntityRepository<TEntity> : EnovaEntityRepositoryBase<TEntity>, IEnovaGuidedEntityRepository<TEntity>
    {
        public EnovaGuidedEntityRepository(IEnovaApiClient apiClient) : base(apiClient)
        {
        }

        public TEntity Get(Guid guid)
        {
           return ApiClient.GetValue<TEntity>(ResourceName, guid);
        }
    }
}
