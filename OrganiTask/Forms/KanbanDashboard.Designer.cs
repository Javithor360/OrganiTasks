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
            this.btnDashboardAdd = new System.Windows.Forms.Button();
            this.btnDashboardSort = new System.Windows.Forms.Button();
            this.btnDashboardSettings = new System.Windows.Forms.Button();
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
            // btnDashboardAdd
            // 
            this.btnDashboardAdd.Location = new System.Drawing.Point(12, 149);
            this.btnDashboardAdd.Name = "btnDashboardAdd";
            this.btnDashboardAdd.Size = new System.Drawing.Size(117, 43);
            this.btnDashboardAdd.TabIndex = 3;
            this.btnDashboardAdd.Text = "➕ Nueva tarea";
            this.btnDashboardAdd.UseVisualStyleBackColor = true;
            // 
            // btnDashboardSort
            // 
            this.btnDashboardSort.Location = new System.Drawing.Point(12, 79);
            this.btnDashboardSort.Name = "btnDashboardSort";
            this.btnDashboardSort.Size = new System.Drawing.Size(117, 43);
            this.btnDashboardSort.TabIndex = 4;
            this.btnDashboardSort.Text = "⇅ Ordenar tablero";
            this.btnDashboardSort.UseVisualStyleBackColor = true;
            // 
            // btnDashboardSettings
            // 
            this.btnDashboardSettings.Location = new System.Drawing.Point(12, 219);
            this.btnDashboardSettings.Name = "btnDashboardSettings";
            this.btnDashboardSettings.Size = new System.Drawing.Size(117, 43);
            this.btnDashboardSettings.TabIndex = 5;
            this.btnDashboardSettings.Text = "⚙️ Configurar tablero";
            this.btnDashboardSettings.UseVisualStyleBackColor = true;
            // 
            // KanbanDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.btnDashboardSettings);
            this.Controls.Add(this.btnDashboardSort);
            this.Controls.Add(this.btnDashboardAdd);
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
        private Button btnDashboardAdd;
        private Button btnDashboardSort;
        private Button btnDashboardSettings;
    }
}