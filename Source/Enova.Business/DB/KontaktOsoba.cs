using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Enova.Business.Old.Core;

namespace Enova.Business.Old.DB
{
    public partial class KontaktOsoba
    {
        public KontaktOsoba()
        {
            this.Guid = Guid.NewGuid();
            this.Imie = string.Empty;
            this.Nazwisko = string.Empty;
            this.Nieaktualny = false;
            this.KontaktEMAIL = string.Empty;
            this.KontaktSkrytkaPocztowa = string.Empty;
            this.KontaktTelefonKomorkowy = string.Empty;
            this.KontaktWWW = string.Empty;
            this.KontrahentID = 0;
            this.KontrahentType = "Kontrahenci";
            this.Stamp = BitConverter.GetBytes(DateTime.Now.Ticks);
            this.Stanowisko = string.Empty;
            this.Uwagi = string.Empty;
        }

        private object kontrahent = null;
        public object Kontrahent
        {
            get
            {
                if (kontrahent == null)
                {
                    if (KontrahentType == "Kontrahenci")
                        kontrahent = ContextManager.DataContext.Kontrahenci.Where(k => k.ID == KontrahentID).FirstOrDefault();
                }
                return kontrahent;
            }
            set
            {
                if (value != null)
                {
                    if (value is Kontrahent)
                    {
                        KontrahentType = "Kontrahenci";
                        KontrahentID = ((Kontrahent)value).ID;
                    }
                }
            }

        }
    }
}
