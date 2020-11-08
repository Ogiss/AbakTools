using System;

namespace AbakTools.EnovaApi.Domain.Common
{
    public  interface IEnovaGuidedEntityRepository<TEntity>
    {
        TEntity Get(Guid guid);
    }
}
