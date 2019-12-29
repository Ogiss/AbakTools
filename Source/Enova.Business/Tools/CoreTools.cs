using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Old.Tools
{
    public static class CoreTools
    {
        #region Methods

        public static string ByteArrayToString(byte[] t)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte num in t)
            {
                ByteToHex(num, sb);
            }
            return sb.ToString();
        }

        public static void ByteToHex(byte b, StringBuilder sb)
        {
            sb.Append(getCharUpp((b & 240) >> 4));
            sb.Append(getCharUpp(b & 15));
        }

        private static char getCharUpp(int v)
        {
            if (v < 10)
            {
                return (char)(0x30 + v);
            }
            return (char)((0x41 + v) - 10);
        }

        #endregion
    }
}
