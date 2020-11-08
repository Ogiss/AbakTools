using System;
using AbakTools.EnovaApi.Domain.Common;
using AbakTools.EnovaApi.Infrastructure.Api;

namespace AbakTools.EnovaApi.Infrastructure.Repositories.Abstract
{
    internal abstract class EnovaEntityRepositoryBase<TEntity> : EnovaRepositoryBase, IEnovaRepository<TEntity>
    {
        protected virtual string ResourceName => throw new NotImplementedException();

        protected EnovaEntityRepositoryBase(IEnovaApiClient apiClient) : base(apiClient)
        {
        }
    }
}
