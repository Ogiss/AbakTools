using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;

namespace Enova.Business.Old.Core
{
    public interface IPrintable
    {
        string ReportTitle { get; }
        string ReportPath { get; }
        IEnumerable<ReportDataSource> ReportDataSources { get; }
    }
}
