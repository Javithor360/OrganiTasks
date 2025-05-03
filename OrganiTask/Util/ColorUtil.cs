using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiTask.Util
{
    public static class ColorUtil
    {
        /// <summary>
        /// Traduce un color en formato string a un objeto Color.
        /// </summary>
        /// <param name="colorString">El color en formato string.</param>
        /// <returns>El objeto Color correspondiente.</returns>
        public static System.Drawing.Color ParseColor(string colorString)
        {
            try
            {
                System.Drawing.Color known = System.Drawing.Color.FromName(colorString);
                if (known.A != 0) // Si el color es conocido y no es transparente
                    return known;

                // Si no, probamos con hex
                if (colorString.StartsWith("#"))
                {
                    // Removemos el #
                    colorString = colorString.Substring(1);
                    // parse RRGGBB
                    int argb = int.Parse(colorString, System.Globalization.NumberStyles.HexNumber);
                    return System.Drawing.Color.FromArgb(255, (argb >> 16) & 0xFF, (argb >> 8) & 0xFF, argb & 0xFF);
                }
            }
            catch { } // Ignoramos cualquier error y retornamos Gray por defecto

            return System.Drawing.Color.Gray;
        }
    }

    public static class ColorHelper
    {
        public static bool IsDarkColor(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5;
        }
    }

}
