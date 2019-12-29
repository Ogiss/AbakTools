using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.DB;

namespace Enova.Old.Handel
{
    public class DefRelacjiZ : SubRow
    {
        #region Properties

        public string Nazwa
        {
            get { return GetFieldValue<string>("Nazwa"); }
            set { SetFieldValue("Nazwa", value); }
        }

        public bool NowyDokument
        {
            get { return GetFieldValue<bool>("NowyDokument"); }
            set { SetFieldValue("NowyDokument", value); }
        }

        #endregion

        #region Methods

        public DefRelacjiZ(IRow parent, string name) : base(parent, name) { }

        public DefRelacjiZ() : this(null, null) { }

        #endregion
    }
}
