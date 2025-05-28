using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class EditLessonForm
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
            this.txtTitle = new TextBox();
            this.txtContent = new TextBox();
            this.txtVideoUrl = new TextBox();
            this.numDuration = new NumericUpDown();
            this.numOrderNumber = new NumericUpDown();
            this.chkIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chỉnh sửa bài học";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label { Text = "Tiêu đề:", Location = new Point(20, 20), Size = new Size(100, 23) };
            this.txtTitle.Location = new Point(130, 20);
            this.txtTitle.Size = new Size(430, 23);

            // Content
            var lblContent = new Label { Text = "Nội dung:", Location = new Point(20, 60), Size = new Size(100, 23) };
            this.txtContent.Location = new Point(130, 60);
            this.txtContent.Size = new Size(430, 200);
            this.txtContent.Multiline = true;
            this.txtContent.ScrollBars = ScrollBars.Vertical;

            // Video URL
            var lblVideoUrl = new Label { Text = "Video URL:", Location = new Point(20, 280), Size = new Size(100, 23) };
            this.txtVideoUrl.Location = new Point(130, 280);
            this.txtVideoUrl.Size = new Size(430, 23);
            //this.txtVideoUrl.PlaceholderText = "https://example.com/video.mp4";

            // Duration
            var lblDuration = new Label { Text = "Thời lượng (phút):", Location = new Point(20, 320), Size = new Size(100, 23) };
            this.numDuration.Location = new Point(130, 320);
            this.numDuration.Size = new Size(100, 23);
            this.numDuration.Minimum = 0;
            this.numDuration.Maximum = 999;

            // Order Number
            var lblOrderNumber = new Label { Text = "Thứ tự:", Location = new Point(250, 320), Size = new Size(60, 23) };
            this.numOrderNumber.Location = new Point(320, 320);
            this.numOrderNumber.Size = new Size(80, 23);
            this.numOrderNumber.Minimum = 1;
            this.numOrderNumber.Maximum = 999;

            // Is Active
            this.chkIsActive.Text = "Bài học hoạt động";
            this.chkIsActive.Location = new Point(130, 360);
            this.chkIsActive.Size = new Size(200, 23);

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(400, 450);
            this.btnSave.Size = new Size(75, 30);
            this.btnSave.BackColor = Color.Green;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(485, 450);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(lblVideoUrl);
            this.Controls.Add(this.txtVideoUrl);
            this.Controls.Add(lblDuration);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(lblOrderNumber);
            this.Controls.Add(this.numOrderNumber);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}