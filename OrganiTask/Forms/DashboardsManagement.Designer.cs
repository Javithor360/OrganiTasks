namespace OrganiTask.Forms.Test
{
    partial class DashboardsManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDashboardTitle;
        private System.Windows.Forms.Button btnSaveDashboard;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TextBox txtDashboardDescription;
        private System.Windows.Forms.Label lblDashboardDescription;
        private System.Windows.Forms.CheckBox chkCreateDefaultCategory;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblTagName;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.Button btnAddTag;
        private System.Windows.Forms.ListBox listBoxTags;
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.txtDashboardTitle = new System.Windows.Forms.TextBox();
            this.btnSaveDashboard = new System.Windows.Forms.Button();
            this.txtDashboardDescription = new System.Windows.Forms.TextBox();
            this.lblDashboardDescription = new System.Windows.Forms.Label();
            this.chkCreateDefaultCategory = new System.Windows.Forms.CheckBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblTagName = new System.Windows.Forms.Label();
            this.txtTagName = new System.Windows.Forms.TextBox();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.listBoxTags = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(243, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CREAR NUEVO TABLERO";
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(21, 90);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(112, 19);
            this.lblCategoryTitle.TabIndex = 1;
            this.lblCategoryTitle.Text = "Título de tablero:";
            // 
            // txtDashboardTitle
            // 
            this.txtDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDashboardTitle.Location = new System.Drawing.Point(21, 112);
            this.txtDashboardTitle.Name = "txtDashboardTitle";
            this.txtDashboardTitle.Size = new System.Drawing.Size(558, 25);
            this.txtDashboardTitle.TabIndex = 2;
            // 
            // btnSaveDashboard
            // 
            this.btnSaveDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSaveDashboard.FlatAppearance.BorderSize = 0;
            this.btnSaveDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveDashboard.ForeColor = System.Drawing.Color.White;
            this.btnSaveDashboard.Location = new System.Drawing.Point(353, 523);
            this.btnSaveDashboard.Name = "btnSaveDashboard";
            this.btnSaveDashboard.Size = new System.Drawing.Size(110, 40);
            this.btnSaveDashboard.TabIndex = 14;
            this.btnSaveDashboard.Text = "CREAR";
            this.btnSaveDashboard.UseVisualStyleBackColor = false;
            this.btnSaveDashboard.Visible = true;
            this.btnSaveDashboard.Click += new System.EventHandler(this.btnSaveDashboard_Click);
            // 
            // txtDashboardDescription
            // 
            this.txtDashboardDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDashboardDescription.Location = new System.Drawing.Point(21, 169);
            this.txtDashboardDescription.Multiline = true;
            this.txtDashboardDescription.Name = "txtDashboardDescription";
            this.txtDashboardDescription.Size = new System.Drawing.Size(558, 60);
            this.txtDashboardDescription.TabIndex = 4;
            // 
            // lblDashboardDescription
            // 
            this.lblDashboardDescription.AutoSize = true;
            this.lblDashboardDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblDashboardDescription.Location = new System.Drawing.Point(21, 147);
            this.lblDashboardDescription.Name = "lblDashboardDescription";
            this.lblDashboardDescription.Size = new System.Drawing.Size(145, 19);
            this.lblDashboardDescription.TabIndex = 3;
            this.lblDashboardDescription.Text = "Descripción (opcional):";
            // 
            // chkCreateDefaultCategory
            // 
            this.chkCreateDefaultCategory.AutoSize = true;
            this.chkCreateDefaultCategory.Checked = true;
            this.chkCreateDefaultCategory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateDefaultCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCreateDefaultCategory.Location = new System.Drawing.Point(21, 245);
            this.chkCreateDefaultCategory.Name = "chkCreateDefaultCategory";
            this.chkCreateDefaultCategory.Size = new System.Drawing.Size(195, 23);
            this.chkCreateDefaultCategory.TabIndex = 5;
            this.chkCreateDefaultCategory.Text = "Crear categoría por defecto";
            this.chkCreateDefaultCategory.UseVisualStyleBackColor = true;
            this.chkCreateDefaultCategory.CheckedChanged += new System.EventHandler(this.chkCreateDefaultCategory_CheckedChanged);
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblCategoryName.Location = new System.Drawing.Point(21, 280);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(141, 19);
            this.lblCategoryName.TabIndex = 6;
            this.lblCategoryName.Text = "Nombre de categoría:";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategoryName.Location = new System.Drawing.Point(21, 302);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(558, 25);
            this.txtCategoryName.TabIndex = 7;
            this.txtCategoryName.Text = "Status";
            // 
            // lblTagName
            // 
            this.lblTagName.AutoSize = true;
            this.lblTagName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTagName.Location = new System.Drawing.Point(21, 337);
            this.lblTagName.Name = "lblTagName";
            this.lblTagName.Size = new System.Drawing.Size(135, 19);
            this.lblTagName.TabIndex = 8;
            this.lblTagName.Text = "Nombre de etiqueta:";
            // 
            // txtTagName
            // 
            this.txtTagName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTagName.Location = new System.Drawing.Point(21, 359);
            this.txtTagName.Name = "txtTagName";
            this.txtTagName.Size = new System.Drawing.Size(450, 25);
            this.txtTagName.TabIndex = 9;
            // 
            // btnAddTag
            // 
            this.btnAddTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnAddTag.FlatAppearance.BorderSize = 0;
            this.btnAddTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTag.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddTag.ForeColor = System.Drawing.Color.White;
            this.btnAddTag.Location = new System.Drawing.Point(477, 359);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(102, 28);
            this.btnAddTag.TabIndex = 10;
            this.btnAddTag.Text = "Agregar";
            this.btnAddTag.UseVisualStyleBackColor = false;
            // 
            // listBoxTags
            // 
            this.listBoxTags.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxTags.FormattingEnabled = true;
            this.listBoxTags.ItemHeight = 17;
            this.listBoxTags.Items.AddRange(new object[] {
            "Sin iniciar",
            "En progreso",
            "Finalizada"});
            this.listBoxTags.Location = new System.Drawing.Point(21, 401);
            this.listBoxTags.Name = "listBoxTags";
            this.listBoxTags.Size = new System.Drawing.Size(558, 106);
            this.listBoxTags.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCancel.Location = new System.Drawing.Point(469, 523);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "CANCELAR";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // DashboardsManagement
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(600, 580);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listBoxTags);
            this.Controls.Add(this.btnAddTag);
            this.Controls.Add(this.txtTagName);
            this.Controls.Add(this.lblTagName);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.chkCreateDefaultCategory);
            this.Controls.Add(this.txtDashboardDescription);
            this.Controls.Add(this.lblDashboardDescription);
            this.Controls.Add(this.lblCategoryTitle);
            this.Controls.Add(this.txtDashboardTitle);
            this.Controls.Add(this.btnSaveDashboard);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DashboardsManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganiTask - Crear Nuevo Tablero";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}