using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.API.Types;
using Enova.API.Business;

namespace Enova.API.Kasa
{
    public interface IRozliczalnyCy : IRozliczalny, IRow, ISessionable
    {
        // Methods
        Currency RozliczonoKsiDoDnia(Date data);

        // Properties
        double Kurs { get; }
        Currency KwotaKsiegi { get; }
        //TabelaKursowa TabelaKursowa { get; }
    }
}
