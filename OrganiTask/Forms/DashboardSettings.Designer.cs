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
        private Label lblCreatorKey;
        private Label lblCreatorValue;
        private Label lblDescKey;
        private Label lblDescText;
        private Label lblCategoriesHeader;
        private TableLayoutPanel tblCategories;
        private Button btnEdit;
        private Button btnSave;
        private Button btnCancel;
        private Panel pnlHeader;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCreator = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCreatorKey = new System.Windows.Forms.Label();
            this.lblCreatorValue = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.lblDescKey = new System.Windows.Forms.Label();
            this.lblDescText = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblCategoriesHeader = new System.Windows.Forms.Label();
            this.tblCategories = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.flpMain.SuspendLayout();
            this.pnlCreator.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.flpMain.Controls.Add(this.pnlCreator);
            this.flpMain.Controls.Add(this.txtHeader);
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
            this.flpMain.Location = new System.Drawing.Point(0, 70);
            this.flpMain.Name = "flpMain";
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Size = new System.Drawing.Size(484, 583);
            this.flpMain.TabIndex = 0;
            this.flpMain.WrapContents = false;
            // 
            // pnlCreator
            // 
            this.pnlCreator.AutoSize = true;
            this.pnlCreator.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreator.Controls.Add(this.lblCreatorKey);
            this.pnlCreator.Controls.Add(this.lblCreatorValue);
            this.pnlCreator.Location = new System.Drawing.Point(20, 20);
            this.pnlCreator.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.pnlCreator.Name = "pnlCreator";
            this.pnlCreator.Size = new System.Drawing.Size(80, 19);
            this.pnlCreator.TabIndex = 2;
            this.pnlCreator.WrapContents = false;
            // 
            // lblCreatorKey
            // 
            this.lblCreatorKey.AutoSize = true;
            this.lblCreatorKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCreatorKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
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
            this.lblCreatorValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCreatorValue.Location = new System.Drawing.Point(80, 0);
            this.lblCreatorValue.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblCreatorValue.Name = "lblCreatorValue";
            this.lblCreatorValue.Size = new System.Drawing.Size(0, 19);
            this.lblCreatorValue.TabIndex = 1;
            // 
            // txtHeader
            // 
            this.txtHeader.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHeader.Location = new System.Drawing.Point(23, 54);
            this.txtHeader.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(440, 25);
            this.txtHeader.TabIndex = 1;
            this.txtHeader.Visible = false;
            // 
            // lblDescKey
            // 
            this.lblDescKey.AutoSize = true;
            this.lblDescKey.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblDescKey.Location = new System.Drawing.Point(20, 94);
            this.lblDescKey.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblDescKey.Name = "lblDescKey";
            this.lblDescKey.Size = new System.Drawing.Size(91, 19);
            this.lblDescKey.TabIndex = 3;
            this.lblDescKey.Text = "Descripción:";
            // 
            // lblDescText
            // 
            this.lblDescText.AutoSize = true;
            this.lblDescText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDescText.Location = new System.Drawing.Point(20, 118);
            this.lblDescText.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblDescText.MaximumSize = new System.Drawing.Size(440, 0);
            this.lblDescText.Name = "lblDescText";
            this.lblDescText.Size = new System.Drawing.Size(0, 19);
            this.lblDescText.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(23, 152);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(440, 100);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.Visible = false;
            // 
            // lblCategoriesHeader
            // 
            this.lblCategoriesHeader.AutoSize = true;
            this.lblCategoriesHeader.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblCategoriesHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblCategoriesHeader.Location = new System.Drawing.Point(20, 267);
            this.lblCategoriesHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblCategoriesHeader.Name = "lblCategoriesHeader";
            this.lblCategoriesHeader.Size = new System.Drawing.Size(112, 28);
            this.lblCategoriesHeader.TabIndex = 6;
            this.lblCategoriesHeader.Text = "Categorías";
            // 
            // tblCategories
            // 
            this.tblCategories.AutoSize = true;
            this.tblCategories.ColumnCount = 3;
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCategories.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCategories.Location = new System.Drawing.Point(20, 305);
            this.tblCategories.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.tblCategories.Name = "tblCategories";
            this.tblCategories.Size = new System.Drawing.Size(0, 0);
            this.tblCategories.TabIndex = 7;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(20, 325);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(443, 40);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 375);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(443, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCancel.Location = new System.Drawing.Point(20, 425);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(443, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(484, 70);
            this.pnlHeader.TabIndex = 11;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(484, 70);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Configuración del Tablero";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DashboardSettings
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 653);
            this.Controls.Add(this.flpMain);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DashboardSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganiTask - Configuración del Tablero";
            this.Load += new System.EventHandler(this.DashboardSettings_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.pnlCreator.ResumeLayout(false);
            this.pnlCreator.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Label lblHeader;
    }
}