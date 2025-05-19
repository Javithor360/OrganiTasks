using System;
using System.Drawing;
using System.Windows.Forms;
using OrganiTask.Entities;
using OrganiTask.Util;

namespace OrganiTask.Forms.Controls
{
    public class TagChipControl : Panel
    {
        public TagChipControl(Tag tag)
        {
            if (tag == null || string.IsNullOrEmpty(tag.Name))
                throw new ArgumentNullException(nameof(tag));

            Color tagColor = ColorUtil.ParseColor(tag.Color);
            bool isDarkColor = ColorUtil.IsDarkColor(tagColor);

            // Configurar el panel
            this.AutoSize = true;
            this.BackColor = tagColor;
            this.Margin = new Padding(2);
            this.Padding = new Padding(5, 2, 5, 2);
            this.BorderStyle = BorderStyle.None;
            this.MaximumSize = new Size(100, 24);
            this.MinimumSize = new Size(20, 20);

            // Configurar la etiqueta
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

            this.Controls.Add(lblTagName);
        }
    }
}
