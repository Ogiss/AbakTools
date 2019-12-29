using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.DB.Web
{
    public partial class NumerDokumentu
    {
        #region Fields

        #endregion

        #region Methods

        public IDokument Dokument { get; set; }

        #endregion

        #region Methods

        public static string LiczSymbol(IDefinicjaDokumentu definicja, object dokument)
        {
            if (definicja == null)
                return "*";
            DefinicjaNumeracji numeracja = definicja.Numeracja;
            string str = "";
            foreach (string str2 in numeracja)
            {
                string str3 = "";
                if (str2 != "")
                {
                    if (str2[0] == '*')
                    {
                        str3 = "*";
                    }
                    else
                    {
                        try
                        {
                            string[] strArray = str2.Split(new char[] { ':' });
                            object obj2 = AbakTools.Business.Tools.Execute(dokument, strArray[0]);
                            str3 = (obj2 == null) ? "" : obj2.ToString();
                            if (strArray.Length > 1 && str3 != "" && obj2 is int)
                            {
                                int length = int.Parse(strArray[1]);
                                if (str3.Length > length)
                                {
                                    str3 = str3.Substring(str3.Length - length, length);
                                }
                                else
                                {
                                    str3 = str3.PadLeft(length, '0');
                                }
                            }
                        }
                        catch { }

                    }
                }
                if (str3 != "")
                {
                    if (str != "")
                    {
                        str = str + "/" + str3;
                    }
                    else
                    {
                        str = str3;
                    }
                }
            }
            return str;
        }

        private int RozmiarNumeru()
        {
            foreach (string str in (IEnumerable)this.Dokument.Definicja.Numeracja)
            {
                if ((str.Length > 0) && (str[0] == '*'))
                {
                    string[] strArray = str.Split(new char[] { ':' });
                    if (strArray.Length == 1)
                    {
                        return 0;
                    }
                    return int.Parse(strArray[1]);
                }
            }
            return 0;
        }

        public string CalcNumerPelny()
        {
            if (this.Symbol == "")
            {
                return "*";
            }
            string newValue = "?";
            if (this.Numer > 0)
            {
                newValue = this.Numer.ToString().PadLeft(this.RozmiarNumeru(), '0');
            }
            string symbol = this.Symbol;
            int index = symbol.IndexOf("*");
            if (-1 == index)
            {
                symbol = symbol + "/" + newValue;
            }
            else
            {
                symbol = symbol.Replace("*", newValue);
            }
            return this.RemoveDupSlash(symbol);
        }

        private string RemoveDupSlash(string ss)
        {
            int startIndex = 0;
            bool flag = false;
            ss = ss.Replace('\\', '/');
            while (startIndex < ss.Length)
            {
                char ch = ss[startIndex];
                if (ch == '/')
                {
                    if (!flag)
                    {
                        flag = true;
                        startIndex++;
                    }
                    else
                    {
                        ss = ss.Remove(startIndex, 1);
                    }
                }
                else
                {
                    flag = false;
                    startIndex++;
                }
            }
            return ss;
        }

        public override string ToString()
        {
            return this.NumerPelny;
        }


        #endregion
    }
}
