using System;
using System.Collections.Generic;
using EnovaApi.Models.CommercialDocument;

namespace AbakTools.EnovaApi.Service
{
    internal partial class EnovaService
    {
        public void PrintDocument(Guid guid)
        {
            var document = _commercialDocumentRepository.Get(guid);

            if (document != null)
            {
                _documentPrinterService.PrintDocument(
                    GetDocumentTemplatePath(document),
                    GetDataSources(document),
                    GetParameters(document));
            }
        }

        public byte[] ExportDocumentToPdf(CommercialDocument document)
        {
            if (document != null)
            {
                return _documentPrinterService.ExportToPdf(
                    GetDocumentTemplatePath(document),
                    GetDataSources(document),
                    GetParameters(document));
            }

            return null;
        }

        public byte[] ExportDocumentToPdf(Guid guid)
        {
            return ExportDocumentToPdf(_commercialDocumentRepository.Get(guid));
        }

        private string GetDocumentTemplatePath(CommercialDocument document)
        {
            switch (document.Definition.Category)
            {
                case DocumentCategory.Invoice:
                    return "Reports/Invoice.rdlc";

                case DocumentCategory.InvoiceCorrection:
                    return "Reports/InvoiceCorrection.rdlc";

                default:
                    throw new NotImplementedException();
            }
        }

        private Dictionary<string, object> GetDataSources(CommercialDocument document)
        {
            return new Dictionary<string, object>
            {
                {"Document", new[] {document}},
                {"Definition", new [] {document.Definition}},
                {"Customer", new[] {document.Customer}},
                {"CustomerAddress", new[] {document.Customer.MainAddress}},
                {"Rows", document.Rows},
                {"Taxes", document.TaxesSummary},
                {"Payments", document.PaymentSummary}
            };
        }

        private Dictionary<string, object> GetParameters(CommercialDocument document)
        {
            return null;
        }
    }
}
