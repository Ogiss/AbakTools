using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace AbakTools.Printer
{
    public partial class ReportForm : Form
    {

        public ReportViewer ReportViewer
        {
            get
            {
                return this.reportViewer;
            }
        }

        public LocalReport LocalReport
        {
            get { return this.ReportViewer.LocalReport; }
        }

        public ReportDataSourceCollection DataSources
        {
            get
            {
                return this.LocalReport.DataSources;
            }
        }

        public string ReportPath
        {
            get { return this.LocalReport.ReportPath; }
            set { this.LocalReport.ReportPath = value; }
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public ReportForm()
        {
            InitializeComponent();
        }


        public ReportForm(string title, string reportPath, IEnumerable<ReportDataSource> dataSources)
        {
            InitializeComponent();
            SetReport(title, reportPath, dataSources);
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.Percent;
            ReportViewer.ZoomPercent = 100;
        }

        public ReportForm(string title, System.IO.Stream stream, IEnumerable<ReportDataSource> dataSources)
        {
            InitializeComponent();
            SetReport(title, stream, dataSources);
            ReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            ReportViewer.ZoomMode = ZoomMode.Percent;
            ReportViewer.ZoomPercent = 100;
        }


        public ReportForm(string reportPath, string dataSourceName, object dataSource, IEnumerable<ReportParameter> parameters)
        {
            InitializeComponent();
            this.ReportViewer.LocalReport.ReportPath = reportPath;
            ReportDataSource reportDataSource = this.ReportViewer.LocalReport.DataSources[dataSourceName];
            if (reportDataSource != null)
            {
                reportDataSource.Value = dataSource;
            }
            else
            {
                this.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSourceName, dataSource));
            }
            if (parameters != null)
                this.ReportViewer.LocalReport.SetParameters(parameters);
        }


        public void SetReport(string title, string reportPath, IEnumerable<ReportDataSource> dataSources)
        {
            this.Text = title;
            this.ReportViewer.LocalReport.ReportPath = reportPath;
            this.ReportViewer.LocalReport.DataSources.Clear();

            foreach (var ds in dataSources)
            {
                this.ReportViewer.LocalReport.DataSources.Add(ds);
            }
        }

        public void SetReport(string title, System.IO.Stream stream, IEnumerable<ReportDataSource> dataSources)
        {
            this.Text = title;
            //this.ReportViewer.LocalReport.ReportPath = reportPath;
            this.ReportViewer.LocalReport.LoadReportDefinition(stream);
            this.ReportViewer.LocalReport.DataSources.Clear();

            foreach (var ds in dataSources)
            {
                this.ReportViewer.LocalReport.DataSources.Add(ds);
            }
        }


        public void AddDataSource(string name, object data)
        {
            this.DataSources.Add(new ReportDataSource(name, data));
        }


        private void ReportForm_Load(object sender, EventArgs e)
        {
            //reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //reportViewer.ZoomMode = ZoomMode.PageWidth; //ZoomMode.Percent;
            //reportViewer.ZoomPercent = 100;
            reportViewer.RefreshReport();
        }

        private void ReportForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == false && e.Control == false && e.Shift == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                }
            }
        }
    }
}
