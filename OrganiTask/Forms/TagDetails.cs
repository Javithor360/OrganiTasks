using OrganiTask.Controllers;
using OrganiTask.Entities.ViewModels;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para ver y modificar la información de una etiqueta.
    /// </summary>
    public partial class TagDetails: Form
    {
        private readonly TagViewModel tag;
        private readonly bool isNew;

        private readonly DashboardController controller = new DashboardController();
        public event EventHandler<TagViewModel> TagSaved;
        public event EventHandler<int> TagDeleted;

        public TagDetails(int categoryId)
        {
            InitializeComponent();
            isNew = true;
            tag = new TagViewModel { Id = 0, CategoryId = categoryId };
        }

        public TagDetails(TagViewModel existingTag)
        {
            InitializeComponent();
            isNew = false;
            tag = existingTag;
        }

        private void TagSettingsForm_Load(object sender, EventArgs e)
        {
            // Título del formulario
            lblHeader.Text = isNew
                ? "➕ Nueva etiqueta"
                : $"✏️ Editar etiqueta «{tag.Name}»";

            if (!isNew)
            {
                txtName.Text = tag.Name;
                try
                {
                    pnlColorPreview.BackColor = ColorTranslator.FromHtml(tag.Color);
                }
                catch
                {
                    pnlColorPreview.BackColor = Color.Gray;
                }
            }
            else
            {
                // color por defecto
                pnlColorPreview.BackColor = Color.Gray;
            }

            btnDelete.Visible = !isNew;
        }


        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pnlColorPreview.BackColor = dlg.Color;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TagController tagController = new TagController();

            // Validación de nombre
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Actualizar el objeto del modelo
            tag.Name = name;
            Color c = pnlColorPreview.BackColor;
            tag.Color = $"#{c.R:X2}{c.G:X2}{c.B:X2}";

            if (isNew)
                tagController.CreateTag(tag);
            else
                tagController.UpdateTag(tag);

            TagSaved?.Invoke(this, tag);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("¿Seguro que deseas eliminar esta etiqueta?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                new TagController().DeleteTag(tag.Id);
                TagDeleted?.Invoke(this, tag.Id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
