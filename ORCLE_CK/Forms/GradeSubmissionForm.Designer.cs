using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class GradeSubmissionForm
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
            this.txtFileUrl = new TextBox();
            this.numGrade = new NumericUpDown();
            this.txtFeedback = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chấm điểm bài nộp";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Student Info
            this.lblStudentInfo.Location = new Point(20, 20);
            this.lblStudentInfo.Size = new Size(540, 40);
            this.lblStudentInfo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.lblStudentInfo.ForeColor = Color.Blue;

            // File URL
            var lblFileUrl = new Label { Text = "File đính kèm:", Location = new Point(20, 80), Size = new Size(150, 23) };
            this.txtFileUrl.Location = new Point(20, 110);
            this.txtFileUrl.Size = new Size(540, 200);
            this.txtFileUrl.Multiline = true;
            this.txtFileUrl.ScrollBars = ScrollBars.Vertical;
            this.txtFileUrl.ReadOnly = true;
            this.txtFileUrl.BackColor = Color.LightGray;

            // Grade
            var lblGrade = new Label { Text = "Điểm:", Location = new Point(20, 330), Size = new Size(100, 23) };
            this.numGrade.Location = new Point(130, 330);
            this.numGrade.Size = new Size(80, 23);
            this.numGrade.Minimum = 0;
            this.numGrade.DecimalPlaces = 2;
            this.numGrade.Increment = 0.5M;

            var lblMaxScore = new Label();
            lblMaxScore.Location = new Point(220, 330);
            lblMaxScore.Size = new Size(100, 23);
            lblMaxScore.Text = $"/ {submission.MaxScore}";

            // Feedback
            var lblFeedback = new Label { Text = "Phản hồi:", Location = new Point(20, 370), Size = new Size(100, 23) };
            this.txtFeedback.Location = new Point(20, 400);
            this.txtFeedback.Size = new Size(540, 100);
            this.txtFeedback.Multiline = true;
            this.txtFeedback.ScrollBars = ScrollBars.Vertical;

            // Buttons
            this.btnSave.Text = "Lưu điểm";
            this.btnSave.Location = new Point(400, 520);
            this.btnSave.Size = new Size(75, 30);
            this.btnSave.BackColor = Color.Green;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(485, 520);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(this.lblStudentInfo);
            this.Controls.Add(lblFileUrl);
            this.Controls.Add(this.txtFileUrl);
            this.Controls.Add(lblGrade);
            this.Controls.Add(this.numGrade);
            this.Controls.Add(lblMaxScore);
            this.Controls.Add(lblFeedback);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}