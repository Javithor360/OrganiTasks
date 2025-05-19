using System.Drawing;

namespace OrganiTask.Util
{
    public static class ColorUtil
    {
        /// <summary>
        /// Dicta si un color es oscuro.
        /// <paramref name="color"/> es el color a evaluar.</param>
        /// <returns>Retorna true si el color es oscuro.</returns>
        /// </summary>
        public static bool IsDarkColor(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5;
        }

        /// <summary>
        /// Traduce un color en formato string a un objeto Color.
        /// </summary>
        /// <param name="colorString">El color en formato string.</param>
        /// <returns>El objeto Color correspondiente.</returns>
        public static Color ParseColor(string colorString)
        {
            try
            {
                Color known = Color.FromName(colorString);
                if (known.A != 0) // Si el color es conocido y no es transparente
                    return known;

                // Si no, probamos con hex
                if (colorString.StartsWith("#"))
                {
                    // Removemos el #
                    colorString = colorString.Substring(1);
                    // parse RRGGBB
                    int argb = int.Parse(colorString, System.Globalization.NumberStyles.HexNumber);
                    return Color.FromArgb(255, (argb >> 16) & 0xFF, (argb >> 8) & 0xFF, argb & 0xFF);
                }
            }
            catch { } // Ignoramos cualquier error y retornamos Gray por defecto

            return Color.Gray;
        }
    }
}
