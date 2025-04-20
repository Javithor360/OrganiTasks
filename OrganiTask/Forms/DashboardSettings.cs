using OrganiTask.Controllers;
using OrganiTask.Entities;
using OrganiTask.Entities.ViewModels;
using OrganiTask.Util;
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

namespace OrganiTask.Forms
{
    public partial class DashboardSettings: Form
    {
        private readonly int dashboardId;
        private readonly DashboardController controller = new DashboardController();

        public DashboardSettings(int dashboardId)
        {
            InitializeComponent();
            this.dashboardId = dashboardId;
        }

        private void DashboardSettings_Load(object sender, EventArgs e)
        {
            LoadDashboardInfo();
        }

        private void LoadDashboardInfo()
        {
            DashboardViewModel dvm = controller.LoadDashboardDetails(dashboardId);

            if (dvm == null)
            {
                MessageBox.Show("Error al cargar el tablero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            string username = controller.GetUsernameFromDashboardOwnerId(dvm.UserId);


            lblHeader.Text = $"Información de {dvm.DashboardTitle}";
            lblCreatorValue.Text = username ?? "(desconocido)";
            lblDescText.Text = dvm.Description ?? "(sin descripción)";

            LoadCategoriesTable();

            btnSave.Visible = false;
            btnCancel.Visible = true;
            btnEdit.Visible = true;
        }

        private void LoadCategoriesTable()
        {
            tblCategories.Controls.Clear();
            tblCategories.RowStyles.Clear();
            tblCategories.RowCount = 0;

            OrganiList<CategoryViewModel> columnTitles = controller.GetDashboardCategories(dashboardId);

            int row = 0;
            foreach (var column in columnTitles)
            {
                var lbl = new Label
                {
                    Text = column.Title,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(3),
                    Tag = column.Id
                };
                tblCategories.RowCount++;
                tblCategories.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tblCategories.Controls.Add(lbl, 0, row);

                var btn = new Button
                {
                    Text = "Editar",
                    AutoSize = true,
                    Tag = column.Id,
                    Margin = new Padding(3)
                };
                btn.Click += (s, e) =>
                {
                    // TODO: Implementar la lógica para editar la categoría 
                    MessageBox.Show($"Editar «{((Button)s).Tag}»");
                };
                tblCategories.Controls.Add(btn, 1, row);

                row++;
            }

            tblCategories.RowCount++;
            tblCategories.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            Button btnNew = new Button
            {
                Text = "➕ Nueva categoría",
                AutoSize = true,
                Margin = new Padding(3)
            };

            btnNew.Click += (s, e) =>
            {
                MessageBox.Show("Vamos a crear una nueva categoría…");
            };

            tblCategories.Controls.Add(btnNew, 0, row);
            tblCategories.SetColumnSpan(btnNew, 2);
        }
    }
}
