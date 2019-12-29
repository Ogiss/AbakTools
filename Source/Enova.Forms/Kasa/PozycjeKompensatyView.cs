using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Kasa
{
    public class PozycjeKompensatyView : PozDokRozliczView
    {

        #region Methods

        //public PozycjeKompensatyView(string key, API.Kasa.IKompensata kompensata)
        public PozycjeKompensatyView(string key, API.Kasa.Kompensata kompensata)
            : base(key, kompensata)
        {
        }

        //public PozycjeKompensatyView(API.Kasa.IKompensata kompensata) : this("PozycjeKompensatyView", kompensata) { }
        public PozycjeKompensatyView(API.Kasa.Kompensata kompensata) : this("PozycjeKompensatyView", kompensata) { }

        public override Type GetDataType()
        {
            //return typeof(API.Kasa.IKompensataPozycja);
            return typeof(API.Kasa.KompensataPozycja);
        }

        public override string GetDefaultXmlDefinition()
        {
            return Properties.Resources.PozycjeKompensatyView_grid;
        }


        #endregion
    }
}
