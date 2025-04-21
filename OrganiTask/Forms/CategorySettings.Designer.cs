using System.Drawing;
using System;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    partial class CategorySettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpTags = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.flpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpTags
            // 
            this.flpTags.AutoScroll = true;
            this.flpTags.Location = new System.Drawing.Point(20, 174);
            this.flpTags.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.flpTags.Name = "flpTags";
            this.flpTags.Size = new System.Drawing.Size(760, 300);
            this.flpTags.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(20, 20);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(760, 40);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Título de categoría";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Location = new System.Drawing.Point(20, 489);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Cerrar";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.Controls.Add(this.lblHeader);
            this.flpMain.Controls.Add(this.txtName);
            this.flpMain.Controls.Add(this.btnEdit);
            this.flpMain.Controls.Add(this.btnSave);
            this.flpMain.Controls.Add(this.flpTags);
            this.flpMain.Controls.Add(this.btnClose);
            this.flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Size = new System.Drawing.Size(800, 450);
            this.flpMain.TabIndex = 0;
            this.flpMain.WrapContents = false;
            // 
            // txtName
            // 
            this.txtName.Font = this.lblHeader.Font;
            this.txtName.Location = this.lblHeader.Location;
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = this.lblHeader.Size;
            this.txtName.TabIndex = 1;
            this.txtName.Visible = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = this.lblHeader.Location;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "✏️";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = this.btnEdit.Location;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "💾";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CategorySettings
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flpMain);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "CategorySettings";
            this.Text = "Configuración de Categoría";
            this.Load += new System.EventHandler(this.CategorySettings_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flpTags;
        private Label lblHeader;
        private Button btnClose;
        private FlowLayoutPanel flpMain;
        private TextBox txtName;
        private Button btnEdit;
        private Button btnSave;
    }
}
