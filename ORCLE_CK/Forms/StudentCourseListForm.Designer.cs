using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class StudentCourseListForm
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
            this.coursesListView = new ListView();
            this.cmbStatus = new ComboBox();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.btnViewCourse = new Button();
            this.btnRefresh = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Khóa học của tôi";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // Search Panel
            var searchPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(940, 50),
                BackColor = Color.LightGray
            };

            var lblSearch = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(10, 15),
                Size = new Size(70, 20)
            };

            this.txtSearch.Location = new Point(90, 12);
            this.txtSearch.Size = new Size(200, 23);

            var lblStatus = new Label
            {
                Text = "Trạng thái:",
                Location = new Point(310, 15),
                Size = new Size(70, 20)
            };

            this.cmbStatus.Location = new Point(390, 12);
            this.cmbStatus.Size = new Size(120, 23);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new[] { "Tất cả", "Đang học", "Hoàn thành", "Tạm dừng" });
            this.cmbStatus.SelectedIndex = 0;

            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Location = new Point(530, 10);
            this.btnSearch.Size = new Size(80, 27);
            this.btnSearch.BackColor = Color.Blue;
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Click += BtnSearch_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(620, 10);
            this.btnRefresh.Size = new Size(80, 27);
            this.btnRefresh.BackColor = Color.Green;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(this.txtSearch);
            searchPanel.Controls.Add(lblStatus);
            searchPanel.Controls.Add(this.cmbStatus);
            searchPanel.Controls.Add(this.btnSearch);
            searchPanel.Controls.Add(this.btnRefresh);

            // Courses ListView
            this.coursesListView.Location = new Point(20, 90);
            this.coursesListView.Size = new Size(940, 500);
            this.coursesListView.View = View.Details;
            this.coursesListView.FullRowSelect = true;
            this.coursesListView.GridLines = true;
            this.coursesListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.coursesListView.Columns.Add("Khóa học", 250);
            this.coursesListView.Columns.Add("Giảng viên", 150);
            this.coursesListView.Columns.Add("Ngày đăng ký", 120);
            this.coursesListView.Columns.Add("Tiến độ", 100);
            this.coursesListView.Columns.Add("Trạng thái", 100);
            this.coursesListView.Columns.Add("Điểm", 80);
            this.coursesListView.Columns.Add("Hoàn thành", 120);

            this.coursesListView.DoubleClick += CoursesListView_DoubleClick;

            // Action Buttons
            this.btnViewCourse.Text = "Xem khóa học";
            this.btnViewCourse.Location = new Point(20, 610);
            this.btnViewCourse.Size = new Size(120, 30);
            this.btnViewCourse.BackColor = Color.Orange;
            this.btnViewCourse.ForeColor = Color.White;
            this.btnViewCourse.Click += BtnViewCourse_Click;

            // Add controls
            this.Controls.Add(searchPanel);
            this.Controls.Add(this.coursesListView);
            this.Controls.Add(this.btnViewCourse);

            this.ResumeLayout(false);
        }

        #endregion
    }
}