using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class StudentDashboardForm
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
            this.recentCoursesPanel = new Panel();
            this.progressPanel = new Panel();
            this.recentCoursesListView = new ListView();
            this.progressListView = new ListView();
            this.lblTotalCourses = new Label();
            this.lblCompletedCourses = new Label();
            this.lblInProgressCourses = new Label();
            this.lblAverageProgress = new Label();
            this.btnViewAllCourses = new Button();
            this.btnFindCourses = new Button();
            this.btnViewProgress = new Button();

            this.SuspendLayout();

            // Form
            this.Text = $"Bảng điều khiển học viên - {currentUser.FullName}";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;

            // Stats Panel
            this.statsPanel.Location = new Point(20, 20);
            this.statsPanel.Size = new Size(1140, 120);
            this.statsPanel.BackColor = Color.White;
            this.statsPanel.BorderStyle = BorderStyle.FixedSingle;

            var lblStatsTitle = new Label
            {
                Text = "Thống kê học tập",
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, 10),
                Size = new Size(200, 25)
            };

            // Stats Labels
            this.lblTotalCourses.Location = new Point(20, 45);
            this.lblTotalCourses.Size = new Size(250, 20);
            this.lblTotalCourses.Font = new Font("Microsoft Sans Serif", 10F);

            this.lblCompletedCourses.Location = new Point(300, 45);
            this.lblCompletedCourses.Size = new Size(250, 20);
            this.lblCompletedCourses.Font = new Font("Microsoft Sans Serif", 10F);

            this.lblInProgressCourses.Location = new Point(580, 45);
            this.lblInProgressCourses.Size = new Size(250, 20);
            this.lblInProgressCourses.Font = new Font("Microsoft Sans Serif", 10F);

            this.lblAverageProgress.Location = new Point(860, 45);
            this.lblAverageProgress.Size = new Size(250, 20);
            this.lblAverageProgress.Font = new Font("Microsoft Sans Serif", 10F);

            // Action Buttons
            this.btnViewAllCourses.Text = "Xem tất cả khóa học";
            this.btnViewAllCourses.Location = new Point(20, 80);
            this.btnViewAllCourses.Size = new Size(150, 30);
            this.btnViewAllCourses.BackColor = Color.Blue;
            this.btnViewAllCourses.ForeColor = Color.White;
            this.btnViewAllCourses.Click += BtnViewAllCourses_Click;

            this.btnFindCourses.Text = "Tìm khóa học mới";
            this.btnFindCourses.Location = new Point(190, 80);
            this.btnFindCourses.Size = new Size(150, 30);
            this.btnFindCourses.BackColor = Color.Green;
            this.btnFindCourses.ForeColor = Color.White;
            this.btnFindCourses.Click += BtnFindCourses_Click;

            this.btnViewProgress.Text = "Xem tiến độ";
            this.btnViewProgress.Location = new Point(360, 80);
            this.btnViewProgress.Size = new Size(150, 30);
            this.btnViewProgress.BackColor = Color.Orange;
            this.btnViewProgress.ForeColor = Color.White;
            this.btnViewProgress.Click += BtnViewProgress_Click;

            this.statsPanel.Controls.Add(lblStatsTitle);
            this.statsPanel.Controls.Add(this.lblTotalCourses);
            this.statsPanel.Controls.Add(this.lblCompletedCourses);
            this.statsPanel.Controls.Add(this.lblInProgressCourses);
            this.statsPanel.Controls.Add(this.lblAverageProgress);
            this.statsPanel.Controls.Add(this.btnViewAllCourses);
            this.statsPanel.Controls.Add(this.btnFindCourses);
            this.statsPanel.Controls.Add(this.btnViewProgress);

            // Recent Courses Panel
            this.recentCoursesPanel.Location = new Point(20, 160);
            this.recentCoursesPanel.Size = new Size(560, 300);
            this.recentCoursesPanel.BackColor = Color.White;
            this.recentCoursesPanel.BorderStyle = BorderStyle.FixedSingle;

            var lblRecentTitle = new Label
            {
                Text = "Khóa học gần đây",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, 10),
                Size = new Size(200, 25)
            };

            this.recentCoursesListView.Location = new Point(10, 40);
            this.recentCoursesListView.Size = new Size(540, 250);
            this.recentCoursesListView.View = View.Details;
            this.recentCoursesListView.FullRowSelect = true;
            this.recentCoursesListView.GridLines = true;
            this.recentCoursesListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.recentCoursesListView.Columns.Add("Khóa học", 250);
            this.recentCoursesListView.Columns.Add("Giảng viên", 150);
            this.recentCoursesListView.Columns.Add("Tiến độ", 80);
            this.recentCoursesListView.Columns.Add("Trạng thái", 80);

            this.recentCoursesListView.DoubleClick += RecentCoursesListView_DoubleClick;

            this.recentCoursesPanel.Controls.Add(lblRecentTitle);
            this.recentCoursesPanel.Controls.Add(this.recentCoursesListView);

            // Progress Panel
            this.progressPanel.Location = new Point(600, 160);
            this.progressPanel.Size = new Size(560, 300);
            this.progressPanel.BackColor = Color.White;
            this.progressPanel.BorderStyle = BorderStyle.FixedSingle;

            var lblProgressTitle = new Label
            {
                Text = "Tiến độ học tập",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(10, 10),
                Size = new Size(200, 25)
            };

            this.progressListView.Location = new Point(10, 40);
            this.progressListView.Size = new Size(540, 250);
            this.progressListView.View = View.Details;
            this.progressListView.FullRowSelect = true;
            this.progressListView.GridLines = true;
            this.progressListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.progressListView.Columns.Add("Khóa học", 200);
            this.progressListView.Columns.Add("Bài học", 80);
            this.progressListView.Columns.Add("Hoàn thành", 80);
            this.progressListView.Columns.Add("Tiến độ", 80);
            this.progressListView.Columns.Add("Điểm", 80);

            this.progressPanel.Controls.Add(lblProgressTitle);
            this.progressPanel.Controls.Add(this.progressListView);

            // Add panels to form
            this.Controls.Add(this.statsPanel);
            this.Controls.Add(this.recentCoursesPanel);
            this.Controls.Add(this.progressPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}