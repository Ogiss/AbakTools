using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;

namespace AbakTools.Printer
{
    public class PrintingService : IDisposable
    {
        private bool m_disposed = false;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private LocalReport report;
        private double pageWidth;
        private double pageHeight;
        private double marginTop;
        private double marginLeft;
        private double marginRight;
        private double marginBottom;

        public PrintingService()
        {
            report = new LocalReport();
            pageWidth = 210;
            pageHeight = 297;
            marginTop = 0;
            marginBottom = 0;
            marginLeft = 0;
            marginRight = 0;
        }

        private Stream createStream(string name, string ext, Encoding enc, string mime, bool willSeek)
        {
            string cd = Directory.GetCurrentDirectory();
            if (!Directory.Exists(cd + "\\tmp"))
                Directory.CreateDirectory(cd + "\\tmp");
            Stream s = new FileStream(cd + "\\tmp\\" + name + "." + ext, FileMode.Create);
            m_streams.Add(s);
            return s;
        }

        private void export()
        {
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", DeviceInfo, createStream, out warnings);
            foreach (Stream s in m_streams)
                s.Position = 0;
        }

        public void Export(string format, string fileName)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = report.Render(format, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        private string DeviceInfo
        {
            get
            {
                return
                    "<DeviceInfo>" +
                    " <OutputFormat>EMF</OutputFormat>" +
                    " <PageWidth>" + pageWidth.ToString() + "mm</PageWidth>" +
                    " <PageHeight>" + pageHeight.ToString() + "mm</PageHeight>" +
                    " <MarginTop>" + marginTop.ToString() + "mm</MarginTop>" +
                    " <MarginLeft>" + marginLeft.ToString() + "mm</MarginLeft>" +
                    " <MarginRight>" + marginRight.ToString() + "mm</MarginRight>" +
                    " <MarginBottom>" + marginBottom.ToString() + "mm</MarginBottom>" +
                    "</DeviceInfo>";
            }
        }

        private void printPage(object sender, PrintPageEventArgs e)
        {
            Metafile m = new Metafile(m_streams[m_currentPageIndex]);
            e.Graphics.DrawImage(m, e.PageBounds);
            m_currentPageIndex++;
            e.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Print()
        {
            this.Print(null);
        }

        public void Print(string printerName)
        {
            if (report != null)
            {
                export();
                m_currentPageIndex = 0;
                PrintDocument pd = new PrintDocument();
                if (!string.IsNullOrEmpty(printerName))
                {
                    pd.PrinterSettings.PrinterName = printerName;
                    if (!pd.PrinterSettings.IsValid)
                    {
                        MessageBox.Show(String.Format("Nie mogę znaleść dukarki \"{0}\".", printerName), "EnovaTools", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                pd.PrintPage += new PrintPageEventHandler(printPage);
                pd.Print();
            }

        }

        public string ReportPath
        {
            get
            {
                return report.ReportPath;
            }
            set
            {
                report.ReportPath = value;
            }
        }

        public ReportDataSourceCollection DataSources
        {
            get
            {
                return report.DataSources;
            }
        }

        public void SetParameters(ReportParameter parameter)
        {
            report.SetParameters(parameter);
        }

        public void SetParameters(IEnumerable<ReportParameter> parameters)
        {
            report.SetParameters(parameters);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool calledByUser)
        {
            if (!m_disposed)
            {
                if (m_streams != null)
                {
                    foreach (Stream s in m_streams)
                        s.Close();
                    m_streams = null;
                }
                m_disposed = true;
            }
        }

        ~PrintingService()
        {
            Dispose(false);
        }

    }
}
