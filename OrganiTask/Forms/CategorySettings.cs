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
        private readonly int categoryId;
        private readonly DashboardController controller = new DashboardController();

        public event EventHandler<int> TagEditRequested;

        public CategorySettings(int categoryId)
        {
            InitializeComponent();
            this.categoryId = categoryId;
        }

        private void CategorySettings_Load(object sender, EventArgs e)
        {
            LoadCategoryDetails();
        }

        private void LoadCategoryDetails()
        {
            // Carga el título de la categoría desde el controlador
            CategoryViewModel cat = controller.LoadCategoryById(categoryId);
            lblHeader.Text = $"Información de {cat.Title}";

            // Carga las etiquetas de la categoría desde el controlador
            OrganiList<TagViewModel> tags = controller.GetTagsForCategory(categoryId);
            RenderTags(tags); // Prepara y renderiza las etiquetas en el panel
        }

        private void RenderTags(OrganiList<TagViewModel> tags)
        {
            flpTags.Controls.Clear(); // Limpiamos el panel de etiquetas
            foreach (TagViewModel tag in tags)
            {
                // Creamos un panel tipo card para la etiqueta
                Panel card = new Panel
                {
                    Width = 100,
                    Height = 50,
                    BackColor = Color.Transparent,
                    Margin = new Padding(5),
                    Tag = tag,
                    Cursor = Cursors.Hand
                };

                // Añadimos un evento al panel para abrir el editor de etiquetas
                EventHandler handler = (s, e) =>
                {
                    TagSetting editor = new TagSetting(tag);
                    editor.TagSaved += (sender, updated) => LoadCategoryDetails();
                    editor.TagDeleted += (sender, deletedId) => LoadCategoryDetails();
                    editor.ShowDialog();
                };

                // Label con el nombre
                Label lbl = new Label
                {
                    Text = tag.Name,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    BackColor = ColorUtil.ParseColor(tag.Color),
                    AutoSize = false
                };

                // Asignamos el evento al label y al panel
                card.Click += handler;
                lbl.Click += handler;

                card.Controls.Add(lbl);
                flpTags.Controls.Add(card);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
