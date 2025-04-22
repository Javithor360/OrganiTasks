using System.Drawing;
using System.Windows.Forms;
using System;

namespace OrganiTask.Forms
{
    partial class TagDetails
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
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblNameKey = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblColorKey = new System.Windows.Forms.Label();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.flpMain.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.Controls.Add(this.lblHeader);
            this.flpMain.Controls.Add(this.lblNameKey);
            this.flpMain.Controls.Add(this.txtName);
            this.flpMain.Controls.Add(this.lblColorKey);
            this.flpMain.Controls.Add(this.btnColorPicker);
            this.flpMain.Controls.Add(this.pnlColorPreview);
            this.flpMain.Controls.Add(this.flpButtons);
            this.flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Size = new System.Drawing.Size(400, 300);
            this.flpMain.TabIndex = 0;
            this.flpMain.WrapContents = false;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(23, 20);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(300, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Editar etiqueta";
            // 
            // lblNameKey
            // 
            this.lblNameKey.AutoSize = true;
            this.lblNameKey.Location = new System.Drawing.Point(20, 60);
            this.lblNameKey.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblNameKey.Name = "lblNameKey";
            this.lblNameKey.Size = new System.Drawing.Size(47, 13);
            this.lblNameKey.TabIndex = 1;
            this.lblNameKey.Text = "Nombre:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(23, 76);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 20);
            this.txtName.TabIndex = 2;
            // 
            // lblColorKey
            // 
            this.lblColorKey.AutoSize = true;
            this.lblColorKey.Location = new System.Drawing.Point(20, 109);
            this.lblColorKey.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblColorKey.Name = "lblColorKey";
            this.lblColorKey.Size = new System.Drawing.Size(34, 13);
            this.lblColorKey.TabIndex = 3;
            this.lblColorKey.Text = "Color:";
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.Location = new System.Drawing.Point(23, 125);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(75, 23);
            this.btnColorPicker.TabIndex = 4;
            this.btnColorPicker.Text = "Seleccionar...";
            this.btnColorPicker.Click += new System.EventHandler(this.btnColorPicker_Click);
            // 
            // pnlColorPreview
            // 
            this.pnlColorPreview.Location = new System.Drawing.Point(30, 151);
            this.pnlColorPreview.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(50, 25);
            this.pnlColorPreview.TabIndex = 5;
            // 
            // flpButtons
            // 
            this.flpButtons.AutoSize = true;
            this.flpButtons.Controls.Add(this.btnCancel);
            this.flpButtons.Controls.Add(this.btnSave);
            this.flpButtons.Controls.Add(this.btnDelete);
            this.flpButtons.Location = new System.Drawing.Point(20, 196);
            this.flpButtons.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Size = new System.Drawing.Size(243, 29);
            this.flpButtons.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(84, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(165, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // TagSetting
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.flpMain);
            this.Name = "TagSetting";
            this.Text = "Editar Etiqueta";
            this.Load += new System.EventHandler(this.TagSettingsForm_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}