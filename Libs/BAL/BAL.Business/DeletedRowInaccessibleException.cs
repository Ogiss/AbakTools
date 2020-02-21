using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class DeletedRowInaccessibleException : RowException
    {
        // Methods
        public DeletedRowInaccessibleException(IRow row)
            : base(row, "Operacja niedozwolona na skasowanym obiekcie danych")
        {
        }
    }
}
