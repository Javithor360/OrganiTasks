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
            this.SuspendLayout();
            // 
            // flpBoard
            // 
            this.flpBoard.AutoScroll = true;
            this.flpBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpBoard.Location = new System.Drawing.Point(135, 79);
            this.flpBoard.Name = "flpBoard";
            this.flpBoard.Size = new System.Drawing.Size(853, 509);
            this.flpBoard.TabIndex = 0;
            this.flpBoard.WrapContents = false;
            // 
            // lblDashboardTitle
            // 
            this.lblDashboardTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDashboardTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardTitle.Location = new System.Drawing.Point(135, 18);
            this.lblDashboardTitle.Name = "lblDashboardTitle";
            this.lblDashboardTitle.Size = new System.Drawing.Size(853, 45);
            this.lblDashboardTitle.TabIndex = 1;
            this.lblDashboardTitle.Text = "label1";
            this.lblDashboardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDashboardBack
            // 
            this.btnDashboardBack.Location = new System.Drawing.Point(12, 546);
            this.btnDashboardBack.Name = "btnDashboardBack";
            this.btnDashboardBack.Size = new System.Drawing.Size(117, 42);
            this.btnDashboardBack.TabIndex = 2;
            this.btnDashboardBack.Text = "🢀 Regresar";
            this.btnDashboardBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboardBack.UseVisualStyleBackColor = true;
            // 
            // btnNewTask
            // 
            this.btnNewTask.Location = new System.Drawing.Point(12, 149);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(117, 43);
            this.btnNewTask.TabIndex = 3;
            this.btnNewTask.Text = "➕ Nueva tarea";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(12, 79);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(117, 43);
            this.btnSort.TabIndex = 4;
            this.btnSort.Text = "⇅ Ordenar tablero";
            this.btnSort.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 219);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(117, 43);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "⚙️ Configurar tablero";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnShowHidden
            // 
            this.btnShowHidden.Location = new System.Drawing.Point(12, 289);
            this.btnShowHidden.Name = "btnShowHidden";
            this.btnShowHidden.Size = new System.Drawing.Size(117, 43);
            this.btnShowHidden.TabIndex = 6;
            this.btnShowHidden.Text = "🔎 Mostrar ocultos";
            this.btnShowHidden.UseVisualStyleBackColor = true;
            this.btnShowHidden.Click += new System.EventHandler(this.btnShowHidden_Click);
            // 
            // KanbanDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.btnShowHidden);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnNewTask);
            this.Controls.Add(this.btnDashboardBack);
            this.Controls.Add(this.lblDashboardTitle);
            this.Controls.Add(this.flpBoard);
            this.Name = "KanbanDashboard";
            this.Text = "KanbanDashboard";
            this.Load += new System.EventHandler(this.KanbanDashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblDashboardTitle;
        private Button btnDashboardBack;
        private Button btnNewTask;
        private Button btnSort;
        private Button btnSettings;
        private Button btnShowHidden;
    }
}