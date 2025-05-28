using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CourseSelectionForm
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
            this.cmbCourses = new ComboBox();
            this.btnSelect = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Chọn khóa học";
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Label
            var lblTitle = new Label
            {
                Text = "Chọn khóa học để quản lý bài học:",
                Location = new Point(20, 20),
                Size = new Size(350, 23),
                Font = new Font("Microsoft Sans Serif", 10F)
            };

            // ComboBox
            this.cmbCourses.Location = new Point(20, 50);
            this.cmbCourses.Size = new Size(340, 23);
            this.cmbCourses.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCourses.DisplayMember = "Title";
            this.cmbCourses.ValueMember = "CourseId";

            // Buttons
            this.btnSelect.Text = "Chọn";
            this.btnSelect.Location = new Point(200, 100);
            this.btnSelect.Size = new Size(75, 30);
            this.btnSelect.BackColor = Color.Green;
            this.btnSelect.ForeColor = Color.White;
            this.btnSelect.Click += BtnSelect_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(285, 100);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.cmbCourses);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}