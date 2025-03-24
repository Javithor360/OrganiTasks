namespace OrganiTask.Forms
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panelSide = new System.Windows.Forms.Panel();
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.checkBoxRemember = new System.Windows.Forms.CheckBox();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.linkForgotPassword = new System.Windows.Forms.LinkLabel();
            this.linkSignInAccount = new System.Windows.Forms.LinkLabel();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.panelSide.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSide
            // 
            this.panelSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelSide.Controls.Add(this.labelAppName);
            this.panelSide.Controls.Add(this.labelWelcome);
            this.panelSide.Location = new System.Drawing.Point(400, 0);
            this.panelSide.Name = "panelSide";
            this.panelSide.Size = new System.Drawing.Size(250, 450);
            this.panelSide.TabIndex = 0;
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.ForeColor = System.Drawing.Color.White;
            this.labelAppName.Location = new System.Drawing.Point(55, 155);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(160, 37);
            this.labelAppName.TabIndex = 1;
            this.labelAppName.Text = "OrganiTask";
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.Color.White;
            this.labelWelcome.Location = new System.Drawing.Point(58, 195);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(145, 21);
            this.labelWelcome.TabIndex = 2;
            this.labelWelcome.Text = "Comienza con éxito";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.labelEmail);
            this.panelMain.Controls.Add(this.labelLogin);
            this.panelMain.Controls.Add(this.textEmail);
            this.panelMain.Controls.Add(this.panelSide);
            this.panelMain.Controls.Add(this.labelUsername);
            this.panelMain.Controls.Add(this.textBoxUsername);
            this.panelMain.Controls.Add(this.labelPassword);
            this.panelMain.Controls.Add(this.textBoxPassword);
            this.panelMain.Controls.Add(this.checkBoxRemember);
            this.panelMain.Controls.Add(this.buttonRegister);
            this.panelMain.Controls.Add(this.linkForgotPassword);
            this.panelMain.Controls.Add(this.linkSignInAccount);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(650, 450);
            this.panelMain.TabIndex = 1;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.labelLogin.Location = new System.Drawing.Point(140, 85);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(118, 28);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Registrarse";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.labelUsername.Location = new System.Drawing.Point(50, 121);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(56, 19);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "Usuario";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsername.Location = new System.Drawing.Point(50, 141);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(300, 25);
            this.textBoxUsername.TabIndex = 2;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.labelPassword.Location = new System.Drawing.Point(50, 226);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(79, 19);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Contraseña";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(50, 246);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '•';
            this.textBoxPassword.Size = new System.Drawing.Size(300, 25);
            this.textBoxPassword.TabIndex = 4;
            // 
            // checkBoxRemember
            // 
            this.checkBoxRemember.AutoSize = true;
            this.checkBoxRemember.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRemember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.checkBoxRemember.Location = new System.Drawing.Point(50, 286);
            this.checkBoxRemember.Name = "checkBoxRemember";
            this.checkBoxRemember.Size = new System.Drawing.Size(115, 19);
            this.checkBoxRemember.TabIndex = 5;
            this.checkBoxRemember.Text = "Recordar usuario";
            this.checkBoxRemember.UseVisualStyleBackColor = true;
            this.checkBoxRemember.Visible = false;
            // 
            // buttonRegister
            // 
            this.buttonRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.buttonRegister.FlatAppearance.BorderSize = 0;
            this.buttonRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegister.ForeColor = System.Drawing.Color.White;
            this.buttonRegister.Location = new System.Drawing.Point(50, 311);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(300, 40);
            this.buttonRegister.TabIndex = 6;
            this.buttonRegister.Text = "REGISTRARSE";
            this.buttonRegister.UseVisualStyleBackColor = false;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // linkForgotPassword
            // 
            this.linkForgotPassword.AutoSize = true;
            this.linkForgotPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkForgotPassword.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.linkForgotPassword.Location = new System.Drawing.Point(200, 286);
            this.linkForgotPassword.Name = "linkForgotPassword";
            this.linkForgotPassword.Size = new System.Drawing.Size(141, 15);
            this.linkForgotPassword.TabIndex = 7;
            this.linkForgotPassword.TabStop = true;
            this.linkForgotPassword.Text = "¿Olvidaste tu contraseña?";
            this.linkForgotPassword.Visible = false;
            // 
            // linkSignInAccount
            // 
            this.linkSignInAccount.AutoSize = true;
            this.linkSignInAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSignInAccount.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.linkSignInAccount.Location = new System.Drawing.Point(128, 364);
            this.linkSignInAccount.Name = "linkSignInAccount";
            this.linkSignInAccount.Size = new System.Drawing.Size(148, 15);
            this.linkSignInAccount.TabIndex = 8;
            this.linkSignInAccount.TabStop = true;
            this.linkSignInAccount.Text = "Inicia Sesión con tu cuenta";
            this.linkSignInAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSignInAccount_LinkClicked);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.labelEmail.Location = new System.Drawing.Point(50, 175);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(51, 19);
            this.labelEmail.TabIndex = 11;
            this.labelEmail.Text = "Correo";
            // 
            // textEmail
            // 
            this.textEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEmail.Location = new System.Drawing.Point(50, 195);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(300, 25);
            this.textEmail.TabIndex = 3;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganiTask - Registro";
            this.panelSide.ResumeLayout(false);
            this.panelSide.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSide;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.CheckBox checkBoxRemember;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.LinkLabel linkForgotPassword;
        private System.Windows.Forms.LinkLabel linkSignInAccount;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textEmail;
    }
}