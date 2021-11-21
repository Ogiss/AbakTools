using System.Drawing;

namespace BAL.Forms.Helpers
{
    public static class ColorHelper
    {
        public static bool TryParseColorFromHtml(string html, out Color color)
        {
            color = default;

            try
            {
                color = ColorTranslator.FromHtml(html);
                return true;
            }
            catch { }

            return false;
        }

        public static Color ParseColor(string html, Color defaultColor)
        {
            if (TryParseColorFromHtml(html, out Color color))
            {
                return color;
            }

            return defaultColor;
        }

        public static string ToHtml(Color color)
        {
            return ColorTranslator.ToHtml(color);
        }
    }
}
