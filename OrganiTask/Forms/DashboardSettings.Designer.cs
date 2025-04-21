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
        private TextBox txtHeader;
        private TextBox txtDescription;
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
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.pnlCreator = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCreatorKey = new System.Windows.Forms.Label();
            this.lblCreatorValue = new System.Windows.Forms.Label();
            this.lblDescKey = new System.Windows.Forms.Label();
            this.lblDescText = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblCategoriesHeader = new System.Windows.Forms.Label();
            this.tblCategories = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.flpMain.SuspendLayout();
            this.pnlCreator.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.Controls.Add(this.lblHeader);
            this.flpMain.Controls.Add(this.txtHeader);
            this.flpMain.Controls.Add(this.pnlCreator);
            this.flpMain.Controls.Add(this.lblDescKey);
            this.flpMain.Controls.Add(this.lblDescText);
            this.flpMain.Controls.Add(this.txtDescription);
            this.flpMain.Controls.Add(this.lblCategoriesHeader);
            this.flpMain.Controls.Add(this.tblCategories);
            this.flpMain.Controls.Add(this.btnEdit);
            this.flpMain.Controls.Add(this.btnSave);
            this.flpMain.Controls.Add(this.btnCancel);
            this.flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Size = new System.Drawing.Size(500, 600);
            this.flpMain.TabIndex = 0;
            this.flpMain.WrapContents = false;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(23, 20);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(440, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Configuración del Tablero";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHeader
            // 
            this.txtHeader.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtHeader.Location = new System.Drawing.Point(23, 53);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(440, 29);
            this.txtHeader.TabIndex = 1;
            this.txtHeader.Visible = false;
            // 
            // pnlCreator
            // 
            this.pnlCreator.AutoSize = true;
            this.pnlCreator.Controls.Add(this.lblCreatorKey);
            this.pnlCreator.Controls.Add(this.lblCreatorValue);
            this.pnlCreator.Location = new System.Drawing.Point(20, 95);
            this.pnlCreator.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlCreator.Name = "pnlCreator";
            this.pnlCreator.Size = new System.Drawing.Size(80, 19);
            this.pnlCreator.TabIndex = 2;
            this.pnlCreator.WrapContents = false;
            // 
            // lblCreatorKey
            // 
            this.lblCreatorKey.AutoSize = true;
            this.lblCreatorKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCreatorKey.Location = new System.Drawing.Point(3, 0);
            this.lblCreatorKey.Name = "lblCreatorKey";
            this.lblCreatorKey.Size = new System.Drawing.Size(69, 19);
            this.lblCreatorKey.TabIndex = 0;
            this.lblCreatorKey.Text = "Creador:";
            // 
            // lblCreatorValue
            // 
            this.lblCreatorValue.AutoSize = true;
            this.lblCreatorValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreatorValue.Location = new System.Drawing.Point(80, 0);
            this.lblCreatorValue.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblCreatorValue.Name = "lblCreatorValue";
            this.lblCreatorValue.Size = new System.Drawing.Size(0, 19);
            this.lblCreatorValue.TabIndex = 1;
            // 
            // lblDescKey
            // 
            this.lblDescKey.AutoSize = true;
            this.lblDescKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescKey.Location = new System.Drawing.Point(20, 124);
            this.lblDescKey.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblDescKey.Name = "lblDescKey";
            this.lblDescKey.Size = new System.Drawing.Size(91, 19);
            this.lblDescKey.TabIndex = 3;
            this.lblDescKey.Text = "Descripción:";
            // 
            // lblDescText
            // 
            this.lblDescText.AutoSize = true;
            this.lblDescText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescText.Location = new System.Drawing.Point(20, 145);
            this.lblDescText.Margin = new System.Windows.Forms.Padding(0, 2, 0, 10);
            this.lblDescText.MaximumSize = new System.Drawing.Size(440, 0);
            this.lblDescText.Name = "lblDescText";
            this.lblDescText.Size = new System.Drawing.Size(0, 19);
            this.lblDescText.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtDescription.Location = new System.Drawing.Point(23, 177);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(440, 100);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Visible = false;
            // 
            // lblCategoriesHeader
            // 
            this.lblCategoriesHeader.AutoSize = true;
            this.lblCategoriesHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoriesHeader.Location = new System.Drawing.Point(20, 290);
            this.lblCategoriesHeader.Margin = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.lblCategoriesHeader.Name = "lblCategoriesHeader";
            this.lblCategoriesHeader.Size = new System.Drawing.Size(91, 21);
            this.lblCategoriesHeader.TabIndex = 6;
            this.lblCategoriesHeader.Text = "Categorías";
            // 
            // tblCategories
            // 
            this.tblCategories.AutoSize = true;
            this.tblCategories.ColumnCount = 3;
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.AutoSize));
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.AutoSize));
            this.tblCategories.Location = new System.Drawing.Point(20, 321);
            this.tblCategories.Margin = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.tblCategories.Name = "tblCategories";
            this.tblCategories.Size = new System.Drawing.Size(0, 0);
            this.tblCategories.TabIndex = 7;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Location = new System.Drawing.Point(20, 336);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Editar";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(20, 364);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Guardar";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(20, 392);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DashboardSettings
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.flpMain);
            this.Name = "DashboardSettings";
            this.Text = "Configuración del Tablero";
            this.Load += new System.EventHandler(this.DashboardSettings_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.pnlCreator.ResumeLayout(false);
            this.pnlCreator.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}