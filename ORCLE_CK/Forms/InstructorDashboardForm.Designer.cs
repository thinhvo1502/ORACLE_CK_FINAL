using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class InstructorDashboardForm
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
            this.statsPanel = new Panel();
            this.recentCoursesListView = new ListView();
            this.recentStudentsListView = new ListView();
            this.btnViewAllCourses = new Button();
            this.btnCreateCourse = new Button();
            this.btnViewStudents = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Bảng điều khiển giảng viên";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Title
            var lblTitle = new Label
            {
                Text = $"Chào mừng {currentUser.FullName}!",
                Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(600, 30)
            };

            // Stats Panel
            this.statsPanel.Location = new Point(20, 70);
            this.statsPanel.Size = new Size(1140, 120);
            this.statsPanel.BorderStyle = BorderStyle.FixedSingle;
            this.statsPanel.BackColor = Color.LightBlue;

            // Action Buttons
            this.btnCreateCourse.Text = "Tạo khóa học mới";
            this.btnCreateCourse.Location = new Point(20, 210);
            this.btnCreateCourse.Size = new Size(150, 35);
            this.btnCreateCourse.BackColor = Color.Green;
            this.btnCreateCourse.ForeColor = Color.White;
            this.btnCreateCourse.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnCreateCourse.Click += BtnCreateCourse_Click;

            this.btnViewAllCourses.Text = "Xem tất cả khóa học";
            this.btnViewAllCourses.Location = new Point(180, 210);
            this.btnViewAllCourses.Size = new Size(150, 35);
            this.btnViewAllCourses.BackColor = Color.Blue;
            this.btnViewAllCourses.ForeColor = Color.White;
            this.btnViewAllCourses.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnViewAllCourses.Click += BtnViewAllCourses_Click;

            this.btnViewStudents.Text = "Quản lý học viên";
            this.btnViewStudents.Location = new Point(340, 210);
            this.btnViewStudents.Size = new Size(150, 35);
            this.btnViewStudents.BackColor = Color.Orange;
            this.btnViewStudents.ForeColor = Color.White;
            this.btnViewStudents.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnViewStudents.Click += BtnViewStudents_Click;

            // Recent Courses
            var lblRecentCourses = new Label
            {
                Text = "Khóa học gần đây",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Location = new Point(20, 270),
                Size = new Size(200, 25)
            };

            this.recentCoursesListView.Location = new Point(20, 300);
            this.recentCoursesListView.Size = new Size(560, 300);
            this.recentCoursesListView.View = View.Details;
            this.recentCoursesListView.FullRowSelect = true;
            this.recentCoursesListView.GridLines = true;
            this.recentCoursesListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.recentCoursesListView.Columns.Add("Tiêu đề", 200);
            this.recentCoursesListView.Columns.Add("Học viên", 80);
            this.recentCoursesListView.Columns.Add("Bài học", 80);
            this.recentCoursesListView.Columns.Add("Ngày tạo", 120);
            this.recentCoursesListView.Columns.Add("Trạng thái", 80);

            this.recentCoursesListView.DoubleClick += RecentCoursesListView_DoubleClick;

            // Recent Students
            var lblRecentStudents = new Label
            {
                Text = "Học viên mới nhất",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Location = new Point(600, 270),
                Size = new Size(200, 25)
            };

            this.recentStudentsListView.Location = new Point(600, 300);
            this.recentStudentsListView.Size = new Size(560, 300);
            this.recentStudentsListView.View = View.Details;
            this.recentStudentsListView.FullRowSelect = true;
            this.recentStudentsListView.GridLines = true;
            this.recentStudentsListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.recentStudentsListView.Columns.Add("Họ tên", 150);
            this.recentStudentsListView.Columns.Add("Email", 180);
            this.recentStudentsListView.Columns.Add("Khóa học", 150);
            this.recentStudentsListView.Columns.Add("Ngày đăng ký", 80);

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.statsPanel);
            this.Controls.Add(this.btnCreateCourse);
            this.Controls.Add(this.btnViewAllCourses);
            this.Controls.Add(this.btnViewStudents);
            this.Controls.Add(lblRecentCourses);
            this.Controls.Add(this.recentCoursesListView);
            this.Controls.Add(lblRecentStudents);
            this.Controls.Add(this.recentStudentsListView);

            this.ResumeLayout(false);
        }
        #endregion
    }
}