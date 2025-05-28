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
            this.Size = new Size(400, isAdminMode ? 300 : 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int yPos = 20;

            // Current Password (only if not admin mode)
            if (!isAdminMode)
            {
                var lblCurrentPassword = new Label { Text = "Mật khẩu hiện tại:", Location = new Point(20, yPos), Size = new Size(120, 23) };
                this.txtCurrentPassword.Location = new Point(150, yPos);
                this.txtCurrentPassword.Size = new Size(200, 23);
                this.txtCurrentPassword.UseSystemPasswordChar = true;

                this.Controls.Add(lblCurrentPassword);
                this.Controls.Add(this.txtCurrentPassword);
                yPos += 40;
            }

            // New Password
            var lblNewPassword = new Label { Text = "Mật khẩu mới:", Location = new Point(20, yPos), Size = new Size(120, 23) };
            this.txtNewPassword.Location = new Point(150, yPos);
            this.txtNewPassword.Size = new Size(200, 23);
            this.txtNewPassword.UseSystemPasswordChar = true;
            yPos += 40;

            // Confirm Password
            var lblConfirmPassword = new Label { Text = "Xác nhận mật khẩu:", Location = new Point(20, yPos), Size = new Size(120, 23) };
            this.txtConfirmPassword.Location = new Point(150, yPos);
            this.txtConfirmPassword.Size = new Size(200, 23);
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            yPos += 40;

            // Generate Password Button (admin mode only)
            if (isAdminMode)
            {
                this.btnGeneratePassword.Text = "Tạo mật khẩu";
                this.btnGeneratePassword.Location = new Point(150, yPos);
                this.btnGeneratePassword.Size = new Size(100, 25);
                this.btnGeneratePassword.BackColor = Color.Blue;
                this.btnGeneratePassword.ForeColor = Color.White;
                this.btnGeneratePassword.Click += BtnGeneratePassword_Click;
                this.Controls.Add(this.btnGeneratePassword);
                yPos += 35;
            }

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(200, yPos + 20);
            this.btnSave.Size = new Size(75, 30);
            this.btnSave.BackColor = Color.Green;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(285, yPos + 20);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblNewPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}