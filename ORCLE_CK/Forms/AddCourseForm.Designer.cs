using System.Windows.Forms;
using System;
using System.Drawing;

namespace ORCLE_CK.Forms
{
    partial class AddCourseForm
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
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Thêm khóa học mới";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            // Header Panel
            //var headerPanel = new Panel
            //{
            //    Height = 60,
            //    Dock = DockStyle.Top,
            //    BackColor = Color.FromArgb(0, 120, 215)
            //};

            //var titleLabel = new Label
            //{
            //    Text = "Thêm khóa học mới",
            //    Font = new Font("Segoe UI", 16, FontStyle.Bold),
            //    ForeColor = Color.White,
            //    AutoSize = true,
            //    Location = new Point(20, 15)
            //};

            //headerPanel.Controls.Add(titleLabel);

            // Content Panel
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            // Title
            var lblTitle = new Label 
            { 
                Text = "Tiêu đề:", 
                Location = new Point(20, 20), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtTitle.Location = new Point(150, 20);
            this.txtTitle.Size = new Size(380, 25);
            this.txtTitle.Font = new Font("Segoe UI", 10);
            this.txtTitle.BorderStyle = BorderStyle.FixedSingle;

            // Description
            var lblDescription = new Label 
            { 
                Text = "Mô tả:", 
                Location = new Point(20, 60), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtDescription.Location = new Point(150, 60);
            this.txtDescription.Size = new Size(380, 150);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Font = new Font("Segoe UI", 10);
            this.txtDescription.BorderStyle = BorderStyle.FixedSingle;

            // Instructor
            var lblInstructor = new Label 
            { 
                Text = "Giảng viên:", 
                Location = new Point(20, 230), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.cmbInstructor.Location = new Point(150, 230);
            this.cmbInstructor.Size = new Size(380, 25);
            this.cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInstructor.DisplayMember = "FullName";
            this.cmbInstructor.ValueMember = "UserId";
            this.cmbInstructor.Font = new Font("Segoe UI", 10);
            this.cmbInstructor.FlatStyle = FlatStyle.Flat;

            // Buttons Panel
            var buttonPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(380, 15);
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.BackColor = Color.FromArgb(0, 120, 215);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(490, 15);
            this.btnCancel.Size = new Size(100, 35);
            this.btnCancel.BackColor = Color.FromArgb(96, 125, 139);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            buttonPanel.Controls.AddRange(new Control[] { this.btnSave, this.btnCancel });

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] 
            { 
                lblTitle, this.txtTitle,
                lblDescription, this.txtDescription,
                lblInstructor, this.cmbInstructor
            });

            // Add panels to form
            this.Controls.AddRange(new Control[] {  contentPanel, buttonPanel });

            this.ResumeLayout(false);
        }

        #endregion
    }
}