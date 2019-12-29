using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old
{
    public class AccessDeniedException : AccessException
    {
        // Methods
        public AccessDeniedException(IRow row)
            : base(row, "Brak praw dostępu do danych.\r\nTyp zapisu: " + row.GetType())
        {
        }
    }
}
