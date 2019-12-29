using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace AbakTools.Forms
{
    public partial class ReportForm : Form
    {

        public ReportForm()
        {
            InitializeComponent();
        }

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

        public void AddDataSource(string name, object data)
        {
            this.DataSources.Add(new ReportDataSource(name, data));
        }


        private void ReportForm_Load(object sender, EventArgs e)
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.PageWidth; //ZoomMode.Percent;
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
