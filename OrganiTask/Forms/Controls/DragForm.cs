using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms.Controls
{
    /// <summary>
    /// Clase que representa un formulario de arrastre.
    /// Este formulario de arrastre es usado para crear una ventana "fantasma" que se muestra
    /// al arrastrar una tarea en el tablero Kanban, mejorando la experiencia de usuario.
    /// </summary>
    public class DragForm : Form
    {
        // Constructor que define el estilo estándar de la ventana de arrastre
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

        // Sobrecarga de CreateParams para permitir que la ventana sea "transparente" a los eventos de mouse,
        // es decir, que los eventos de mouse se envíen a la ventana subyacente (que los ignore)
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
