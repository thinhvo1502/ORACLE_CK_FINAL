using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class ChangePasswordForm
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
            this.txtCurrentPassword = new TextBox();
            this.txtNewPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.btnGeneratePassword = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Đổi mật khẩu";
            this.Size = new Size(500, isAdminMode ? 350 : 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            // Content Panel
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            int yPos = 20;

            // Current Password (only if not admin mode)
            if (!isAdminMode)
            {
                var lblCurrentPassword = new Label 
                { 
                    Text = "Mật khẩu hiện tại:", 
                    Location = new Point(20, yPos), 
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                this.txtCurrentPassword.Location = new Point(150, yPos);
                this.txtCurrentPassword.Size = new Size(280, 25);
                this.txtCurrentPassword.Font = new Font("Segoe UI", 10);
                this.txtCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
                this.txtCurrentPassword.UseSystemPasswordChar = true;

                contentPanel.Controls.Add(lblCurrentPassword);
                contentPanel.Controls.Add(this.txtCurrentPassword);
                yPos += 40;
            }

            // New Password
            var lblNewPassword = new Label 
            { 
                Text = "Mật khẩu mới:", 
                Location = new Point(20, yPos), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtNewPassword.Location = new Point(150, yPos);
            this.txtNewPassword.Size = new Size(280, 25);
            this.txtNewPassword.Font = new Font("Segoe UI", 10);
            this.txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtNewPassword.UseSystemPasswordChar = true;
            yPos += 40;

            // Confirm Password
            var lblConfirmPassword = new Label 
            { 
                Text = "Xác nhận mật khẩu:", 
                Location = new Point(20, yPos), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtConfirmPassword.Location = new Point(150, yPos);
            this.txtConfirmPassword.Size = new Size(280, 25);
            this.txtConfirmPassword.Font = new Font("Segoe UI", 10);
            this.txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            yPos += 40;

            // Generate Password Button (admin mode only)
            if (isAdminMode)
            {
                this.btnGeneratePassword.Text = "Tạo mật khẩu";
                this.btnGeneratePassword.Location = new Point(150, yPos);
                this.btnGeneratePassword.Size = new Size(150, 35);
                this.btnGeneratePassword.BackColor = Color.FromArgb(33, 150, 243);
                this.btnGeneratePassword.ForeColor = Color.White;
                this.btnGeneratePassword.FlatStyle = FlatStyle.Flat;
                this.btnGeneratePassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                this.btnGeneratePassword.Cursor = Cursors.Hand;
                this.btnGeneratePassword.Click += BtnGeneratePassword_Click;
                contentPanel.Controls.Add(this.btnGeneratePassword);
                yPos += 45;
            }

            // Buttons Panel
            var buttonPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(280, 15);
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.BackColor = Color.FromArgb(0, 120, 215);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(390, 15);
            this.btnCancel.Size = new Size(100, 35);
            this.btnCancel.BackColor = Color.FromArgb(96, 125, 139);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += BtnCancel_Click;

            buttonPanel.Controls.AddRange(new Control[] { this.btnSave, this.btnCancel });

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] 
            { 
                lblNewPassword, this.txtNewPassword,
                lblConfirmPassword, this.txtConfirmPassword
            });

            // Add panels to form
            this.Controls.AddRange(new Control[] { contentPanel, buttonPanel });

            this.ResumeLayout(false);
        }

        #endregion
    }
}