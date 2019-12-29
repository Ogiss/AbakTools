using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old.App;

namespace Enova.Business.Old
{
    public class OperatorNotFoundException : ArgumentException
    {
        // Fields
        public readonly string Database;
        public readonly string Operator;

        // Methods
        public OperatorNotFoundException(string op, Database db)
            : base("Operator '" + op + "' nieznaleziony w bazie danych '" + db.Name + "'")
        {
            this.Operator = op;
            this.Database = db.Name;
        }
    }

}
