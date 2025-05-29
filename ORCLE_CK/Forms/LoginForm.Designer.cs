using ORCLE_CK.Constants;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class LoginForm
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
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblTitle = new Label();
            this.SuspendLayout();

            // Form
            this.Text = $"Đăng nhập - {AppConstants.APP_NAME}";
            this.Size = new Size(480, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            // Title
            this.lblTitle.Text = AppConstants.APP_NAME;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(50, 30);
            this.lblTitle.Size = new Size(350, 30);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Username
            this.lblUsername.Text = "Tên đăng nhập:";
            this.lblUsername.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblUsername.Location = new Point(50, 90);
            this.lblUsername.Size = new Size(100, 20);

            this.txtUsername.Location = new Point(50, 115);
            this.txtUsername.Size = new Size(350, 25);
            this.txtUsername.Font = new Font("Microsoft Sans Serif", 10F);

            // Password
            this.lblPassword.Text = "Mật khẩu:";
            this.lblPassword.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblPassword.Location = new Point(50, 150);
            this.lblPassword.Size = new Size(100, 20);

            this.txtPassword.Location = new Point(50, 175);
            this.txtPassword.Size = new Size(350, 25);
            this.txtPassword.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtPassword.UseSystemPasswordChar = true;

            // Login Button
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Location = new Point(200, 220);
            this.btnLogin.Size = new Size(100, 35);
            this.btnLogin.Font = new Font("Microsoft Sans Serif", 10F);
            this.btnLogin.BackColor = Color.DodgerBlue;
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // Exit Button
            this.btnExit.Text = "Thoát";
            this.btnExit.Location = new Point(310, 220);
            this.btnExit.Size = new Size(90, 35);
            this.btnExit.Font = new Font("Microsoft Sans Serif", 10F);
            this.btnExit.BackColor = Color.Gray;
            this.btnExit.ForeColor = Color.White;
            this.btnExit.FlatStyle = FlatStyle.Flat;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);

            // Set default button and tab order
            this.AcceptButton = this.btnLogin;
            this.txtUsername.TabIndex = 0;
            this.txtPassword.TabIndex = 1;
            this.btnLogin.TabIndex = 2;
            this.btnExit.TabIndex = 3;

            this.ResumeLayout(false);
        }

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblTitle;

        #endregion
    }
}