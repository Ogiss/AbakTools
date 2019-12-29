using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: BAL.Business.DataContext(typeof(Enova.API.Handel.DokumentHandlowy), typeof(Enova.Forms.Handel.DokHandlowyContext))]

namespace Enova.Forms.Handel
{
    public class DokHandlowyContext : BAL.Business.DataContext
    {

        #region Properties

        public Enova.API.Handel.DokumentHandlowy Dokument
        {
            get
            {
                return GetData() as Enova.API.Handel.DokumentHandlowy;
            }
        }

        #endregion

        #region Methods

        public DokHandlowyContext()
            : base(null, true)
        {
        }

        public override string GetTitle()
        {
            /*
            var dok = this.GetData() as Enova.API.Handel.DokumentHandlowy;
            if (dok != null)
                return "Dokument handlowy: " + dok.NumerPelny;
             */
            if(Dokument != null)
                return "Dokument handlowy: " + Dokument.NumerPelny;
            return base.GetTitle();
        }

        public override Type GetDataType()
        {
            return typeof(Enova.API.Handel.DokumentHandlowy);
        }

        public virtual void InitPozycjeDokGrid(PozycjeDokHanGrid grid)
        {

        }

        #endregion
    }
}
