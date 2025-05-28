using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class QuizResultDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblStudentName = new Label();
            this.lblQuizTitle = new Label();
            this.lblScore = new Label();
            this.lblTotalScore = new Label();
            this.lblPercentage = new Label();
            this.lblTimeTaken = new Label();
            this.lblTakenAt = new Label();
            this.lblStatus = new Label();
            this.btnClose = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chi tiết kết quả Quiz";
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

            this.lblQuizTitle.Location = new Point(20, startY + (labelHeight + spacing));
            this.lblQuizTitle.Size = new Size(440, labelHeight);

            this.lblScore.Location = new Point(20, startY + (labelHeight + spacing) * 2);
            this.lblScore.Size = new Size(440, labelHeight);

            this.lblTotalScore.Location = new Point(20, startY + (labelHeight + spacing) * 3);
            this.lblTotalScore.Size = new Size(440, labelHeight);

            this.lblPercentage.Location = new Point(20, startY + (labelHeight + spacing) * 4);
            this.lblPercentage.Size = new Size(440, labelHeight);

            this.lblTimeTaken.Location = new Point(20, startY + (labelHeight + spacing) * 5);
            this.lblTimeTaken.Size = new Size(440, labelHeight);

            this.lblTakenAt.Location = new Point(20, startY + (labelHeight + spacing) * 6);
            this.lblTakenAt.Size = new Size(440, labelHeight);

            this.lblStatus.Location = new Point(20, startY + (labelHeight + spacing) * 7);
            this.lblStatus.Size = new Size(440, labelHeight);
            this.lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Close button
            this.btnClose.Text = "Đóng";
            this.btnClose.Location = new Point(200, startY + (labelHeight + spacing) * 8);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.BackColor = Color.Gray;
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Click += BtnClose_Click;

            // Add controls
            this.Controls.Add(this.lblStudentName);
            this.Controls.Add(this.lblQuizTitle);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblTotalScore);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.lblTimeTaken);
            this.Controls.Add(this.lblTakenAt);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }
    }
}