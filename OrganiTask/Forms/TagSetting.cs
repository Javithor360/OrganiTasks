using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    /// <summary>
    /// Formulario para ver y modificar la información de una etiqueta.
    /// </summary>
    public partial class TagSetting: Form
    {
        private readonly TagViewModel tag;
        private readonly DashboardController controller = new DashboardController();
        public event EventHandler<TagViewModel> TagSaved;
        public event EventHandler<int> TagDeleted;

        public TagSetting(TagViewModel tag)
        {
            InitializeComponent();
            this.tag = tag;
        }

        private void TagSettingsForm_Load(object sender, EventArgs e)
        {
            // Título del formulario
            lblHeader.Text = $"Editar etiqueta: {tag.Name}";
            // Cargar información de la etiqueta
            txtName.Text = tag.Name;
            pnlColorPreview.BackColor = ColorTranslator.FromHtml(tag.Color); // Convert color from hex to Color
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
            // Validate
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Update tag
            tag.Name = name;
            // Convert color to hex
            Color c = pnlColorPreview.BackColor;
            tag.Color = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
            controller.UpdateTag(tag);
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
                controller.DeleteTag(tag.Id);
                TagDeleted?.Invoke(this, tag.Id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
