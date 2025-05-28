using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CourseStudentManagementForm
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
            this.listViewStudents = new ListView();
            this.btnRefresh = new Button();
            this.btnViewProgress = new Button();
            this.btnRemoveStudent = new Button();
            this.btnAddStudent = new Button();
            this.txtSearch = new TextBox();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = $"Học viên khóa học: {course.Title}";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Search controls
            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 20), Size = new Size(70, 23) };
            this.txtSearch.Location = new Point(100, 20);
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.KeyDown += TxtSearch_KeyDown;

            // Buttons
            this.btnAddStudent.Text = "Thêm học viên";
            this.btnAddStudent.Location = new Point(320, 19);
            this.btnAddStudent.Size = new Size(100, 25);
            this.btnAddStudent.BackColor = Color.Green;
            this.btnAddStudent.ForeColor = Color.White;
            this.btnAddStudent.Click += BtnAddStudent_Click;

            this.btnViewProgress.Text = "Xem tiến độ";
            this.btnViewProgress.Location = new Point(430, 19);
            this.btnViewProgress.Size = new Size(100, 25);
            this.btnViewProgress.BackColor = Color.Blue;
            this.btnViewProgress.ForeColor = Color.White;
            this.btnViewProgress.Click += BtnViewProgress_Click;

            this.btnRemoveStudent.Text = "Xóa học viên";
            this.btnRemoveStudent.Location = new Point(540, 19);
            this.btnRemoveStudent.Size = new Size(100, 25);
            this.btnRemoveStudent.BackColor = Color.Red;
            this.btnRemoveStudent.ForeColor = Color.White;
            this.btnRemoveStudent.Click += BtnRemoveStudent_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(650, 19);
            this.btnRefresh.Size = new Size(80, 25);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewStudents.Location = new Point(20, 60);
            this.listViewStudents.Size = new Size(940, 480);
            this.listViewStudents.View = View.Details;
            this.listViewStudents.FullRowSelect = true;
            this.listViewStudents.GridLines = true;
            this.listViewStudents.MultiSelect = false;
            this.listViewStudents.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewStudents.Columns.Add("Họ tên", 200);
            this.listViewStudents.Columns.Add("Email", 200);
            this.listViewStudents.Columns.Add("Ngày đăng ký", 120);
            this.listViewStudents.Columns.Add("Tiến độ (%)", 100);
            this.listViewStudents.Columns.Add("Bài học hoàn thành", 120);
            this.listViewStudents.Columns.Add("Điểm trung bình", 100);
            this.listViewStudents.Columns.Add("Trạng thái", 100);

            this.listViewStudents.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 550);
            this.statusStrip.Size = new Size(980, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAddStudent);
            this.Controls.Add(this.btnViewProgress);
            this.Controls.Add(this.btnRemoveStudent);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewStudents);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}