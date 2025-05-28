using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class InstructorStudentManagementForm
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
            this.cmbCourseFilter = new ComboBox();
            this.txtSearch = new TextBox();
            this.btnRefresh = new Button();
            this.btnViewProgress = new Button();
            this.btnSendMessage = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý học viên";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Search controls
            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 20), Size = new Size(70, 23) };
            this.txtSearch.Location = new Point(100, 20);
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.KeyDown += TxtSearch_KeyDown;

            var lblCourse = new Label { Text = "Khóa học:", Location = new Point(320, 20), Size = new Size(70, 23) };
            this.cmbCourseFilter.Location = new Point(400, 20);
            this.cmbCourseFilter.Size = new Size(200, 23);
            this.cmbCourseFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCourseFilter.SelectedIndexChanged += CmbCourseFilter_SelectedIndexChanged;

            // Buttons
            this.btnViewProgress.Text = "Xem tiến độ";
            this.btnViewProgress.Location = new Point(620, 19);
            this.btnViewProgress.Size = new Size(100, 25);
            this.btnViewProgress.BackColor = Color.Blue;
            this.btnViewProgress.ForeColor = Color.White;
            this.btnViewProgress.Click += BtnViewProgress_Click;

            this.btnSendMessage.Text = "Gửi tin nhắn";
            this.btnSendMessage.Location = new Point(730, 19);
            this.btnSendMessage.Size = new Size(100, 25);
            this.btnSendMessage.BackColor = Color.Green;
            this.btnSendMessage.ForeColor = Color.White;
            this.btnSendMessage.Click += BtnSendMessage_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(840, 19);
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
            this.listViewStudents.Columns.Add("Khóa học", 200);
            this.listViewStudents.Columns.Add("Ngày đăng ký", 120);
            this.listViewStudents.Columns.Add("Tiến độ", 100);
            this.listViewStudents.Columns.Add("Điểm TB", 80);
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
            this.Controls.Add(lblCourse);
            this.Controls.Add(this.cmbCourseFilter);
            this.Controls.Add(this.btnViewProgress);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewStudents);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}