﻿using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class InstructorCourseManagementForm
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
            this.listViewCourses = new ListView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.btnViewLessons = new Button();
            this.btnViewStudents = new Button();
            this.txtSearch = new TextBox();
            this.cmbStatusFilter = new ComboBox();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý khóa học của tôi";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Search controls
            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 20), Size = new Size(70, 23) };
            this.txtSearch.Location = new Point(100, 20);
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.KeyDown += TxtSearch_KeyDown;

            var lblStatus = new Label { Text = "Trạng thái:", Location = new Point(320, 20), Size = new Size(70, 23) };
            this.cmbStatusFilter.Location = new Point(400, 20);
            this.cmbStatusFilter.Size = new Size(120, 23);
            this.cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.Items.AddRange(new[] { "Tất cả", "Hoạt động", "Vô hiệu" });
            this.cmbStatusFilter.SelectedIndex = 0;
            this.cmbStatusFilter.SelectedIndexChanged += CmbStatusFilter_SelectedIndexChanged;

            // Buttons
            this.btnAdd.Text = "Thêm khóa học";
            this.btnAdd.Location = new Point(550, 19);
            this.btnAdd.Size = new Size(100, 25);
            this.btnAdd.BackColor = Color.Green;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnEdit.Text = "Sửa";
            this.btnEdit.Location = new Point(660, 19);
            this.btnEdit.Size = new Size(80, 25);
            this.btnEdit.BackColor = Color.Orange;
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Click += BtnEdit_Click;

            this.btnViewLessons.Text = "Bài học";
            this.btnViewLessons.Location = new Point(750, 19);
            this.btnViewLessons.Size = new Size(80, 25);
            this.btnViewLessons.BackColor = Color.Purple;
            this.btnViewLessons.ForeColor = Color.White;
            this.btnViewLessons.Click += BtnViewLessons_Click;

            this.btnViewStudents.Text = "Học viên";
            this.btnViewStudents.Location = new Point(840, 19);
            this.btnViewStudents.Size = new Size(80, 25);
            this.btnViewStudents.BackColor = Color.Blue;
            this.btnViewStudents.ForeColor = Color.White;
            this.btnViewStudents.Click += BtnViewStudents_Click;

            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new Point(930, 19);
            this.btnDelete.Size = new Size(80, 25);
            this.btnDelete.BackColor = Color.Red;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(1020, 19);
            this.btnRefresh.Size = new Size(80, 25);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewCourses.Location = new Point(20, 60);
            this.listViewCourses.Size = new Size(1140, 580);
            this.listViewCourses.View = View.Details;
            this.listViewCourses.FullRowSelect = true;
            this.listViewCourses.GridLines = true;
            this.listViewCourses.MultiSelect = false;
            this.listViewCourses.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewCourses.Columns.Add("ID", 60);
            this.listViewCourses.Columns.Add("Tiêu đề", 300);
            this.listViewCourses.Columns.Add("Mô tả", 350);
            this.listViewCourses.Columns.Add("Học viên", 80);
            this.listViewCourses.Columns.Add("Bài học", 80);
            this.listViewCourses.Columns.Add("Ngày tạo", 120);
            this.listViewCourses.Columns.Add("Cập nhật", 120);
            this.listViewCourses.Columns.Add("Trạng thái", 80);

            this.listViewCourses.DoubleClick += ListView_DoubleClick;
            this.listViewCourses.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 650);
            this.statusStrip.Size = new Size(1180, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(lblStatus);
            this.Controls.Add(this.cmbStatusFilter);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnViewLessons);
            this.Controls.Add(this.btnViewStudents);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewCourses);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}