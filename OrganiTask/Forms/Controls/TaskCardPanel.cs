using System.Drawing;
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
            this.Cursor = Cursors.Hand;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(5);
        }

        // Método que se llama cuando se agrega un control al panel
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            // Adjuntamos los eventos de arrastre y soltar a los controles hijos recursivamente
            AttachDragDropHandlersRecursive(e.Control);
        }

        // Método auxiliar para adjuntar los eventos de arrastre y soltar a los controles hijos y sus subcontroles
        private void AttachDragDropHandlersRecursive(Control control)
        {
            // Adjuntamos los eventos al control actual
            control.MouseDown += Child_MouseDown;
            control.MouseMove += Child_MouseMove;
            control.MouseUp += Child_MouseUp;

            // Recursivamente adjuntamos los eventos a todos los controles hijos
            foreach (Control childControl in control.Controls)
            {
                AttachDragDropHandlersRecursive(childControl);
            }

            // Suscribirse al evento ControlAdded para adjuntar eventos a nuevos controles
            control.ControlAdded += (sender, e) => AttachDragDropHandlersRecursive(e.Control);
        }

        // Los métodos listados a continuación simplemente reenvían los eventos de arrastre y soltar al panel principal
        private void Child_MouseDown(object sender, MouseEventArgs e)
        {
            // Convertir las coordenadas del control hijo a coordenadas del control padre
            Point locationOnParent = this.PointToClient(((Control)sender).PointToScreen(e.Location));
            MouseEventArgs newEvent = new MouseEventArgs(e.Button, e.Clicks, locationOnParent.X, locationOnParent.Y, e.Delta);
            this.OnMouseDown(newEvent);
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            // Convertir las coordenadas del control hijo a coordenadas del control padre
            Point locationOnParent = this.PointToClient(((Control)sender).PointToScreen(e.Location));
            MouseEventArgs newEvent = new MouseEventArgs(e.Button, e.Clicks, locationOnParent.X, locationOnParent.Y, e.Delta);
            this.OnMouseMove(newEvent);
        }

        private void Child_MouseUp(object sender, MouseEventArgs e)
        {
            // Convertir las coordenadas del control hijo a coordenadas del control padre
            Point locationOnParent = this.PointToClient(((Control)sender).PointToScreen(e.Location));
            MouseEventArgs newEvent = new MouseEventArgs(e.Button, e.Clicks, locationOnParent.X, locationOnParent.Y, e.Delta);
            this.OnMouseUp(newEvent);
        }
    }
}