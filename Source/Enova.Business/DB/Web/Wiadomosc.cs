using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class Wiadomosc
    {
        public Kontrahent Kontrahent
        {
            get
            {
                if (!KontrahentRefReference.IsLoaded)
                    KontrahentRefReference.Load();
                return KontrahentRef;
            }
        }

        public string Utf8Tekst
        {
            get
            {
                return System.Net.WebUtility.HtmlDecode(this.Tekst);
            }
            set
            {
                //this.Tekst = System.Net.WebUtility.HtmlEncode(value);
                this.Tekst = value;
            }
        }

        public override string ToString()
        {
            return this.Utf8Tekst;
        }
    }
}
