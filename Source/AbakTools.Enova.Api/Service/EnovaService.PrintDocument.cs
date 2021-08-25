using System;
using System.Collections.Generic;
using AbakTools.EnovaApi.Domain.CommercialDocument;
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
                    CheckIsInvoiceCorrectionSupported(document);
                    return "Reports/InvoiceCorrection.rdlc";

                default:
                    throw new NotImplementedException();
            }
        }

        private void CheckIsInvoiceCorrectionSupported(CommercialDocument document)
        {
            foreach (var row in document.Rows)
            {
                if (row.CorrectionType != PositionCorrectionType.None)
                {
                    if (!(row.CorrectionType == PositionCorrectionType.Quantity || row.CorrectionType == PositionCorrectionType.Return))
                    {
                        throw new NotSupportedException("Faktura korygująca z korektą ceny nie jest obsługiwana w wydrukach Tools-ów. Musisz wydrukować ją w Enova-ej.");
                    }
                }
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
                {"Payments", document.PaymentSummary},
                {"CorrectionInfo", GetCorrectionInfo(document) },
                { "CorrectedDocument", document.CorrectedDocument != null ? new[] { document.CorrectedDocument } : Array.Empty<CommercialDocument>() }
            };
        }

        private CorrectionInfo[] GetCorrectionInfo(CommercialDocument document)
        {
            if (document.Definition.Category == DocumentCategory.InvoiceCorrection)
            {
                var correctionInfo = new CorrectionInfo();
                var correctionDiff = Math.Abs(document.TotalValueWithoutTax);
                var taxCorrectionDiff = Math.Abs(document.TotalValueTax);

                correctionInfo.CorrectionTitle = "Zwrot towaru";

                if (document.TotalValueWithoutTax < 0)
                {
                    correctionInfo.TotalValueCorrectionInfo = $"Kwota zmniejszenia wartości bez podatku: {correctionDiff:N2} PLN";
                    correctionInfo.TotalTaxCorrectionInfo = $"Kwota zmniejszenia podatku należnego: {taxCorrectionDiff:N2} PLN";
                }
                else if (document.TotalValueWithoutTax > 0)
                {
                    correctionInfo.TotalValueCorrectionInfo = $"Kwota zwiększenia wartości bez podatku: {correctionDiff:N2} PLN";
                    correctionInfo.TotalTaxCorrectionInfo = $"Kwota zwiększenia podatku należnego: {taxCorrectionDiff:N2} PLN";
                }

                return new[] { correctionInfo };
            }

            return null;
        }

        private Dictionary<string, object> GetParameters(CommercialDocument document)
        {
            return null;
        }
    }
}
