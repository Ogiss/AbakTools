using System.Collections.Generic;
using System.IO;
using System.Linq;
using AbakTools.Framework.Utils.Extensions;
using Microsoft.Reporting.WinForms;

namespace AbakTools.Printer.DocumentPrintService
{
    class DocumentPrinterService : IDocumentPrinterService
    {
        public void PrintDocument(string templatePath, IDictionary<string, object> dataSources, IDictionary<string, object> parameters = null)
        {
            var form = new ReportForm(
                "AbakTools",
                templatePath,
                PrepareDocumentDataSources(dataSources),
                PreapeDocymentParameters(parameters));

            form.Show();
        }

        public byte[] ExportToPdf(string templatePath, IDictionary<string, object> dataSources, IDictionary<string, object> parameters = null)
        {
            var reportDataSources = PrepareDocumentDataSources(dataSources);
            var reportParameters = PreapeDocymentParameters(parameters);
            var localReport = new LocalReport();
            
            localReport.ReportPath = templatePath;

            localReport.DataSources.Clear();
            reportDataSources.ForEach(localReport.DataSources.Add);

            if(reportParameters != null)
            {
                localReport.SetParameters(reportParameters);
            }

            return localReport.Render("PDF", GetRenderDeviceInfo());
        }

        private string GetRenderDeviceInfo()
        {
            return "<DeviceInfo>" +
          "  <OutputFormat>PDF</OutputFormat>" +
          "  <PageWidth>21cm</PageWidth>" +
          "  <PageHeight>29.7cm</PageHeight>" +
          "  <MarginTop>0.5cm</MarginTop>" +
          "  <MarginLeft>0.8cm</MarginLeft>" +
          "  <MarginRight>0.8cm</MarginRight>" +
          "  <MarginBottom>0.5cm</MarginBottom>" +
          "</DeviceInfo>";
        }

        private IEnumerable<ReportDataSource> PrepareDocumentDataSources(IDictionary<string, object> dataSources)
        {
            return dataSources.Select(x => new ReportDataSource(x.Key, x.Value)).ToList();
        }

        private IEnumerable<ReportParameter> PreapeDocymentParameters(IDictionary<string, object> parameters)
        {
            return parameters?.Select(x => new ReportParameter(x.Key, x.Value?.ToString())).ToList();
        }
    }
}