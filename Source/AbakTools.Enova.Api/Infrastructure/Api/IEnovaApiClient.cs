using System;
using System.Threading.Tasks;

namespace AbakTools.EnovaApi.Infrastructure.Api
{
    internal interface IEnovaApiClient
    {
        TResult GetValue<TResult>(string resourceName, Guid objectGuid);
        Task<TResult> GetValueAsync<TResult>(string resourceName, Guid objectGuid);
    }
}
