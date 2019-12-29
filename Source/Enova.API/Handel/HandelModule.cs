using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Business;

namespace Enova.API.Handel
{
    public interface HandelModule : Business.Module
    {
        #region Properties

        DefDokHandlowych DefDokHandlowych { get; }
        DokHandlowe DokHandlowe { get; }
        PozycjeDokHan PozycjeDokHan { get; }
        DefRelHandlowych DefRelHandlowych { get; }

        #endregion

        #region Methods

        void DrukujDokument(System.Windows.Forms.Form form, Guid guid, string template,
            Printer.Destinations destination = Printer.Destinations.Preview,
            string outputFile = null);

        void ZmienDateDokumentu(int ID, DateTime data);

        


        #endregion
    }
}
