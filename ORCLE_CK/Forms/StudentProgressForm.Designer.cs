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
            this.lblStudentName = new Label();
            this.lblCourseTitle = new Label();
            this.lblEnrollmentDate = new Label();
            this.lblProgress = new Label();
            this.lblCompletedLessons = new Label();
            this.lblAverageGrade = new Label();
            this.progressBar = new ProgressBar();
            this.btnClose = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chi tiết tiến độ học viên";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Labels
            int startY = 20;
            int labelHeight = 30;
            int spacing = 10;

            this.lblStudentName.Location = new Point(20, startY);
            this.lblStudentName.Size = new Size(440, labelHeight);
            this.lblStudentName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            this.lblCourseTitle.Location = new Point(20, startY + (labelHeight + spacing));
            this.lblCourseTitle.Size = new Size(440, labelHeight);

            this.lblEnrollmentDate.Location = new Point(20, startY + (labelHeight + spacing) * 2);
            this.lblEnrollmentDate.Size = new Size(440, labelHeight);

            this.lblProgress.Location = new Point(20, startY + (labelHeight + spacing) * 3);
            this.lblProgress.Size = new Size(440, labelHeight);

            // Progress Bar
            this.progressBar.Location = new Point(20, startY + (labelHeight + spacing) * 4);
            this.progressBar.Size = new Size(440, 25);
            this.progressBar.Maximum = 100;

            this.lblCompletedLessons.Location = new Point(20, startY + (labelHeight + spacing) * 5);
            this.lblCompletedLessons.Size = new Size(440, labelHeight);

            this.lblAverageGrade.Location = new Point(20, startY + (labelHeight + spacing) * 6);
            this.lblAverageGrade.Size = new Size(440, labelHeight);

            // Close button
            this.btnClose.Text = "Đóng";
            this.btnClose.Location = new Point(200, startY + (labelHeight + spacing) * 7);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.BackColor = Color.Gray;
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Click += BtnClose_Click;

            // Add controls
            this.Controls.Add(this.lblStudentName);
            this.Controls.Add(this.lblCourseTitle);
            this.Controls.Add(this.lblEnrollmentDate);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblCompletedLessons);
            this.Controls.Add(this.lblAverageGrade);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }

        #endregion
    }
}