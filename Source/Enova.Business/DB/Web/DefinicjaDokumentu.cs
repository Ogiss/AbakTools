using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class DefinicjaDokumentu : IDefinicjaDokumentu
    {
        #region Fields

        internal static Guid ZamowienieGuid = new Guid("00000000-0011-0002-0001-000000000000");
        internal static Guid ZwrotGuid = new Guid("00000000-0011-0002-0002-000000000000");
        internal static Guid ReklamacjaGuid = new Guid("00000000-0011-0002-0003-000000000000");

        #endregion
    }
}
