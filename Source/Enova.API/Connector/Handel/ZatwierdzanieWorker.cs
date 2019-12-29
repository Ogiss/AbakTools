using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//[assembly: TypeMap("Soneta.Handel.ZatwierdzanieWorker, Soneta.Handel", typeof(Enova.API.Handel.IZatwierdzanieWorker), typeof(Enova.API.Connector.Handel.ZatwierdzanieWorker))]

namespace Enova.API.Connector.Handel
{
    internal class ZatwierdzanieWorker : API.Types.ObjectBase, API.Handel.ZatwierdzanieWorker
    {
        public object Zatwierdź()
        {
            return CallMethod("Zatwierdź");
        }

        public object ZatwierdźLista()
        {
            return CallMethod("ZatwierdźLista");
        }

        public API.Handel.DokumentHandlowy Dokument
        {
            get
            {
                return EnovaHelper.FromEnova<API.Handel.DokumentHandlowy>(GetValue("Dokument"));
            }
            set
            {
                SetValue("Dokument", EnovaHelper.ToEnova(value));
            }
        }

        public API.Handel.DokumentHandlowy[] Dokumenty
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
