using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.App
{
    public class IncorrectPasswordException : ArgumentException
    {
        // Fields
        public readonly string Database;
        public readonly bool IsAdministrator;
        public readonly string Operator;
        public readonly string Prompt;

        // Methods
        internal IncorrectPasswordException(string op, string prompt, bool isAdministrator, Database db)
            : base("Niepoprawne hasło dostępu operatora '" + op + "' w bazie danych '" + db.Name + "'")
        {
            this.Operator = op;
            this.Prompt = prompt;
            this.IsAdministrator = isAdministrator;
            this.Database = db.Name;
        }
    }

}
