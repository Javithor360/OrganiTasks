using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms.Controls
{
    public class DragForm : Form
    {
        public DragForm(Image dragImage)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.TopMost = false;
            this.Opacity = 0.75;
            this.BackgroundImage = dragImage;
            this.ClientSize = dragImage.Size;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // WS_EX_TRANSPARENT (0x20) permite que la ventana sea "transparente" a los eventos de mouse
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
}
