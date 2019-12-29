using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Enova.Business.Old;

namespace Enova.Old.Handel
{
    public class KontrolerRelacjiInfo : SubRow
    {
        #region Properties

        [Description("Określa, czy kontroler relacji powinien przeliczać pozycje relacji handlwych.")]
        public bool PrzeliczajPozycjeRelacji
        {
            get
            {
                return GetFieldValue<bool>("PrzeliczajPozycjeRelacji");
            }
            set
            {
                this.SetFieldValue("PrzeliczajPozycjeRelacji", value);
            }
        }

        [Description("Określa, czy kontroler relacji jest włączony.")]
        public bool Disabled
        {
            get
            {
                return this.GetFieldValue<bool>("Disabled");
            }
            set
            {
                this.SetFieldValue("Disabled", value);
            }
        }

        #endregion
    }
}
