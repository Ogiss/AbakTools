using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbakTools.Business
{
    [Flags]
    public enum OperatorPrawaDostepu
    {
        Brak = 0,
        Przedstawiciel = 0x0001,
        Magazynier = 0x0002,
        Pakowacz = 0x0004,
        Kierownik = 0x0008,
        SMS = 0x0010,
        Administrator = 0x00FF,
        SuperAdmin = 0xFFFF
    }
}
