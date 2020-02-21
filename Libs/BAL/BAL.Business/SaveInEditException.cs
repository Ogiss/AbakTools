using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class SaveInEditException : BusException
    {
        // Methods
        public SaveInEditException()
            : base("Niedozwolona operacja zapisu danych w trakcie edycji")
        {
        }
    }
}
