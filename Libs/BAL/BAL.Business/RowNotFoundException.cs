using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class RowNotFoundException : BusException
    {
        // Fields
        public readonly int ID;
        public readonly ITable Table;

        // Methods
        public RowNotFoundException()
            : base("Zapis nieznaleziony")
        {
        }

        public RowNotFoundException(string s)
            : base(s)
        {
        }

        public RowNotFoundException(ITable table, int id)
            : base(string.Format("Zapis o ID " + id + " nieznaleziony w tabeli {1}.\n\nMożliwe zmiany zapisu na innym stanowisku lub w innej sesji programu.", id, (table == null) ? "?" : table.TableName))
        {
            this.Table = table;
            this.ID = id;
        }
    }
}
