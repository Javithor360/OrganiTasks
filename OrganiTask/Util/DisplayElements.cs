using System;
using System.Drawing;
using System.Windows.Forms;
using OrganiTask.Entities; 
using System.Drawing.Drawing2D;

namespace OrganiTask.Util
{
    public static class DisplayElements
    {
        public static Panel CreateTagChip(Tag tag)
        {
            Console.Write(tag.Name);
            
            Color tagColor = ColorUtil.ParseColor(tag.Color);
            Color textColor = ColorUtil.IsDarkColor(tagColor) ? Color.White : Color.Black;

            Panel tagChip = new Panel
            {
                AutoSize = false,
                Size = new Size(80, 22),
                Margin = new Padding(0, 0, 5, 5),
                BackColor = tagColor,
                BorderStyle = BorderStyle.None
            };

            tagChip.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    int radius = 10;
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(tagChip.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(tagChip.Width - radius, tagChip.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, tagChip.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    e.Graphics.FillPath(new SolidBrush(tagColor), path);
                }
            };

            Label lblTagName = new Label
            {
                Text = tag.Name,
                AutoSize = false,
                Size = new Size(80, 22),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = textColor,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };

            tagChip.Controls.Add(lblTagName);
            return tagChip;
        }
    }
}
