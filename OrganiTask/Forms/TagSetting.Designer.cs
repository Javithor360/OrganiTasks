using System.Drawing;
using System.Windows.Forms;
using System;

namespace OrganiTask.Forms
{
    partial class TagSetting
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flpMain;
        private Label lblHeader;
        private Label lblNameKey;
        private TextBox txtName;
        private Label lblColorKey;
        private Button btnColorPicker;
        private Panel pnlColorPreview;
        private FlowLayoutPanel flpButtons;
        private Button btnSave;
        private Button btnCancel;
        private Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flpMain = new FlowLayoutPanel();
            this.lblHeader = new Label();
            this.lblNameKey = new Label();
            this.txtName = new TextBox();
            this.lblColorKey = new Label();
            this.btnColorPicker = new Button();
            this.pnlColorPreview = new Panel();
            this.flpButtons = new FlowLayoutPanel();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.btnDelete = new Button();

            // flpMain
            this.flpMain.Dock = DockStyle.Fill;
            this.flpMain.FlowDirection = FlowDirection.TopDown;
            this.flpMain.WrapContents = false;
            this.flpMain.Padding = new Padding(20);
            this.flpMain.AutoScroll = true;
            this.flpMain.Controls.Add(this.lblHeader);
            this.flpMain.Controls.Add(this.lblNameKey);
            this.flpMain.Controls.Add(this.txtName);
            this.flpMain.Controls.Add(this.lblColorKey);
            this.flpMain.Controls.Add(this.btnColorPicker);
            this.flpMain.Controls.Add(this.pnlColorPreview);
            this.flpMain.Controls.Add(this.flpButtons);

            // lblHeader
            this.lblHeader.AutoSize = false;
            this.lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblHeader.Height = 30;
            this.lblHeader.Dock = DockStyle.Top;
            this.lblHeader.Text = "Editar etiqueta";
            // lblNameKey
            this.lblNameKey.AutoSize = true;
            this.lblNameKey.Text = "Nombre:";
            this.lblNameKey.Margin = new Padding(0, 10, 0, 0);
            // txtName
            this.txtName.Width = 300;
            // lblColorKey
            this.lblColorKey.AutoSize = true;
            this.lblColorKey.Text = "Color:";
            this.lblColorKey.Margin = new Padding(0, 10, 0, 0);
            // btnColorPicker
            this.btnColorPicker.Text = "Seleccionar...";
            this.btnColorPicker.Click += new EventHandler(this.btnColorPicker_Click);
            // pnlColorPreview
            this.pnlColorPreview.Width = 50;
            this.pnlColorPreview.Height = 25;
            this.pnlColorPreview.Margin = new Padding(10, 0, 0, 0);
            // flpButtons
            this.flpButtons.FlowDirection = FlowDirection.LeftToRight;
            this.flpButtons.AutoSize = true;
            this.flpButtons.Margin = new Padding(0, 20, 0, 0);
            this.flpButtons.Controls.Add(this.btnSave);
            this.flpButtons.Controls.Add(this.btnCancel);
            this.flpButtons.Controls.Add(this.btnDelete);
            // btnSave
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            // btnCancel
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            // btnDelete
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // TagSettingsForm
            this.ClientSize = new Size(400, 300);
            this.Controls.Add(this.flpMain);
            this.Text = "Editar Etiqueta";
            this.Load += new EventHandler(this.TagSettingsForm_Load);
        }
    }
}