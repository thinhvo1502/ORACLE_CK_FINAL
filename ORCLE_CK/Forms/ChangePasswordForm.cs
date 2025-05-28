using ORCLE_CK.Services;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class ChangePasswordForm : Form
    {
        private readonly UserService userService;
        private readonly int userId;
        private readonly bool isAdminMode;

        private TextBox txtCurrentPassword;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnSave;
        private Button btnCancel;
        private Button btnGeneratePassword;

        public ChangePasswordForm(int userId, bool isAdminMode = false)
        {
            this.userId = userId;
            this.isAdminMode = isAdminMode;
            userService = new UserService();
            InitializeComponent();
        }
        private void BtnGeneratePassword_Click(object sender, EventArgs e)
        {
            var newPassword = PasswordHelper.GenerateRandomPassword(12);
            txtNewPassword.Text = newPassword;
            txtConfirmPassword.Text = newPassword;

            MessageBox.Show($"Mật khẩu được tạo: {newPassword}\nVui lòng sao chép và gửi cho người dùng.",
                "Mật khẩu mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text.Length < AppSettings.PasswordMinLength)
                {
                    MessageBox.Show($"Mật khẩu phải có ít nhất {AppSettings.PasswordMinLength} ký tự!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success;
                if (isAdminMode)
                {
                    // Admin can change password without current password
                    var newPasswordHash = PasswordHelper.HashPassword(txtNewPassword.Text);
                    success = userService.ChangePasswordByAdmin(userId, newPasswordHash);
                }
                else
                {
                    // Regular user must provide current password
                    success = userService.ChangePassword(userId, txtCurrentPassword.Text, txtNewPassword.Text);
                }

                if (success)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể đổi mật khẩu!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
