using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WinForms;

namespace AbakTools.Printer.DocumentPrintService
{
    class DocumentPrinterService : IDocumentPrinterService
    {
        public void PrintDocument(string templatePath, IDictionary<string, object> dataSources, IDictionary<string, object> parameters = null)
        {
            var reportDataSources = dataSources.Select(x => new ReportDataSource(x.Key, x.Value)).ToList();
            var reportParameters = parameters?.Select(x => new ReportParameter(x.Key, x.Value?.ToString())).ToList();

            var form = new ReportForm("AbakTools", templatePath, reportDataSources, reportParameters);

            form.Show();
        }
    }
}