using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL.Business;
using BAL.Forms;

[assembly: AppService(typeof(IGridService),typeof(AbakTools.Handel.Forms.HandelGridService))]

namespace AbakTools.Handel.Forms
{
    public class HandelGridService : GridService ,IGridService
    {
        public override string[] GetAvailableKeys()
        {
            return new string[]{
                "ZamowieniaView",
                "ReklamacjeView",
                "DostawyView",
                "TowaryAtrybutyListaView",
                "MagazynAVView",
                "ZamowienieMagazynAVView"
            };
        }

        public override Type GetDefaultGridTemplateType(string key)
        {
            switch (key)
            {
                case "ZamowieniaView":
                    return typeof(AbakTools.Zamowienia.Forms.ZamowieniaViewGridTemplate);
                case "ReklamacjeView":
                    return typeof(AbakTools.Handel.Forms.ReklamacjeViewGridTemplate);
                case "DostawyView":
                    return typeof(AbakTools.Core.Forms.DokumentZHistoriaGridTemplate);
                case "ZamowienieMagazynAVView":
                case "TowaryAtrybutyListaView":
                case "MagazynAVView":
                    return typeof(AbakTools.Towary.Forms.MagazynAVViewGridTemplate);
            }
            return base.GetDefaultGridTemplateType(key);
        }
    }
}
