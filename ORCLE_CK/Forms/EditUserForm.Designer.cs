using ORCLE_CK.Models;
using static ORCLE_CK.Forms.AddUserForm;
using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class EditUserForm
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
            this.txtFullName = new TextBox();
            this.txtUsername = new TextBox();
            this.txtEmail = new TextBox();
            this.cmbRole = new ComboBox();
            this.chkIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.btnChangePassword = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chỉnh sửa người dùng";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Full Name
            var lblFullName = new Label { Text = "Họ tên:", Location = new Point(20, 20), Size = new Size(100, 23) };
            this.txtFullName.Location = new Point(130, 20);
            this.txtFullName.Size = new Size(280, 23);

            // Username
            var lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(20, 60), Size = new Size(100, 23) };
            this.txtUsername.Location = new Point(130, 60);
            this.txtUsername.Size = new Size(280, 23);

            // Email
            var lblEmail = new Label { Text = "Email:", Location = new Point(20, 100), Size = new Size(100, 23) };
            this.txtEmail.Location = new Point(130, 100);
            this.txtEmail.Size = new Size(280, 23);

            // Role
            var lblRole = new Label { Text = "Vai trò:", Location = new Point(20, 140), Size = new Size(100, 23) };
            this.cmbRole.Location = new Point(130, 140);
            this.cmbRole.Size = new Size(280, 23);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.Add(new ComboBoxItem("Học viên", UserRole.Student));
            this.cmbRole.Items.Add(new ComboBoxItem("Giảng viên", UserRole.Instructor));
            this.cmbRole.Items.Add(new ComboBoxItem("Quản trị viên", UserRole.Admin));
            this.cmbRole.DisplayMember = "Text";
            this.cmbRole.ValueMember = "Value";

            // Is Active
            this.chkIsActive.Text = "Tài khoản hoạt động";
            this.chkIsActive.Location = new Point(130, 180);
            this.chkIsActive.Size = new Size(200, 23);

            // Change Password Button
            this.btnChangePassword.Text = "Đổi mật khẩu";
            this.btnChangePassword.Location = new Point(130, 220);
            this.btnChangePassword.Size = new Size(120, 30);
            this.btnChangePassword.BackColor = Color.Orange;
            this.btnChangePassword.ForeColor = Color.White;
            this.btnChangePassword.Click += BtnChangePassword_Click;

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(250, 300);
            this.btnSave.Size = new Size(75, 30);
            this.btnSave.BackColor = Color.Green;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(335, 300);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}