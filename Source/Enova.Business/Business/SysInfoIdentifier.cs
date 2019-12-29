using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public enum SysInfoIdentifier
    {
        BusinessIntegrity = 500,
        [Obsolete]
        ConfigVersion = 11,
        ConstraintsNotCreated = 14,
        ConversionInProgress = 13,
        Description1 = 3,
        Description2 = 4,
        Description3 = 5,
        DllsStart = 200,
        DllsStop = 0x12b,
        Guid = 7,
        LocalLicence = 12,
        Name = 2,
        NumerSeryjny = 10,
        Version = 1,
        VersionStart = 100,
        VersionStop = 0xc7
    }
}
