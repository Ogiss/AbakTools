using AbakTools.EnovaApi.Domain.CommercialDocument;
using AbakTools.Printer.DocumentPrintService;

namespace AbakTools.EnovaApi.Service
{
    internal partial class EnovaService : IEnovaService
    {
        private ICommercialDocumentRepository _commercialDocumentRepository;
        private IDocumentPrinterService _documentPrinterService;

        public EnovaService(
            ICommercialDocumentRepository commercialDocumentRepository,
            IDocumentPrinterService documentPrinterService)
        {
            _commercialDocumentRepository = commercialDocumentRepository;
            _documentPrinterService = documentPrinterService;
        }
    }
}
