using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class BadRegistrationModuleException : ModuleException
    {
        public BadRegistrationModuleException(string moduleName) : base(moduleName, "Nieprawidłowo zarejestrowany moduł") { }
    }
}
