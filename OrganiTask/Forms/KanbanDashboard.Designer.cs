using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    partial class KanbanDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flpBoard;

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
            this.flpBoard = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDashboardTitle = new System.Windows.Forms.Label();
            this.btnDashboardBack = new System.Windows.Forms.Button();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnShowHidden = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.pnlHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpBoard
            // 
            this.flpBoard.AutoScroll = true;
            this.flpBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.flpBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBoard.Location = new System.Drawing.Point(250, 70);
            this.flpBoard.Name = "flpBoard";
            this.flpBoard.Padding = new System.Windows.Forms.Padding(20);
            this.flpBoard.Size = new System.Drawing.Size(750, 530);
            this.flpBoard.TabIndex = 0;
            this.flpBoard.WrapContents = false;
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDashboardTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblDashboardTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(1000, 70);
            this.lblDashboardTitle.TabIndex = 1;
            this.lblDashboardTitle.Text = "Tablero Kanban";
            this.lblDashboardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDashboardBack
            // 
            this.btnDashboardBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDashboardBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnDashboardBack.FlatAppearance.BorderSize = 0;
            this.btnDashboardBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboardBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboardBack.ForeColor = System.Drawing.Color.White;
            this.btnDashboardBack.Location = new System.Drawing.Point(20, 475);
            this.btnDashboardBack.Name = "btnDashboardBack";
            this.btnDashboardBack.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDashboardBack.Size = new System.Drawing.Size(210, 40);
            this.btnDashboardBack.TabIndex = 2;
            this.btnDashboardBack.Text = "🢀 Regresar";
            this.btnDashboardBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboardBack.UseVisualStyleBackColor = false;
            // 
            // btnNewTask
            // 
            this.btnNewTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnNewTask.FlatAppearance.BorderSize = 0;
            this.btnNewTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewTask.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewTask.ForeColor = System.Drawing.Color.White;
            this.btnNewTask.Location = new System.Drawing.Point(20, 120);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnNewTask.Size = new System.Drawing.Size(210, 40);
            this.btnNewTask.TabIndex = 3;
            this.btnNewTask.Text = "➕ Nueva tarea";
            this.btnNewTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewTask.UseVisualStyleBackColor = false;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSort.FlatAppearance.BorderSize = 0;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSort.ForeColor = System.Drawing.Color.White;
            this.btnSort.Location = new System.Drawing.Point(20, 60);
            this.btnSort.Name = "btnSort";
            this.btnSort.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnSort.Size = new System.Drawing.Size(210, 40);
            this.btnSort.TabIndex = 4;
            this.btnSort.Text = "⇅ Ordenar tablero";
            this.btnSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(20, 180);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnSettings.Size = new System.Drawing.Size(210, 40);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "⚙️ Configurar tablero";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnShowHidden
            // 
            this.btnShowHidden.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnShowHidden.FlatAppearance.BorderSize = 0;
            this.btnShowHidden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHidden.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowHidden.ForeColor = System.Drawing.Color.White;
            this.btnShowHidden.Location = new System.Drawing.Point(20, 240);
            this.btnShowHidden.Name = "btnShowHidden";
            this.btnShowHidden.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnShowHidden.Size = new System.Drawing.Size(210, 40);
            this.btnShowHidden.TabIndex = 6;
            this.btnShowHidden.Text = "🔎 Mostrar ocultos";
            this.btnShowHidden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowHidden.UseVisualStyleBackColor = false;
            this.btnShowHidden.Click += new System.EventHandler(this.btnShowHidden_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblDashboardTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 70);
            this.pnlHeader.TabIndex = 7;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlSidebar.Controls.Add(this.btnNewTask);
            this.pnlSidebar.Controls.Add(this.btnSort);
            this.pnlSidebar.Controls.Add(this.cboSort);
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.btnShowHidden);
            this.pnlSidebar.Controls.Add(this.btnDashboardBack);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 70);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(250, 530);
            this.pnlSidebar.TabIndex = 8;
            // 
            // cboSort
            // 
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Font = this.btnSort.Font;
            this.cboSort.FormattingEnabled = true;
            this.cboSort.Location = this.btnSort.Location;
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = this.btnSort.Size;
            this.cboSort.TabIndex = this.btnSort.TabIndex;
            this.cboSort.Visible = false;
            // 
            // KanbanDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.flpBoard);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "KanbanDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganiTask - Tablero Kanban";
            this.Load += new System.EventHandler(this.KanbanDashboard_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblDashboardTitle;
        private Button btnDashboardBack;
        private Button btnNewTask;
        private Button btnSort;
        private Button btnSettings;
        private Button btnShowHidden;
        private Panel pnlHeader;
        private Panel pnlSidebar;
        private ComboBox cboSort;
    }
}