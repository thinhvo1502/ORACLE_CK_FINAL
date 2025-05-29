using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CourseManagementForm
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
            this.Text = "Quản lý khóa học";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 9F);

            // ==== Tạo Panel header cho thanh tìm kiếm và các nút ====
            var panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var lblSearch = new Label { Text = "🔍 Tìm kiếm:", AutoSize = true, Location = new Point(10, 18) };
            txtSearch = new TextBox { Location = new Point(90, 15), Size = new Size(200, 25) };
            txtSearch.KeyDown += TxtSearch_KeyDown;

            btnSearch = CreateFlatButton("Tìm", Color.DodgerBlue);
            btnSearch.Location = new Point(300, 15);
            btnSearch.Click += BtnSearch_Click;

            var lblInstructor = new Label { Text = "👨‍🏫 Giảng viên:", AutoSize = true, Location = new Point(390, 18) };
            cmbInstructorFilter = new ComboBox
            {
                Location = new Point(480, 15),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbInstructorFilter.SelectedIndexChanged += CmbInstructorFilter_SelectedIndexChanged;

            btnAdd = CreateFlatButton("➕ Thêm", Color.ForestGreen);
            btnAdd.Location = new Point(650, 15);
            btnAdd.Click += BtnAdd_Click;

            btnEdit = CreateFlatButton("✏️ Sửa", Color.Orange);
            btnEdit.Location = new Point(740, 15);
            btnEdit.Click += BtnEdit_Click;

            btnDelete = CreateFlatButton("🗑️ Xóa", Color.IndianRed);
            btnDelete.Location = new Point(830, 15);
            btnDelete.Click += BtnDelete_Click;

            btnViewLessons = CreateFlatButton("📚 Bài học", Color.MediumPurple);
            btnViewLessons.Location = new Point(920, 15);
            btnViewLessons.Click += BtnViewLessons_Click;

            btnRefresh = CreateFlatButton("🔄 Làm mới", Color.Gray);
            btnRefresh.Location = new Point(1010, 15);
            btnRefresh.Click += BtnRefresh_Click;

            panelHeader.Controls.AddRange(new Control[]
            {
        lblSearch, txtSearch, btnSearch, lblInstructor, cmbInstructorFilter,
        btnAdd, btnEdit, btnDelete, btnViewLessons, btnRefresh
            });

            // ==== ListView chính ====
            listViewCourses = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            listViewCourses.Columns.Add("ID", 60);
            listViewCourses.Columns.Add("Tiêu đề", 200);
            listViewCourses.Columns.Add("Mô tả", 300);
            listViewCourses.Columns.Add("Giảng viên", 150);
            listViewCourses.Columns.Add("Ngày tạo", 100);
            listViewCourses.Columns.Add("Học viên", 80);
            listViewCourses.Columns.Add("Bài học", 80);
            listViewCourses.Columns.Add("Trạng thái", 100);

            listViewCourses.DoubleClick += ListView_DoubleClick;
            listViewCourses.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // ==== StatusStrip ====
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Sẵn sàng" };
            statusStrip.Items.Add(statusLabel);

            // ==== Thêm vào form ====
            this.Controls.Add(listViewCourses);
            this.Controls.Add(panelHeader);
            this.Controls.Add(statusStrip);
        }

        private Button CreateFlatButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                AutoSize = true,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Height = 30,
                Width = 80
            };
        }

        #endregion
    }
}