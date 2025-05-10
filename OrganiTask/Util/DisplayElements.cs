using System;
using System.Drawing;
using System.Windows.Forms;
using OrganiTask.Entities;
using System.Drawing.Drawing2D;

namespace OrganiTask.Util
{
    public static class DisplayElements
    {
        /// <summary>
        /// Crea un panel de etiqueta estilo "chip" con el color y nombre de la etiqueta.
        /// </summary>
        /// <param name="tag">Etiqueta a mostrar</param>
        /// <returns>Panel con la etiqueta formateada</returns>
        public static Panel CreateTagChip(Tag tag)
        {
            // Validar que la etiqueta no sea nula
            if (tag == null || string.IsNullOrEmpty(tag.Name))
                return null;

            // Parsear el color de la etiqueta
            Color tagColor = ColorUtil.ParseColor(tag.Color);
            bool isDarkColor = ColorUtil.IsDarkColor(tagColor);

            // Panel principal del chip con bordes redondeados
            Panel chipPanel = new Panel
            {
                AutoSize = true,
                BackColor = tagColor,
                Margin = new Padding(2),
                Padding = new Padding(5, 2, 5, 2),
                BorderStyle = BorderStyle.None,
                MaximumSize = new Size(100, 24),
                MinimumSize = new Size(20, 20)
            };

            // Etiqueta con el texto
            Label lblTagName = new Label
            {
                Text = tag.Name,
                Font = new Font("Segoe UI", 8),
                ForeColor = isDarkColor ? Color.White : Color.Black,
                AutoSize = true,
                MaximumSize = new Size(90, 20),
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill
            };

            chipPanel.Controls.Add(lblTagName);
            return chipPanel;
        }
    }
}