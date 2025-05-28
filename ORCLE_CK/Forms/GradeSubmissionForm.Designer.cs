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
            this.txtContent = new TextBox();
            this.numScore = new NumericUpDown();
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

            // Content
            var lblContent = new Label { Text = "Nội dung bài nộp:", Location = new Point(20, 80), Size = new Size(150, 23) };
            this.txtContent.Location = new Point(20, 110);
            this.txtContent.Size = new Size(540, 200);
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = ScrollBars.Vertical;
            this.txtContent.ReadOnly = true;
            this.txtContent.BackColor = Color.LightGray;

            // Score
            var lblScore = new Label { Text = "Điểm:", Location = new Point(20, 330), Size = new Size(100, 23) };
            this.numScore.Location = new Point(130, 330);
            this.numScore.Size = new Size(80, 23);
            this.numScore.Minimum = 0;
            this.numScore.DecimalPlaces = 1;

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
            //this.txtFeedback.PlaceholderText = "Nhập phản hồi cho học viên...";

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
            this.Controls.Add(lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(lblScore);
            this.Controls.Add(this.numScore);
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