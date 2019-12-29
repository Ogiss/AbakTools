using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Drawing;

namespace Enova.Business.Old.Core
{
    public static class Tools
    {
        public static string FromHtmlText(string s)
        {
            return !string.IsNullOrEmpty(s) ? s.Replace("&oacute;", "ó") : null;
        }

        public static bool IsAlphaChar(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == ' ' || c == '_';
        }

        public static bool IsDigitChar(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsPrintableChar(char c)
        {
            return IsAlphaChar(c) || IsDigitChar(c) ||
                c == '"' || c == '\'' || c == '!' || c == '@' || c == '!' || c == '.' || c == ','
                || c == '#' || c == '$' || c == ':' || c == ';' || c == '-' || c=='+';
        }

        public static string LinkRewrite(string str)
        {
            string ret = "";
            int length = str.Length;

            for (int i = 0; i < length; i++)
            {
                char c = str[i];
                if (HttpUtility.HtmlEncode(c.ToString()).Length > 1)
                {
                    string entity = HttpUtility.HtmlEncode(c.ToString());
                    ret += entity[1];
                }
                else if (c == ' ')
                {
                    ret += '_';
                }
                else if (IsAlphaChar(c))
                {
                    ret += c;
                }
                else if (IsDigitChar(c))
                {
                    ret += c;
                }

            }

            return ret.Trim().ToLower();
        }

        public static string MD5(string pass)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(pass));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataMd5.Length; i++)
                sb.AppendFormat("{0:x2}", dataMd5[i]);
            return sb.ToString();
        }

        public static string UniqId(string prefix, bool more_entropy)
        {
            if (string.IsNullOrEmpty(prefix))
                prefix = string.Empty;

            if (!more_entropy)
            {
                return (prefix + System.Guid.NewGuid().ToString()).Substring(0, 13);
            }
            else
            {
                return (prefix + System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString()).Substring(0, 23);
            }
        }



        public static string GenSecureKey()
        {
            return MD5(UniqId(new Random().Next().ToString(), true));
        }

        public static string GenPassword(string passwd)
        {
            return MD5("6sc94YKfyNM23DHpkz2ook4oGRRWFoezht9NwGuTz4rXEIVT2rHaUr40" + passwd);
        }

        public static Bitmap RenderImage(Bitmap srcImg, int width, int height)
        {
            Bitmap img = new Bitmap(width, height);
            Color bkgColor = Color.White;
            bool hOrientation = srcImg.Width > srcImg.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    img.SetPixel(x, y, bkgColor);
                }
            }
            Graphics g = Graphics.FromImage(img);
            float s = ((float)srcImg.Width) / ((float)srcImg.Height);
            int newWidth = hOrientation ? width : ((int)(height * s));
            int newHeight = hOrientation ? ((int)(((float)width) / s)) : height;
            int dstX = hOrientation ? 0 : ((width - newWidth) / 2);
            int dstY = hOrientation ? ((height - newHeight) / 2) : 0;
            Bitmap tImg = new Bitmap(srcImg, newWidth, newHeight);
            g.DrawImage(tImg, dstX, dstY, newWidth, newHeight);
            g.Dispose();
            return img;
        }


    }
}
