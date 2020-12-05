using EnovaApi.Models.CommercialDocument;
using System;

namespace AbakTools.EnovaApi
{
    public interface IEnovaService
    {
        void PrintDocument(Guid guid);
        byte[] ExportDocumentToPdf(Guid guid);
        byte[] ExportDocumentToPdf(CommercialDocument document);
    }
}
