using OrganiTask.Controllers;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OrganiTask.Util;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para ver y modificar la información de una categoría.
    /// </summary>
    public partial class CategorySettings : Form
    {
        private readonly DashboardController controller = new DashboardController(); // Instancia del controlador

        private int categoryId; // ID de la categoría
        private int dashboardId; // ID del tablero al que pertenece la categoría
        private bool isNew, isEditing; // Bandera para indicar si estamos en modo edición

        // Eventos propios
        public event EventHandler CategoryUpdated;

        // Primer constructor, para crear una nueva categoría
        public CategorySettings(int dashboardId)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
            this.isNew = true;
        }

        // Segundo constructor, para editar una categoría existente o ver su información
        public CategorySettings(int categoryId, int dashboardId)
        {
            InitializeComponent();
            this.categoryId = categoryId;
            this.dashboardId = dashboardId;
            this.isNew = false;
        }

        private void CategorySettings_Load(object sender, EventArgs e)
        {
            if (isNew) // Si es una nueva categoría, mostramos el formulario en modo edición
                EnterEditMode();
            else // Si no, cargamos la información de la categoría
                LoadCategoryDetails();
        }

        private void LoadCategoryDetails()
        {
            // Carga el título de la categoría desde el controlador
            CategoryViewModel cat = controller.LoadCategoryById(categoryId);
            lblHeader.Text = cat.Title;

            // Definimos qué controles se tienen que mostrar y qué otros no
            txtName.Visible = false;
            btnSave.Visible = false;

            lblHeader.Visible = true;
            btnEdit.Visible = true;
            flpTags.Visible = true;
            btnClose.Visible = true;

            // Carga las etiquetas de la categoría desde el controlador
            OrganiList<TagViewModel> tags = controller.GetTagsForCategory(categoryId);
            RenderTags(tags); // Prepara y renderiza las etiquetas en el panel
        }

        private void EnterEditMode()
        {
            isEditing = true; // Activamos el modo edición

            // Ajustamos los controles según conveniencia
            txtName.Text = isNew ? "" : lblHeader.Text;
            txtName.Visible = true;
            btnSave.Visible = true;

            lblHeader.Visible = false;
            btnEdit.Visible = false;
            flpTags.Visible = false;
            btnClose.Visible = false;
        }

        private void SaveCategory()
        {
            // Validamos el nombre de la categoría
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isNew) // Si es una nueva categoría, creamos su modelo
            {
                CategoryViewModel newCategory = new CategoryViewModel 
                { 
                    DashboardId = dashboardId, 
                    Title = name 
                };

                controller.CreateCategory(newCategory); // y lo enviamos al controlador
                categoryId = newCategory.Id; // Guardamos el ID de la nueva categoría
                isNew = false; // Indicamos que la categoría ya no es nueva
            }
            else // Si no es nueva, simplemente actualizamos su título
            {
                // Actualizamos el título de la categoría
                controller.UpdateCategory(new CategoryViewModel { Id = categoryId, Title = name });
            }

            isEditing = false; // Salimos del modo edición
            CategoryUpdated?.Invoke(this, EventArgs.Empty); // Disparamos el evento de actualización de categoría
            LoadCategoryDetails(); // Recargamos la información de la categoría
        }

        private void RenderTags(OrganiList<TagViewModel> tags)
        {
            flpTags.Controls.Clear();

            // Obtenemos las tags existentes
            foreach (TagViewModel tag in tags)
            {
                // Creamos una card para cada tag
                Panel card = CreateTagCard(tag);
                flpTags.Controls.Add(card); // y la agregamos al panel
            }

            // luego, al final creamos una card para crear una nueva tag
            // esta card actúa como botón de creación

            // Definimos un panel que actuará como botón
            Panel plusCard = new Panel
            {
                Width = 100,
                Height = 50,
                BackColor = Color.LightGray,
                Margin = new Padding(5),
                Cursor = Cursors.Hand
            };

            // Dentro del panel asignamos un label que contendrá el indicador visual
            Label plusLabel = new Label
            {
                Text = "+",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DimGray
            };

            // Asignamos el evento de click al panel y al label
            // y agregamos los controles
            plusCard.Controls.Add(plusLabel);
            plusCard.Click += (s, e) => BeginInlineNewTag(plusCard);
            plusLabel.Click += (s, e) => BeginInlineNewTag(plusCard);
            flpTags.Controls.Add(plusCard);
        }

        private Panel CreateTagCard(TagViewModel tag)
        {
            // Definimos un panel que actuará como tarjeta
            Panel card = new Panel
            {
                Width = 100,
                Height = 50,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Tag = tag
            };

            // Definimos el comportamiento de los eventos en un handler
            EventHandler handler = (s, e) =>
            {
                // Instanciamos el formulario de detalles de la etiqueta
                TagDetails tagDetails = new TagDetails(tag);

                // Listener para el evento de guardar
                tagDetails.TagSaved += (ss, updated) => { 
                    LoadCategoryDetails();
                    CategoryUpdated?.Invoke(this, EventArgs.Empty); 
                };

                // Listener para el evento de eliminar
                tagDetails.TagDeleted += (ss, deletedId) => { 
                    LoadCategoryDetails();
                    CategoryUpdated?.Invoke(this, EventArgs.Empty); 
                };

                tagDetails.ShowDialog();
            };

            var baseColor = ColorUtil.ParseColor(tag.Color);

            // Definimos un label que contendrá el nombre de la etiqueta
            Label lbl = new Label
            {
                Text = tag.Name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = ColorUtil.IsDarkColor(baseColor) ? Color.White : Color.Black,
                BackColor = baseColor,
                AutoSize = false
            };

            // Asignamos el handler de eventos al panel y al label
            card.Click += handler;
            lbl.Click += handler;

            // Agregamos el label al panel
            card.Controls.Add(lbl);
            return card;
        }

        private void BeginInlineNewTag(Panel plusCard)
        {
            // Desactivamos el evento de click para evitar que se llame varias veces
            plusCard.Click -= (s, e) => BeginInlineNewTag(plusCard);

            // Limpiamos el panel para definir nuevos controles de creación
            plusCard.Controls.Clear();

            // Primero creamos un text box para el nombre de la etiqueta
            TextBox txt = new TextBox
            {
                Width = plusCard.Width - 10,
                Location = new Point(5, 5)
            };
            plusCard.Controls.Add(txt); // agregamos el text box al panel

            // Ahora un paneel de preview para el color
            Panel preview = new Panel
            {
                BackColor = Color.Gray,
                Size = new Size(20, 20),
                Location = new Point(5, txt.Bottom + 5),
                Cursor = Cursors.Hand
            };
            // Asignamos el evento de click al panel de preview
            preview.Click += (s, e) =>
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    dlg.Color = preview.BackColor;
                    if (dlg.ShowDialog() == DialogResult.OK)
                        preview.BackColor = dlg.Color;
                }
            };
            plusCard.Controls.Add(preview); // agregamos el panel de preview al panel

            // El botón de guardar
            Button btnOk = new Button
            {
                Text = "✓",
                Size = new Size(24, 24),
                Location = new Point(preview.Right + 5, txt.Bottom + 3)
            };
            // Con su evento click
            btnOk.Click += (s, e) =>
            {
                // Obtenemos y validamos el nombre
                string name = txt.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Creamos el modelo de la etiqueta
                TagViewModel vm = new TagViewModel
                {
                    CategoryId = categoryId,
                    Name = name,
                    Color = $"#{preview.BackColor.R:X2}{preview.BackColor.G:X2}{preview.BackColor.B:X2}"
                };

                // Llamamos al controlador para crear la etiqueta
                controller.CreateTag(vm);

                // Recargamos la categoría y disparamos el evento de cambio
                LoadCategoryDetails();
                CategoryUpdated?.Invoke(this, EventArgs.Empty);
            };
            plusCard.Controls.Add(btnOk); // agregamos el botón de guardar al panel

            // Botón de cancelar
            Button btnCancel = new Button
            {
                Text = "✕",
                Size = new Size(24, 24),
                Location = new Point(btnOk.Right + 5, txt.Bottom + 3)
            };

            btnCancel.Click += (s, e) => RenderTags(controller.GetTagsForCategory(categoryId)); // simplemente recargamos las etiquetas
            plusCard.Controls.Add(btnCancel); // agregamos el botón de cancelar al panel
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnterEditMode();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCategory();
        }
    }
}
