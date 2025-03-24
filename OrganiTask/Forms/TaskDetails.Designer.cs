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
            this.flpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoScroll = true;
            this.flpMain.Controls.Add(this.lblTitle);
            this.flpMain.Controls.Add(this.lblDesc);
            this.flpMain.Controls.Add(this.tblDetails);
            this.flpMain.Controls.Add(this.btnEdit);
            this.flpMain.Controls.Add(this.btnSave);
            this.flpMain.Controls.Add(this.btnCancel);
            this.flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Padding = new System.Windows.Forms.Padding(20);
            this.flpMain.Size = new System.Drawing.Size(484, 361);
            this.flpMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
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
            this.lblDesc.Location = new System.Drawing.Point(23, 50);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(449, 60);
            this.lblDesc.TabIndex = 0;
            this.lblDesc.Text = "lblDesc";
            // 
            // tblDetails
            // 
            this.tblDetails.AutoSize = true;
            this.tblDetails.ColumnCount = 2;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblDetails.Location = new System.Drawing.Point(20, 120);
            this.tblDetails.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDetails.Size = new System.Drawing.Size(0, 0);
            this.tblDetails.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Location = new System.Drawing.Point(23, 133);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Editar";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(23, 162);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Guardar";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(23, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // TaskDetails
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.flpMain);
            this.Name = "TaskDetails";
            this.Text = "Detalles de la Tarea";
            this.Load += new System.EventHandler(this.TaskDetails_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}