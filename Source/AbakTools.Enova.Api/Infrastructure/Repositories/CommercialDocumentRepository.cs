using AbakTools.EnovaApi.Domain.CommercialDocument;
using AbakTools.EnovaApi.Infrastructure.Api;
using AbakTools.EnovaApi.Infrastructure.Repositories.Abstract;
using EnovaApi.Models.CommercialDocument;

namespace AbakTools.EnovaApi.Infrastructure.Repositories
{
    internal class CommercialDocumentRepository : EnovaGuidedEntityRepository<CommercialDocument>, ICommercialDocumentRepository
    {
        protected override string ResourceName => global::Enova.Api.ResourcesNames.CommercialDocuments;

        public CommercialDocumentRepository(IEnovaApiClient apiClient) : base(apiClient)
        {
        }
    }
}
