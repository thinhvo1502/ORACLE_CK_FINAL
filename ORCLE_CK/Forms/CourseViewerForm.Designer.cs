using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CourseViewerForm
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
            this.infoPanel = new Panel();
            this.lessonsPanel = new Panel();
            this.lessonsListView = new ListView();
            this.lblTitle = new Label();
            this.lblInstructor = new Label();
            this.lblDescription = new Label();
            this.lblProgress = new Label();
            this.progressBar = new ProgressBar();
            this.btnStartLesson = new Button();
            this.btnViewProgress = new Button();

            this.SuspendLayout();

            // Form
            this.Text = $"Khóa học: {course.Title}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;

            // Info Panel
            this.infoPanel.Location = new Point(20, 20);
            this.infoPanel.Size = new Size(940, 200);
            this.infoPanel.BackColor = Color.White;
            this.infoPanel.BorderStyle = BorderStyle.FixedSingle;
            this.infoPanel.Padding = new Padding(20);

            // Course Title
            this.lblTitle.Text = course.Title;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(900, 30);

            // Instructor
            this.lblInstructor.Text = $"Giảng viên: {course.InstructorName}";
            this.lblInstructor.Font = new Font("Microsoft Sans Serif", 12F);
            this.lblInstructor.Location = new Point(0, 40);
            this.lblInstructor.Size = new Size(400, 25);

            // Progress
            this.lblProgress.Text = "Tiến độ học tập:";
            this.lblProgress.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblProgress.Location = new Point(0, 75);
            this.lblProgress.Size = new Size(150, 20);

            this.progressBar.Location = new Point(160, 75);
            this.progressBar.Size = new Size(300, 20);
            this.progressBar.Style = ProgressBarStyle.Continuous;

            // Description
            var lblDescTitle = new Label
            {
                Text = "Mô tả khóa học:",
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold),
                Location = new Point(0, 110),
                Size = new Size(150, 20)
            };

            this.lblDescription.Text = course.Description ?? "Chưa có mô tả";
            this.lblDescription.Location = new Point(0, 135);
            this.lblDescription.Size = new Size(900, 45);
            this.lblDescription.Font = new Font("Microsoft Sans Serif", 9F);

            this.infoPanel.Controls.Add(this.lblTitle);
            this.infoPanel.Controls.Add(this.lblInstructor);
            this.infoPanel.Controls.Add(this.lblProgress);
            this.infoPanel.Controls.Add(this.progressBar);
            this.infoPanel.Controls.Add(lblDescTitle);
            this.infoPanel.Controls.Add(this.lblDescription);

            // Lessons Panel
            this.lessonsPanel.Location = new Point(20, 240);
            this.lessonsPanel.Size = new Size(940, 400);
            this.lessonsPanel.BackColor = Color.White;
            this.lessonsPanel.BorderStyle = BorderStyle.FixedSingle;

            var lblLessonsTitle = new Label
            {
                Text = "Danh sách bài học",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 10),
                Size = new Size(200, 25)
            };

            this.lessonsListView.Location = new Point(20, 40);
            this.lessonsListView.Size = new Size(900, 300);
            this.lessonsListView.View = View.Details;
            this.lessonsListView.FullRowSelect = true;
            this.lessonsListView.GridLines = true;
            this.lessonsListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.lessonsListView.Columns.Add("STT", 50);
            this.lessonsListView.Columns.Add("Tiêu đề", 300);
            this.lessonsListView.Columns.Add("Thời lượng", 100);
            this.lessonsListView.Columns.Add("Trạng thái", 100);
            this.lessonsListView.Columns.Add("Hoàn thành", 100);

            this.lessonsListView.DoubleClick += LessonsListView_DoubleClick;

            // Action Buttons
            this.btnStartLesson.Text = "Bắt đầu học";
            this.btnStartLesson.Location = new Point(20, 350);
            this.btnStartLesson.Size = new Size(120, 30);
            this.btnStartLesson.BackColor = Color.Green;
            this.btnStartLesson.ForeColor = Color.White;
            this.btnStartLesson.Click += BtnStartLesson_Click;

            this.btnViewProgress.Text = "Xem tiến độ";
            this.btnViewProgress.Location = new Point(160, 350);
            this.btnViewProgress.Size = new Size(120, 30);
            this.btnViewProgress.BackColor = Color.Blue;
            this.btnViewProgress.ForeColor = Color.White;
            this.btnViewProgress.Click += BtnViewProgress_Click;

            this.lessonsPanel.Controls.Add(lblLessonsTitle);
            this.lessonsPanel.Controls.Add(this.lessonsListView);
            this.lessonsPanel.Controls.Add(this.btnStartLesson);
            this.lessonsPanel.Controls.Add(this.btnViewProgress);

            // Add panels to form
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.lessonsPanel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}