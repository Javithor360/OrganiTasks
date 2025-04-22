using System.Drawing;
using System.Windows.Forms;

namespace OrganiTask.Forms
{
    partial class TaskDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flpMain;
        private Label lblTitle;
        private Label lblDesc;
        private TableLayoutPanel tblDetails;
        private Button btnEdit;
        private Button btnSave;
        private Button btnCancel;

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
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.flpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.flpMain.Controls.Add(this.lblTitle);
            this.flpMain.Controls.Add(this.lblDesc);
            this.flpMain.Controls.Add(this.tblDetails);
            this.flpMain.Controls.Add(this.btnSave);
            this.flpMain.Controls.Add(this.btnEdit);
            this.flpMain.Controls.Add(this.btnDelete);
            this.flpMain.Controls.Add(this.btnCancel);
            this.flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Size = new System.Drawing.Size(484, 381);
            this.flpMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(23, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(449, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "lblTitle";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesc
            // 
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDesc.Location = new System.Drawing.Point(23, 50);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(449, 60);
            this.lblDesc.TabIndex = 0;
            this.lblDesc.Text = "lblDesc";
            // 
            // tblDetails
            // 
            this.tblDetails.AutoSize = true;
            this.tblDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tblDetails.ColumnCount = 2;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblDetails.Location = new System.Drawing.Point(20, 120);
            this.tblDetails.Margin = new System.Windows.Forms.Padding(0, 10, 0, 20);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDetails.Size = new System.Drawing.Size(0, 0);
            this.tblDetails.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(23, 195);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(445, 40);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(23, 140);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(445, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCancel.Location = new System.Drawing.Point(23, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(445, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(23, 250);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(445, 40);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // TaskDetails
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 381);
            this.Controls.Add(this.flpMain);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "TaskDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalles de la Tarea";
            this.Load += new System.EventHandler(this.TaskDetails_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnDelete;
    }
}