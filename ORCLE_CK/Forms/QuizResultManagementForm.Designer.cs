using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class QuizResultManagementForm
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
            this.listViewResults = new ListView();
            this.btnRefresh = new Button();
            this.btnViewDetail = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Kết quả Quiz";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Buttons
            this.btnViewDetail.Text = "Xem chi tiết";
            this.btnViewDetail.Location = new Point(20, 20);
            this.btnViewDetail.Size = new Size(100, 30);
            this.btnViewDetail.BackColor = Color.Blue;
            this.btnViewDetail.ForeColor = Color.White;
            this.btnViewDetail.Click += BtnViewDetail_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(130, 20);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewResults.Location = new Point(20, 70);
            this.listViewResults.Size = new Size(940, 480);
            this.listViewResults.View = View.Details;
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.GridLines = true;
            this.listViewResults.MultiSelect = false;

            this.listViewResults.Columns.Add("Học viên", 200);
            this.listViewResults.Columns.Add("Điểm", 80);
            this.listViewResults.Columns.Add("Điểm tối đa", 100);
            this.listViewResults.Columns.Add("Phần trăm", 100);
            this.listViewResults.Columns.Add("Thời gian làm", 120);
            this.listViewResults.Columns.Add("Ngày làm", 150);
            this.listViewResults.Columns.Add("Trạng thái", 100);

            // Add controls
            this.Controls.Add(this.btnViewDetail);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewResults);

            this.ResumeLayout(false);
        }

        #endregion
    }
}