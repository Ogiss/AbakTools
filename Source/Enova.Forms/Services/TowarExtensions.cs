using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Forms.Services
{
    public static class TowarExtensions
    {
        public static string Prefix(this API.Towary.Towar towar)
        {
            return (string)towar.Features["PREFIX"];
        }

        public static string Suffix(this API.Towary.Towar towar)
        {
            return (string)towar.Features["SUFFIX"];
        }

        public static bool Nowosc(this API.Towary.Towar towar)
        {
            return (bool)towar.Features["NOWOŚĆ"];
        }

        public static double? MnoznikObrotow(this API.Towary.Towar towar)
        {
            double? m = (double?)towar.Features["MNOŻNIK OBROTÓW"];
            return m == null || m.Value == 0 ? 1 : m.Value;
        }

        public static double ProcentObrotow(this API.Towary.Towar towar)
        {
            var p = (API.Types.Percent)towar.Features["PROCENT OBROTÓW"];
            if (p == null)
                return 1;
            var d = (double)p;
            return d == 0 ? 1 : d;
        }

        public static double ProcentDodawanychObrotow(this API.Towary.Towar towar)
        {
            var p = (API.Types.Percent)towar.Features["PROCENT DODANYCH OBROTÓW"];
            if (p == null)
                return 1;
            var d = (double)p;
            return d == 0 ? 1 : d;
        }

        public static int KolejnoscNaFormularzu(this API.Towary.Towar towar)
        {
            return (int)towar.Features["Kolejność na formularzu"];
        }

        public static string KolorNaForm(this API.Towary.Towar towar)
        {
            return (string)towar.Features["Kolor na formularzu"];
        }
    }
}
