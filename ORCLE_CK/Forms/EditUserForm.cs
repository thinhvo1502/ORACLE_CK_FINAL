using ORCLE_CK.Models;
using ORCLE_CK.Services;
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
    public partial class EditUserForm : Form
    {
        private readonly UserService userService;
        private readonly User userToEdit;

        private TextBox txtFullName;
        private TextBox txtUsername;
        private TextBox txtEmail;
        private ComboBox cmbRole;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;
        private Button btnChangePassword;

        public EditUserForm(User user)
        {
            userToEdit = user ?? throw new ArgumentNullException(nameof(user));
            userService = new UserService();
            InitializeComponent();
            LoadUserData();
        }
        private void LoadUserData()
        {
            txtFullName.Text = userToEdit.FullName;
            txtUsername.Text = userToEdit.Username;
            txtEmail.Text = userToEdit.Email;
            chkIsActive.Checked = userToEdit.IsActive;

            // Set role
            foreach (ComboBoxItem item in cmbRole.Items)
            {
                if (item.Value.ToString().ToLower() == userToEdit.Role.ToLower())
                {
                    cmbRole.SelectedItem = item;
                    break;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedItem = (ComboBoxItem)cmbRole.SelectedItem;
                userToEdit.FullName = txtFullName.Text.Trim();
                userToEdit.Username = txtUsername.Text.Trim();
                userToEdit.Email = txtEmail.Text.Trim();
                userToEdit.Role = selectedItem.Value.ToString().ToLower();
                userToEdit.IsActive = chkIsActive.Checked;

                if (userService.UpdateUser(userToEdit))
                {
                    MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật người dùng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            using var changePasswordForm = new ChangePasswordForm(userToEdit.UserId, true);
            changePasswordForm.ShowDialog();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public UserRole Value { get; set; }

            public ComboBoxItem(string text, UserRole value)
            {
                Text = text;
                Value = value;
            }
        }
    }
}
