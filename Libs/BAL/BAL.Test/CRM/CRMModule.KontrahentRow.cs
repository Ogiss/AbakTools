using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;

namespace BAL.Test.CRM
{
    public partial class CRMModule
    {
        [DefaultProperty("Nazwa")]
        public class KontrahentRow : Row
        {
            public string Kod { get; set; }
            public string Nazwa { get; set; }
            public string NIP { get; set; }
        }
    }
}
