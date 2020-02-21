using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Business
{
    public class ModuleException : ExceptionBase
    {
        #region Fields

        private string moduleName;

        #endregion

        #region Properties

        public string ModuleName
        {
            get { return moduleName; }
        }


        #endregion

        public ModuleException(string moduleName, string message)
            : base("Module: " + moduleName + " Message:" + message)
        {
            this.moduleName = moduleName;
        }

    }
}
