using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Services
{
    public static class DokumentHandlowyExtensions
    {
        public static string Przewoznik(this API.Handel.DokumentHandlowy dh)
        {
            return (string)dh.Features["PRZEWOŻNIK"];
        }
    }
}
