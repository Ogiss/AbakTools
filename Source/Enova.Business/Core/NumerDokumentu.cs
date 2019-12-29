using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enova.Business.Old;
using Enova.Business.Old.Core;

namespace Enova.Old.Core
{
    [Obsolete("Funkcja autonumeracja brak kodu")]
    public class NumerDokumentu : SubRow
    {
        #region Fields

        public const int PelnyLength = 40;
        public const int SymbolLength = 0x20;
        public static readonly string Marker = "*";

        #endregion

        #region Properties

        public int Numer
        {
            get { return GetFieldValue<int>("Numer"); }
            set { SetFieldValue("Numer", value); }
        }
        public string Pelny
        {
            get { return GetFieldValue<string>("Pelny"); }
            set { SetFieldValue("Pelny", value); }
        }
        public string Symbol
        {
            get { return GetFieldValue<string>("Symbol"); }
            set { SetFieldValue("Symbol", value); }
        }
        private IDokument Dokument
        {
            get { return (IDokument)this.Parent; }
        }
        public string NumerPelny
        {
            get
            {
                string pelny = this.Pelny;
                if (pelny != "")
                {
                    return pelny;
                }
                return this.CalcNumerPelny();
            }
            set
            {
                if (!Tools.IsNumber(value))
                {
                    string symbol = this.Symbol;
                    if (symbol.Length > 0)
                    {
                        value = this.RemoveDupSlash(value);
                        if (symbol.Length > value.Length)
                        {
                            throw new ArgumentException();
                        }
                        int index = symbol.IndexOf(Marker);
                        if (-1 == index)
                        {
                            index = symbol.Length;
                        }
                        string str2 = value.Substring(0, index);
                        string str3 = Tools.Sub(value, ((value.Length - symbol.Length) + index) + 1);
                        if ((str2 != symbol.Substring(0, index)) || (str3 != Tools.Sub(symbol, index + 1)))
                        {
                            throw new ArgumentException();
                        }
                        value = value.Substring(index, (value.Length - str3.Length) - index);
                    }
                }
                this.Numer = int.Parse(value);
            }
        }

        public IKey WgNumeruDokumentu
        {
            get
            {
                var pinfo = this.Table.GetType().GetProperty(this.FullName + "WgNumeruDokumentu");
                if (pinfo != null)
                    return (IKey)pinfo.GetValue(this.Table, null);
                return null;
            }
        }
        public IKey WgSymboluDokumentu
        {
            get
            {
                var pinfo = this.Table.GetType().GetProperty(this.FullName + "WgSymboluDokumentu");
                if (pinfo != null)
                    return (IKey)pinfo.GetValue(this.Table, null);
                return null;
            }
        }

        #endregion

        #region Methods

        public NumerDokumentu() { }
        public NumerDokumentu(IRow parent, string nazwa) : base(parent, nazwa) { }

        [Obsolete("Nie wpełni działajaca implementacja funkcji")]
        public void Autonumeracja()
        {
            if (base.IsLive && (this.Numer == 0))
            {
                int num;
                bool readOnly = base.ReadOnly;
                if (base.ReadOnly)
                {
                    base.ReadOnly = false;
                }
                //IDokument prev = (IDokument)new SubTable(this.WgSymboluDokumentu, this.Symbol).GetPrev(new object[0]);
                IDokument prev = (IDokument)this.WgSymboluDokumentu.CreateSubTable(this.Symbol).GetPrev(new object[0]);
                if (prev == null)
                {
                    num = 1;
                }
                /*
                else if (((Row)prev).AccessRight == AccessRights.Denied)
                {
                    num = prev.Numer.GetDeniedNumer() + 1;
                }
                 */
                else
                {
                    num = prev.Numer.Numer + 1;
                }

                /*
                IWyłącznikNumeratora root = base.Root as IWyłącznikNumeratora;
                if ((root != null) && root.SprawdzaćSkasowane())
                {
                    foreach (Row row in base.Table.Rows.Changed)
                    {
                        if ((row.State == RowState.Deleted) && (((string)row[base.Prefix + ".Symbol", RowVersion.Original]) == this.Symbol))
                        {
                            int num2 = (int)row[base.Prefix + ".Numer", RowVersion.Original];
                            if (num <= num2)
                            {
                                num = num2 + 1;
                            }
                        }
                    }
                }
                 */
                this.Numer = num;
                if (readOnly)
                {
                    base.ReadOnly = true;
                }
            }
        }

        private string CalcNumerPelny()
        {
            if (this.Symbol == "")
            {
                return Marker;
            }
            string newValue = "?";
            if (this.Numer > 0)
            {
                newValue = this.Numer.ToString().PadLeft(this.RozmiarNumeru(), '0');
            }
            string symbol = this.Symbol;
            int index = symbol.IndexOf(Marker);
            if (-1 == index)
            {
                symbol = symbol + "/" + newValue;
            }
            else
            {
                symbol = symbol.Replace(Marker, newValue);
            }
            return this.RemoveDupSlash(symbol);
        }

        private int GetDeniedNumer()
        {
            return (int)base.GetFieldValue("Numer");
        }

        public static string LiczSymbol(IDefinicjaDokumentu defdok, string component, object dokument)
        {
            if (defdok == null)
            {
                return "*";
            }
            DefinicjaNumeracji numeracja = defdok.Numeracja;
            if (!numeracja.IsComponent(component))
            {
                return null;
            }
            string str = "";
            foreach (string str2 in (IEnumerable)numeracja)
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
                            object obj2 = Tools.Execute(dokument, strArray[0]);
                            str3 = (obj2 == null) ? "" : obj2.ToString();
                            if (((strArray.Length > 1) && (str3 != "")) && (obj2 is int))
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
                        catch
                        {
                        }
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

        public void PrzeliczSymbol()
        {
            this.PrzeliczSymbol("");
        }

        public void PrzeliczSymbol(string component)
        {
            if (base.Root.State != RowState.Detached)
            {
                string str = LiczSymbol(this.Dokument.Definicja, component, this.Dokument);
                if (str != null)
                {
                    this.SetSymbol(str);
                }
            }
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

        private void SetSymbol(string value)
        {
            value = this.RemoveDupSlash(value);
            if ((value.Length > 0) && (-1 == value.IndexOf(Marker)))
            {
                if (value[value.Length - 1] != '/')
                {
                    value = value + '/';
                }
                value = value + Marker;
            }
            if (this.Symbol != value)
            {
                this.Numer = 0;
                this.Symbol = value;
            }
        }

        public override string ToString()
        {
            return this.NumerPelny;
        }

        public static string UsunZbedneSeparatory(string numer)
        {
            if ((numer == null) || (numer == ""))
            {
                return numer;
            }
            return numer.Replace("//", "/").Trim(new char[] { '/' });
        }

        #endregion

    }
}
