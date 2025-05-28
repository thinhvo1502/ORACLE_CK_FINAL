using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class EditAssignmentForm
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
            this.txtDescription = new TextBox();
            this.dtpDueDate = new DateTimePicker();
            this.chkHasDueDate = new CheckBox();
            this.numMaxScore = new NumericUpDown();
            this.chkIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chỉnh sửa bài tập";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label { Text = "Tiêu đề:", Location = new Point(20, 20), Size = new Size(100, 23) };
            this.txtTitle.Location = new Point(130, 20);
            this.txtTitle.Size = new Size(430, 23);

            // Description
            var lblDescription = new Label { Text = "Mô tả:", Location = new Point(20, 60), Size = new Size(100, 23) };
            this.txtDescription.Location = new Point(130, 60);
            this.txtDescription.Size = new Size(430, 200);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;

            // Due Date
            this.chkHasDueDate.Text = "Có hạn nộp";
            this.chkHasDueDate.Location = new Point(130, 280);
            this.chkHasDueDate.Size = new Size(100, 23);
            this.chkHasDueDate.CheckedChanged += ChkHasDueDate_CheckedChanged;

            var lblDueDate = new Label { Text = "Hạn nộp:", Location = new Point(20, 320), Size = new Size(100, 23) };
            this.dtpDueDate.Location = new Point(130, 320);
            this.dtpDueDate.Size = new Size(200, 23);
            this.dtpDueDate.Format = DateTimePickerFormat.Custom;
            this.dtpDueDate.CustomFormat = "dd/MM/yyyy HH:mm";

            // Max Score
            var lblMaxScore = new Label { Text = "Điểm tối đa:", Location = new Point(350, 320), Size = new Size(80, 23) };
            this.numMaxScore.Location = new Point(440, 320);
            this.numMaxScore.Size = new Size(80, 23);
            this.numMaxScore.Minimum = 1;
            this.numMaxScore.Maximum = 1000;

            // Is Active
            this.chkIsActive.Text = "Bài tập hoạt động";
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
            this.Controls.Add(lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.chkHasDueDate);
            this.Controls.Add(lblDueDate);
            this.Controls.Add(this.dtpDueDate);
            this.Controls.Add(lblMaxScore);
            this.Controls.Add(this.numMaxScore);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}