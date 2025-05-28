using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class UserManagementForm
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
            this.listViewUsers = new ListView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.btnSearch = new Button();
            this.txtSearch = new TextBox();
            this.cmbRoleFilter = new ComboBox();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý người dùng";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Search controls
            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 20), Size = new Size(70, 23) };
            this.txtSearch.Location = new Point(100, 20);
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.KeyDown += TxtSearch_KeyDown;

            this.btnSearch.Text = "Tìm";
            this.btnSearch.Location = new Point(310, 19);
            this.btnSearch.Size = new Size(60, 25);
            this.btnSearch.Click += BtnSearch_Click;

            var lblRole = new Label { Text = "Vai trò:", Location = new Point(390, 20), Size = new Size(50, 23) };
            this.cmbRoleFilter.Location = new Point(450, 20);
            this.cmbRoleFilter.Size = new Size(120, 23);
            this.cmbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRoleFilter.Items.AddRange(new[] { "Tất cả", "Quản trị viên", "Giảng viên", "Học viên" });
            this.cmbRoleFilter.SelectedIndex = 0;
            this.cmbRoleFilter.SelectedIndexChanged += CmbRoleFilter_SelectedIndexChanged;

            // Buttons
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Location = new Point(600, 19);
            this.btnAdd.Size = new Size(80, 25);
            this.btnAdd.BackColor = Color.Green;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnEdit.Text = "Sửa";
            this.btnEdit.Location = new Point(690, 19);
            this.btnEdit.Size = new Size(80, 25);
            this.btnEdit.BackColor = Color.Orange;
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Click += BtnEdit_Click;

            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new Point(780, 19);
            this.btnDelete.Size = new Size(80, 25);
            this.btnDelete.BackColor = Color.Red;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(870, 19);
            this.btnRefresh.Size = new Size(80, 25);
            this.btnRefresh.BackColor = Color.Blue;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewUsers.Location = new Point(20, 60);
            this.listViewUsers.Size = new Size(940, 480);
            this.listViewUsers.View = View.Details;
            this.listViewUsers.FullRowSelect = true;
            this.listViewUsers.GridLines = true;
            this.listViewUsers.MultiSelect = false;
            this.listViewUsers.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewUsers.Columns.Add("ID", 60);
            this.listViewUsers.Columns.Add("Họ tên", 200);
            this.listViewUsers.Columns.Add("Tên đăng nhập", 150);
            this.listViewUsers.Columns.Add("Email", 200);
            this.listViewUsers.Columns.Add("Vai trò", 120);
            this.listViewUsers.Columns.Add("Ngày tạo", 120);
            this.listViewUsers.Columns.Add("Đăng nhập cuối", 150);
            this.listViewUsers.Columns.Add("Trạng thái", 80);

            this.listViewUsers.DoubleClick += ListView_DoubleClick;
            this.listViewUsers.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 550);
            this.statusStrip.Size = new Size(980, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(lblRole);
            this.Controls.Add(this.cmbRoleFilter);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewUsers);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}