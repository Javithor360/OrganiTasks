using System.Drawing;
using System.Windows.Forms;
using System;

namespace OrganiTask.Forms
{
    partial class DashboardSettings
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel flpMain;
        private FlowLayoutPanel pnlCreator;
        private Label lblHeader;
        private Label lblCreatorKey;
        private Label lblCreatorValue;
        private Label lblDescKey;
        private Label lblDescText;
        private Label lblCategoriesHeader;
        private TableLayoutPanel tblCategories;
        private Button btnEdit;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblCreatorKey = new System.Windows.Forms.Label();
            this.lblCreatorValue = new System.Windows.Forms.Label();
            this.lblDescKey = new System.Windows.Forms.Label();
            this.lblDescText = new System.Windows.Forms.Label();
            this.lblCategoriesHeader = new System.Windows.Forms.Label();
            this.tblCategories = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlCreator = new System.Windows.Forms.FlowLayoutPanel();

            this.flpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.WrapContents = false;
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Controls.Add(this.lblHeader);
            this.flpMain.Controls.Add(this.pnlCreator);
            this.flpMain.Controls.Add(this.lblDescKey);
            this.flpMain.Controls.Add(this.lblDescText);
            this.flpMain.Controls.Add(this.lblCategoriesHeader);
            this.flpMain.Controls.Add(this.tblCategories);
            this.flpMain.Controls.Add(this.btnEdit);
            this.flpMain.Controls.Add(this.btnSave);
            this.flpMain.Controls.Add(this.btnCancel);
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Size = new System.Drawing.Size(440, 30);
            this.lblHeader.Text = "Configuración del Tablero";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCreator
            // 
            this.pnlCreator.AutoSize = true;
            this.pnlCreator.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlCreator.WrapContents = false;
            this.pnlCreator.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlCreator.Controls.Add(this.lblCreatorKey);
            this.pnlCreator.Controls.Add(this.lblCreatorValue);
            // 
            // lblCreatorKey
            // 
            this.lblCreatorKey.AutoSize = true;
            this.lblCreatorKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCreatorKey.Text = "Creador:";
            // 
            // lblCreatorValue
            // 
            this.lblCreatorValue.AutoSize = true;
            this.lblCreatorValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCreatorValue.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblCreatorValue.Name = "lblCreatorValue";
            // 
            // lblDescKey
            // 
            this.lblDescKey.AutoSize = true;
            this.lblDescKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescKey.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblDescKey.Text = "Descripción:";
            // 
            // lblDescText
            // 
            this.lblDescText.AutoSize = true;
            this.lblDescText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblDescText.Margin = new System.Windows.Forms.Padding(0, 2, 0, 10);
            this.lblDescText.MaximumSize = new System.Drawing.Size(440, 0);
            this.lblDescText.Text = "";
            // 
            // lblCategoriesHeader
            // 
            this.lblCategoriesHeader.AutoSize = true;
            this.lblCategoriesHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoriesHeader.Margin = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblCategoriesHeader.Text = "Categorías";
            // 
            // tblCategories
            // 
            this.tblCategories.AutoSize = true;
            this.tblCategories.ColumnCount = 2;
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblCategories.Location = new System.Drawing.Point(20, 240);
            this.tblCategories.Margin = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.tblCategories.Name = "tblCategories";
            this.tblCategories.RowStyles.Clear();
            this.tblCategories.Size = new System.Drawing.Size(440, 0);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Text = "Editar";
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Text = "Guardar";
            this.btnSave.Visible = false;
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Visible = false;
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            // 
            // DashboardSettings
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.flpMain);
            this.Text = "Configuración del Tablero";
            this.Load += new System.EventHandler(this.DashboardSettings_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}