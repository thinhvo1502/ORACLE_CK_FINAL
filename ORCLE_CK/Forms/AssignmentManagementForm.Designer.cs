using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class AssignmentManagementForm
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
            this.listViewAssignments = new ListView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.btnViewSubmissions = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý bài tập";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Buttons
            this.btnAdd.Text = "Thêm bài tập";
            this.btnAdd.Location = new Point(20, 20);
            this.btnAdd.Size = new Size(100, 30);
            this.btnAdd.BackColor = Color.Green;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnEdit.Text = "Sửa";
            this.btnEdit.Location = new Point(130, 20);
            this.btnEdit.Size = new Size(80, 30);
            this.btnEdit.BackColor = Color.Orange;
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Click += BtnEdit_Click;

            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new Point(220, 20);
            this.btnDelete.Size = new Size(80, 30);
            this.btnDelete.BackColor = Color.Red;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnViewSubmissions.Text = "Xem bài nộp";
            this.btnViewSubmissions.Location = new Point(310, 20);
            this.btnViewSubmissions.Size = new Size(100, 30);
            this.btnViewSubmissions.BackColor = Color.Blue;
            this.btnViewSubmissions.ForeColor = Color.White;
            this.btnViewSubmissions.Click += BtnViewSubmissions_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(420, 20);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewAssignments.Location = new Point(20, 70);
            this.listViewAssignments.Size = new Size(1140, 550);
            this.listViewAssignments.View = View.Details;
            this.listViewAssignments.FullRowSelect = true;
            this.listViewAssignments.GridLines = true;
            this.listViewAssignments.MultiSelect = false;
            this.listViewAssignments.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewAssignments.Columns.Add("ID", 60);
            this.listViewAssignments.Columns.Add("Tiêu đề", 300);
            this.listViewAssignments.Columns.Add("Mô tả", 400);
            this.listViewAssignments.Columns.Add("Hạn nộp", 150);
            this.listViewAssignments.Columns.Add("Điểm tối đa", 100);
            this.listViewAssignments.Columns.Add("Số bài nộp", 100);
            this.listViewAssignments.Columns.Add("Trạng thái", 100);

            this.listViewAssignments.DoubleClick += ListView_DoubleClick;
            this.listViewAssignments.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 640);
            this.statusStrip.Size = new Size(1180, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnViewSubmissions);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewAssignments);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}