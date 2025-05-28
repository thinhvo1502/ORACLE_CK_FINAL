using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class EditCourseForm
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
            this.cmbInstructor = new ComboBox();
            this.chkIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chỉnh sửa khóa học";
            this.Size = new Size(550, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label { Text = "Tiêu đề:", Location = new Point(20, 20), Size = new Size(100, 23) };
            this.txtTitle.Location = new Point(130, 20);
            this.txtTitle.Size = new Size(380, 23);

            // Description
            var lblDescription = new Label { Text = "Mô tả:", Location = new Point(20, 60), Size = new Size(100, 23) };
            this.txtDescription.Location = new Point(130, 60);
            this.txtDescription.Size = new Size(380, 150);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;

            // Instructor
            var lblInstructor = new Label { Text = "Giảng viên:", Location = new Point(20, 230), Size = new Size(100, 23) };
            this.cmbInstructor.Location = new Point(130, 230);
            this.cmbInstructor.Size = new Size(380, 23);
            this.cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInstructor.DisplayMember = "FullName";
            this.cmbInstructor.ValueMember = "UserId";

            // Is Active
            this.chkIsActive.Text = "Khóa học hoạt động";
            this.chkIsActive.Location = new Point(130, 270);
            this.chkIsActive.Size = new Size(200, 23);

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(350, 350);
            this.btnSave.Size = new Size(75, 30);
            this.btnSave.BackColor = Color.Green;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(435, 350);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(lblInstructor);
            this.Controls.Add(this.cmbInstructor);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}