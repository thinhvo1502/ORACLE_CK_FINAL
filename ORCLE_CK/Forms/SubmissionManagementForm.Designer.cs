using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class SubmissionManagementForm
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
            this.listViewSubmissions = new ListView();
            this.btnGrade = new Button();
            this.btnRefresh = new Button();
            this.btnViewDetails = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý bài nộp";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Buttons
            this.btnGrade.Text = "Chấm điểm";
            this.btnGrade.Location = new Point(20, 20);
            this.btnGrade.Size = new Size(100, 30);
            this.btnGrade.BackColor = Color.Green;
            this.btnGrade.ForeColor = Color.White;
            this.btnGrade.Click += BtnGrade_Click;

            this.btnViewDetails.Text = "Xem chi tiết";
            this.btnViewDetails.Location = new Point(130, 20);
            this.btnViewDetails.Size = new Size(100, 30);
            this.btnViewDetails.BackColor = Color.Blue;
            this.btnViewDetails.ForeColor = Color.White;
            this.btnViewDetails.Click += BtnViewDetails_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(240, 20);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewSubmissions.Location = new Point(20, 70);
            this.listViewSubmissions.Size = new Size(1140, 550);
            this.listViewSubmissions.View = View.Details;
            this.listViewSubmissions.FullRowSelect = true;
            this.listViewSubmissions.GridLines = true;
            this.listViewSubmissions.MultiSelect = false;
            this.listViewSubmissions.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewSubmissions.Columns.Add("ID", 60);
            this.listViewSubmissions.Columns.Add("Học viên", 200);
            this.listViewSubmissions.Columns.Add("Nội dung", 300);
            this.listViewSubmissions.Columns.Add("Thời gian nộp", 150);
            this.listViewSubmissions.Columns.Add("Điểm", 80);
            this.listViewSubmissions.Columns.Add("Điểm tối đa", 100);
            this.listViewSubmissions.Columns.Add("Phản hồi", 200);
            this.listViewSubmissions.Columns.Add("Trạng thái", 100);

            this.listViewSubmissions.DoubleClick += ListView_DoubleClick;
            this.listViewSubmissions.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 640);
            this.statusStrip.Size = new Size(1180, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(this.btnGrade);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewSubmissions);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}