using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;
using System.Security.Cryptography;

namespace AbakTools.Business
{
    public static class Tools
    {
        static string defaultKey = "154FF43AB5AA984173EFCCBB";

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
                else if (c == ' ' || c == '_' || c == '-')
                {
                    ret += '_';
                }
                else if (char.IsDigit(c) || char.IsLetter(c))
                {
                    ret += c;
                }
            }

            return ReplaceRegionChars(ret.Trim().ToLower());
        }

        private static Dictionary<char, char> regionCharMap = new Dictionary<char, char>()
        {
            { 'ę', 'e' },
            { 'ó', 'o' },
            { 'ą', 'a' },
            { 'ś', 's' },
            { 'ł', 'l' },
            { 'ż', 'z' },
            { 'ź', 'z' },
            { 'ć', 'c' },
            { 'ń', 'n' },
            { 'Ę', 'E' },
            { 'Ó', 'O' },
            { 'Ą', 'A' },
            { 'Ś', 'S' },
            { 'Ł', 'L' },
            { 'Ż', 'Z' },
            { 'Ź', 'Z' },
            { 'Ć', 'C' },
            { 'Ń', 'N' }
        };

        public static string ReplaceRegionChars(string str)
        {
            string ret = "";
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (regionCharMap.ContainsKey(c))
                    ret += regionCharMap[c];
                else
                    ret += c;
            }
            return ret;
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

        public static string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, defaultKey, true);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            return Encrypt(toEncrypt, defaultKey, useHashing);
        }

        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static object Execute(object obj, string props)
        {
            foreach (string str in props.Split(new char[] { '.' }))
            {
                if (obj == null)
                {
                    return obj;
                }
                obj = TypeDescriptor.GetProperties(obj).Find(str, true).GetValue(obj);
            }
            return obj;
        }
    }
}
