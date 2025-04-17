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
        private System.Windows.Forms.Button btnDeleteDashboard;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Panel panelHeader;
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
            this.btnDeleteDashboard = new System.Windows.Forms.Button();
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
            this.panelHeader.Size = new System.Drawing.Size(450, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(221, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Gestión de Dashboard";
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(21, 90);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(76, 19);
            this.lblCategoryTitle.TabIndex = 1;
            this.lblCategoryTitle.Text = "Dashboard";
            // 
            // txtDashboardTitle
            // 
            this.txtDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDashboardTitle.Location = new System.Drawing.Point(21, 112);
            this.txtDashboardTitle.Name = "txtDashboardTitle";
            this.txtDashboardTitle.Size = new System.Drawing.Size(410, 25);
            this.txtDashboardTitle.TabIndex = 2;
            // 
            // btnSaveDashboard
            // 
            this.btnSaveDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSaveDashboard.FlatAppearance.BorderSize = 0;
            this.btnSaveDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveDashboard.ForeColor = System.Drawing.Color.White;
            this.btnSaveDashboard.Location = new System.Drawing.Point(21, 157);
            this.btnSaveDashboard.Name = "btnSaveDashboard";
            this.btnSaveDashboard.Size = new System.Drawing.Size(200, 40);
            this.btnSaveDashboard.TabIndex = 3;
            this.btnSaveDashboard.Text = "GUARDAR";
            this.btnSaveDashboard.UseVisualStyleBackColor = false;
            this.btnSaveDashboard.Click += new System.EventHandler(this.btnSaveDashboard_Click);
            // 
            // btnDeleteDashboard
            // 
            this.btnDeleteDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteDashboard.Enabled = false;
            this.btnDeleteDashboard.FlatAppearance.BorderSize = 0;
            this.btnDeleteDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDashboard.Location = new System.Drawing.Point(231, 157);
            this.btnDeleteDashboard.Name = "btnDeleteDashboard";
            this.btnDeleteDashboard.Size = new System.Drawing.Size(200, 40);
            this.btnDeleteDashboard.TabIndex = 4;
            this.btnDeleteDashboard.Text = "ELIMINAR";
            this.btnDeleteDashboard.UseVisualStyleBackColor = false;
            // 
            // DashboardsManagement
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(450, 220);
            this.Controls.Add(this.lblCategoryTitle);
            this.Controls.Add(this.txtDashboardTitle);
            this.Controls.Add(this.btnSaveDashboard);
            this.Controls.Add(this.btnDeleteDashboard);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DashboardsManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganiTask - Gestión de Dashboard";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}