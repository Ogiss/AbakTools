using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Types;
using BAL.Business;

namespace BAL.Test.Core
{
    public partial class CoreModule
    {
        public class AdresRow : Row, IHostedRow
        {
            [Hidden]
            public Host Host { get; set; }
            public TypAdresu Typ { get; set; }
            public string Ulica { get; set; }
            [Caption("Nr domu")]
            public string NrDomu { get; set; }
            [Caption("Nr lokalu")]
            public string NrLokalu { get; set; }
            public string Miejscowosc { get; set; }
            [Caption("Kod pocztowy")]
            public string KodPocztowy { get; set; }

            public AdresRow()
            {
                this.Host = new Host();
            }
        }
    }
}
