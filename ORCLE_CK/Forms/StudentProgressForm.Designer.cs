using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class StudentProgressForm
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
            this.summaryPanel = new Panel();
            this.detailPanel = new Panel();
            this.progressListView = new ListView();
            this.lblTotalCourses = new Label();
            this.lblCompletedCourses = new Label();
            this.lblAverageProgress = new Label();
            this.lblTotalHours = new Label();
            this.overallProgressBar = new ProgressBar();
            this.cmbCourseFilter = new ComboBox();
            this.btnRefresh = new Button();
            this.btnViewCertificate = new Button();

            this.SuspendLayout();

            // Form
            this.Text = $"Tiến độ học tập - {currentUser.FullName}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;

            // Summary Panel
            this.summaryPanel.Location = new Point(20, 20);
            this.summaryPanel.Size = new Size(940, 150);
            this.summaryPanel.BackColor = Color.White;
            this.summaryPanel.BorderStyle = BorderStyle.FixedSingle;
            this.summaryPanel.Padding = new Padding(20);

            var lblSummaryTitle = new Label
            {
                Text = "Tổng quan tiến độ học tập",
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(0, 0),
                Size = new Size(300, 25)
            };

            // Statistics
            this.lblTotalCourses.Location = new Point(0, 35);
            this.lblTotalCourses.Size = new Size(200, 20);
            this.lblTotalCourses.Font = new Font("Microsoft Sans Serif", 10F);

            this.lblCompletedCourses.Location = new Point(220, 35);
            this.lblCompletedCourses.Size = new Size(200, 20);
            this.lblCompletedCourses.Font = new Font("Microsoft Sans Serif", 10F);

            this.lblAverageProgress.Location = new Point(440, 35);
            this.lblAverageProgress.Size = new Size(200, 20);
            this.lblAverageProgress.Font = new Font("Microsoft Sans Serif", 10F);

            this.lblTotalHours.Location = new Point(660, 35);
            this.lblTotalHours.Size = new Size(200, 20);
            this.lblTotalHours.Font = new Font("Microsoft Sans Serif", 10F);

            // Overall Progress Bar
            var lblOverallProgress = new Label
            {
                Text = "Tiến độ tổng thể:",
                Location = new Point(0, 70),
                Size = new Size(150, 20),
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold)
            };

            this.overallProgressBar.Location = new Point(160, 70);
            this.overallProgressBar.Size = new Size(400, 25);
            this.overallProgressBar.Style = ProgressBarStyle.Continuous;

            // Action Buttons
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(0, 105);
            this.btnRefresh.Size = new Size(100, 30);
            this.btnRefresh.BackColor = Color.Blue;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            this.btnViewCertificate.Text = "Xem chứng chỉ";
            this.btnViewCertificate.Location = new Point(120, 105);
            this.btnViewCertificate.Size = new Size(120, 30);
            this.btnViewCertificate.BackColor = Color.Orange;
            this.btnViewCertificate.ForeColor = Color.White;
            this.btnViewCertificate.Click += BtnViewCertificate_Click;

            this.summaryPanel.Controls.Add(lblSummaryTitle);
            this.summaryPanel.Controls.Add(this.lblTotalCourses);
            this.summaryPanel.Controls.Add(this.lblCompletedCourses);
            this.summaryPanel.Controls.Add(this.lblAverageProgress);
            this.summaryPanel.Controls.Add(this.lblTotalHours);
            this.summaryPanel.Controls.Add(lblOverallProgress);
            this.summaryPanel.Controls.Add(this.overallProgressBar);
            this.summaryPanel.Controls.Add(this.btnRefresh);
            this.summaryPanel.Controls.Add(this.btnViewCertificate);

            // Detail Panel
            this.detailPanel.Location = new Point(20, 190);
            this.detailPanel.Size = new Size(940, 450);
            this.detailPanel.BackColor = Color.White;
            this.detailPanel.BorderStyle = BorderStyle.FixedSingle;

            var lblDetailTitle = new Label
            {
                Text = "Chi tiết tiến độ từng khóa học",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 10),
                Size = new Size(300, 25)
            };

            // Filter
            var lblFilter = new Label
            {
                Text = "Lọc:",
                Location = new Point(20, 45),
                Size = new Size(50, 20)
            };

            this.cmbCourseFilter.Location = new Point(80, 42);
            this.cmbCourseFilter.Size = new Size(200, 23);
            this.cmbCourseFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCourseFilter.Items.AddRange(new[] { "Tất cả", "Đang học", "Hoàn thành", "Chưa bắt đầu" });
            this.cmbCourseFilter.SelectedIndex = 0;
            this.cmbCourseFilter.SelectedIndexChanged += CmbCourseFilter_SelectedIndexChanged;

            // Progress ListView
            this.progressListView.Location = new Point(20, 75);
            this.progressListView.Size = new Size(900, 350);
            this.progressListView.View = View.Details;
            this.progressListView.FullRowSelect = true;
            this.progressListView.GridLines = true;
            this.progressListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.progressListView.Columns.Add("Khóa học", 250);
            this.progressListView.Columns.Add("Giảng viên", 150);
            this.progressListView.Columns.Add("Ngày đăng ký", 120);
            this.progressListView.Columns.Add("Tiến độ", 100);
            this.progressListView.Columns.Add("Điểm", 80);
            this.progressListView.Columns.Add("Trạng thái", 100);
            this.progressListView.Columns.Add("Hoàn thành", 120);

            this.detailPanel.Controls.Add(lblDetailTitle);
            this.detailPanel.Controls.Add(lblFilter);
            this.detailPanel.Controls.Add(this.cmbCourseFilter);
            this.detailPanel.Controls.Add(this.progressListView);

            // Add panels to form
            this.Controls.Add(this.summaryPanel);
            this.Controls.Add(this.detailPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}