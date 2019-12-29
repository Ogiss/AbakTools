using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public interface IDokumentZHistoria : IRow
    {
        ICollection<HistoriaDokumentu> Historia { get; }
        HistoriaDokumentu OstatniaHistoriaDokumentu { get; }
        HistoriaDokumentu ZmienStatus(StatusDokumentu status, string opis = "");
    }
}
