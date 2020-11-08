using System.Collections.Generic;

namespace AbakTools.Printer.DocumentPrintService
{
    public interface IDocumentPrinterService
    {
        void PrintDocument(string templatePath, IDictionary<string, object> dataSources, IDictionary<string, object> parameters = null);
    }
}
