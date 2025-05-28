using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class LessonViewerForm
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
            this.contentPanel = new Panel();
            this.lblTitle = new Label();
            this.lblDuration = new Label();
            this.txtContent = new TextBox();
            this.txtVideoUrl = new TextBox();
            this.btnMarkComplete = new Button();
            this.btnPrevious = new Button();
            this.btnNext = new Button();
            this.chkCompleted = new CheckBox();

            this.SuspendLayout();

            // Form
            this.Text = $"Bài học: {lesson.Title}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Content Panel
            this.contentPanel.Location = new Point(20, 20);
            this.contentPanel.Size = new Size(940, 600);
            this.contentPanel.BackColor = Color.White;
            this.contentPanel.Padding = new Padding(20);

            // Lesson Title
            this.lblTitle.Text = lesson.Title;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(0, 0);
            this.lblTitle.Size = new Size(900, 30);

            // Duration
            this.lblDuration.Text = $"Thời lượng: {lesson.Duration} phút";
            this.lblDuration.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblDuration.Location = new Point(0, 40);
            this.lblDuration.Size = new Size(200, 20);

            // Video URL (if available)
            if (!string.IsNullOrEmpty(lesson.VideoUrl))
            {
                var lblVideo = new Label
                {
                    Text = "Video bài học:",
                    Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold),
                    Location = new Point(0, 70),
                    Size = new Size(150, 20)
                };

                this.txtVideoUrl.Text = lesson.VideoUrl;
                this.txtVideoUrl.Location = new Point(0, 95);
                this.txtVideoUrl.Size = new Size(900, 23);
                this.txtVideoUrl.ReadOnly = true;
                this.txtVideoUrl.BackColor = Color.LightGray;

                this.contentPanel.Controls.Add(lblVideo);
                this.contentPanel.Controls.Add(this.txtVideoUrl);
            }

            // Content
            var lblContent = new Label
            {
                Text = "Nội dung bài học:",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Location = new Point(0, 130),
                Size = new Size(200, 25)
            };

            this.txtContent.Text = lesson.Content ?? "Chưa có nội dung";
            this.txtContent.Location = new Point(0, 160);
            this.txtContent.Size = new Size(900, 350);
            this.txtContent.Multiline = true;
            this.txtContent.ReadOnly = true;
            this.txtContent.ScrollBars = ScrollBars.Vertical;
            this.txtContent.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtContent.BackColor = Color.White;

            // Completion Status
            this.chkCompleted.Text = "Đánh dấu đã hoàn thành bài học này";
            this.chkCompleted.Location = new Point(0, 520);
            this.chkCompleted.Size = new Size(300, 25);
            this.chkCompleted.Font = new Font("Microsoft Sans Serif", 10F);

            this.contentPanel.Controls.Add(this.lblTitle);
            this.contentPanel.Controls.Add(this.lblDuration);
            this.contentPanel.Controls.Add(lblContent);
            this.contentPanel.Controls.Add(this.txtContent);
            this.contentPanel.Controls.Add(this.chkCompleted);

            // Navigation Buttons
            this.btnPrevious.Text = "Bài trước";
            this.btnPrevious.Location = new Point(20, 640);
            this.btnPrevious.Size = new Size(100, 30);
            this.btnPrevious.BackColor = Color.Gray;
            this.btnPrevious.ForeColor = Color.White;
            this.btnPrevious.Click += BtnPrevious_Click;

            this.btnNext.Text = "Bài tiếp";
            this.btnNext.Location = new Point(140, 640);
            this.btnNext.Size = new Size(100, 30);
            this.btnNext.BackColor = Color.Blue;
            this.btnNext.ForeColor = Color.White;
            this.btnNext.Click += BtnNext_Click;

            this.btnMarkComplete.Text = "Hoàn thành";
            this.btnMarkComplete.Location = new Point(260, 640);
            this.btnMarkComplete.Size = new Size(120, 30);
            this.btnMarkComplete.BackColor = Color.Green;
            this.btnMarkComplete.ForeColor = Color.White;
            this.btnMarkComplete.Click += BtnMarkComplete_Click;

            // Add controls to form
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnMarkComplete);

            this.ResumeLayout(false);
        }

        #endregion
    }
}