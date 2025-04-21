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
        private FlowLayoutPanel flpMain;
        private Label lblHeader;
        private FlowLayoutPanel flpTags;
        private Button btnClose;

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
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.flpTags = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.flpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.Controls.Add(this.lblHeader);
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
            // flpTags
            // 
            this.flpTags.AutoScroll = true;
            this.flpTags.Location = new System.Drawing.Point(20, 70);
            this.flpTags.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.flpTags.Name = "flpTags";
            this.flpTags.Size = new System.Drawing.Size(760, 300);
            this.flpTags.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Location = new System.Drawing.Point(20, 413);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Cerrar";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
    }
}
