using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class SubmissionDetailForm
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
            this.lblStudentInfo = new Label();
            this.txtContent = new TextBox();
            this.lblScoreInfo = new Label();
            this.txtFeedback = new TextBox();
            this.btnClose = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chi tiết bài nộp";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Student Info
            this.lblStudentInfo.Location = new Point(20, 20);
            this.lblStudentInfo.Size = new Size(640, 60);
            this.lblStudentInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblStudentInfo.ForeColor = Color.Blue;

            // Content
            var lblContent = new Label { Text = "Nội dung bài nộp:", Location = new Point(20, 100), Size = new Size(150, 23) };
            this.txtContent.Location = new Point(20, 130);
            this.txtContent.Size = new Size(640, 250);
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = ScrollBars.Vertical;
            this.txtContent.ReadOnly = true;
            this.txtContent.BackColor = Color.LightGray;

            // Score Info
            this.lblScoreInfo.Location = new Point(20, 400);
            this.lblScoreInfo.Size = new Size(640, 23);
            this.lblScoreInfo.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);

            // Feedback
            var lblFeedback = new Label { Text = "Phản hồi từ giảng viên:", Location = new Point(20, 440), Size = new Size(150, 23) };
            this.txtFeedback.Location = new Point(20, 470);
            this.txtFeedback.Size = new Size(640, 80);
            this.txtFeedback.Multiline = true;
            this.txtFeedback.ScrollBars = ScrollBars.Vertical;
            this.txtFeedback.ReadOnly = true;
            this.txtFeedback.BackColor = Color.LightGray;

            // Button
            this.btnClose.Text = "Đóng";
            this.btnClose.Location = new Point(585, 560);
            this.btnClose.Size = new Size(75, 30);
            this.btnClose.BackColor = Color.Gray;
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Click += BtnClose_Click;

            // Add controls
            this.Controls.Add(this.lblStudentInfo);
            this.Controls.Add(lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lblScoreInfo);
            this.Controls.Add(lblFeedback);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }

        #endregion
    }
}