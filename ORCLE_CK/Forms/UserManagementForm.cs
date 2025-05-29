using ORCLE_CK.Constants;
using ORCLE_CK.Models;
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
    public partial class UserManagementForm : Form
    {
        private readonly UserService userService;
        private ListView listViewUsers;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnSearch;
        private TextBox txtSearch;
        private ComboBox cmbRoleFilter;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public UserManagementForm()
        {
            userService = new UserService();
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewUsers.Items.Clear();

                var users = userService.GetAllUsers();

                foreach (var user in users)
                {
                    var item = new ListViewItem(user.UserId.ToString());
                    item.SubItems.Add(user.FullName);
                    item.SubItems.Add(user.Username);
                    item.SubItems.Add(user.Email);
                    item.SubItems.Add(user.RoleDisplayName);
                    item.SubItems.Add(user.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add(user.LastLoginAt?.ToString(AppConstants.DATETIME_FORMAT) ?? "Chưa đăng nhập");
                    item.SubItems.Add(user.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = user;

                    if (!user.IsActive)
                        item.ForeColor = Color.Gray;

                    listViewUsers.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {users.Count} người dùng";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading users: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewUsers.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearch_Click(sender, e);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchUsers();
        }

        private void SearchUsers()
        {
            try
            {
                var searchText = txtSearch.Text.Trim().ToLower();
                var selectedRole = cmbRoleFilter.SelectedItem.ToString();

                foreach (ListViewItem item in listViewUsers.Items)
                {
                    var user = (User)item.Tag;
                    bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                       user.FullName.ToLower().Contains(searchText) ||
                                       user.Username.ToLower().Contains(searchText) ||
                                       user.Email.ToLower().Contains(searchText);

                    bool matchesRole = selectedRole == "Tất cả" ||
                                     user.RoleDisplayName == selectedRole;

                    item.BackColor = (matchesSearch && matchesRole) ? Color.White : Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error searching users: {ex.Message}", ex);
            }
        }

        private void CmbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchUsers();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using var addForm = new AddUserForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
                statusLabel.Text = MessageConstants.USER_CREATED_SUCCESS;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 0) return;

            var selectedUser = (User)listViewUsers.SelectedItems[0].Tag;
            using var editForm = new EditUserForm(selectedUser);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
                statusLabel.Text = MessageConstants.USER_UPDATED_SUCCESS;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count == 0) return;

            var selectedUser = (User)listViewUsers.SelectedItems[0].Tag;

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{selectedUser.FullName}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (userService.DeleteUser(selectedUser.UserId))
                    {
                        LoadUsers();
                        statusLabel.Text = MessageConstants.USER_DELETED_SUCCESS;
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa người dùng!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error deleting user: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi xóa người dùng: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
    }
}