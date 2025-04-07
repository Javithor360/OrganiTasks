using System.Windows.Forms;
using OrganiTask.Entities.ViewModels;

namespace OrganiTask.Forms.Controls
{
    /// <summary>
    /// Clase que representa un panel de tarjeta de tarea.
    /// </summary>
    public class TaskCardPanel : Panel
    {
        // Propiedades para almacenar la información de la tarjeta
        public TaskViewModel TaskData { get; set; } // Información de la tarea
        public int CurrentTagId { get; set; } // ID de la etiqueta que se está usando para renderizar el tablero asignada a tarea

        // Constructor que aplica el estilo estándar a la tarjeta
        public TaskCardPanel()
        {
            this.Width = 200;
            this.Height = 80;
            this.Cursor = Cursors.Hand;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(5);
        }

        // Método que se llama cuando se agrega un control al panel
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            // Adjuntamos los eventos de arrastre y soltar a los controles hijos
            AttachDragDropHandlers(e.Control);
        }

        // Método auxiliar para adjuntar los eventos de arrastre y soltar a los controles hijos
        private void AttachDragDropHandlers(Control control)
        {
            control.MouseDown += Child_MouseDown;
            control.MouseMove += Child_MouseMove;
            control.MouseUp += Child_MouseUp;
        }

        // Los métodos listados a continuación simplemente reenvían los eventos de arrastre y soltar al panel principal

        private void Child_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        private void Child_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
    }
}
