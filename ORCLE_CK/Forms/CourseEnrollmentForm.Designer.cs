using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CourseEnrollmentForm
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
            this.coursesListView = new ListView();
            this.txtSearch = new TextBox();
            this.cmbInstructor = new ComboBox();
            this.btnSearch = new Button();
            this.btnEnroll = new Button();
            this.btnRefresh = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Tìm kiếm và đăng ký khóa học";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // Search Panel
            var searchPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(940, 50),
                BackColor = Color.LightGray
            };

            var lblSearch = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(10, 15),
                Size = new Size(70, 20)
            };

            this.txtSearch.Location = new Point(90, 12);
            this.txtSearch.Size = new Size(200, 23);
            //this.txtSearch.PlaceholderText = "Nhập tên khóa học...";

            var lblInstructor = new Label
            {
                Text = "Giảng viên:",
                Location = new Point(310, 15),
                Size = new Size(70, 20)
            };

            this.cmbInstructor.Location = new Point(390, 12);
            this.cmbInstructor.Size = new Size(150, 23);
            this.cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Location = new Point(560, 10);
            this.btnSearch.Size = new Size(80, 27);
            this.btnSearch.BackColor = Color.Blue;
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Click += BtnSearch_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(650, 10);
            this.btnRefresh.Size = new Size(80, 27);
            this.btnRefresh.BackColor = Color.Green;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(this.txtSearch);
            searchPanel.Controls.Add(lblInstructor);
            searchPanel.Controls.Add(this.cmbInstructor);
            searchPanel.Controls.Add(this.btnSearch);
            searchPanel.Controls.Add(this.btnRefresh);

            // Courses ListView
            this.coursesListView.Location = new Point(20, 90);
            this.coursesListView.Size = new Size(940, 500);
            this.coursesListView.View = View.Details;
            this.coursesListView.FullRowSelect = true;
            this.coursesListView.GridLines = true;
            this.coursesListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.coursesListView.Columns.Add("Khóa học", 300);
            this.coursesListView.Columns.Add("Giảng viên", 150);
            this.coursesListView.Columns.Add("Mô tả", 300);
            this.coursesListView.Columns.Add("Ngày tạo", 100);
            this.coursesListView.Columns.Add("Trạng thái", 80);

            this.coursesListView.DoubleClick += CoursesListView_DoubleClick;

            // Action Buttons
            this.btnEnroll.Text = "Đăng ký khóa học";
            this.btnEnroll.Location = new Point(20, 610);
            this.btnEnroll.Size = new Size(150, 30);
            this.btnEnroll.BackColor = Color.Orange;
            this.btnEnroll.ForeColor = Color.White;
            this.btnEnroll.Click += BtnEnroll_Click;

            // Add controls
            this.Controls.Add(searchPanel);
            this.Controls.Add(this.coursesListView);
            this.Controls.Add(this.btnEnroll);

            this.ResumeLayout(false);
        }

        #endregion
    }
}