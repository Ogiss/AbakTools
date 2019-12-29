using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;

namespace Enova.API.Core
{
    public interface IDokumentHandlowy : IDokument, Row, ISessionable
    {
        // Methods
        Date WyliczDateZaplaty(KierunekPlatnosci kierunek);

        // Properties
        IKontrahent Kontrahent { get; }
    }




}
