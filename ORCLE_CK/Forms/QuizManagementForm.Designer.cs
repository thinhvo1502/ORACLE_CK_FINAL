using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class QuizManagementForm
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
            this.listViewQuizzes = new ListView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.btnViewResults = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý Quiz";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Buttons
            this.btnAdd.Text = "Thêm Quiz";
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

            this.btnViewResults.Text = "Xem kết quả";
            this.btnViewResults.Location = new Point(310, 20);
            this.btnViewResults.Size = new Size(100, 30);
            this.btnViewResults.BackColor = Color.Blue;
            this.btnViewResults.ForeColor = Color.White;
            this.btnViewResults.Click += BtnViewResults_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(420, 20);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewQuizzes.Location = new Point(20, 70);
            this.listViewQuizzes.Size = new Size(1140, 550);
            this.listViewQuizzes.View = View.Details;
            this.listViewQuizzes.FullRowSelect = true;
            this.listViewQuizzes.GridLines = true;
            this.listViewQuizzes.MultiSelect = false;
            this.listViewQuizzes.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewQuizzes.Columns.Add("ID", 60);
            this.listViewQuizzes.Columns.Add("Tiêu đề", 300);
            this.listViewQuizzes.Columns.Add("Mô tả", 300);
            this.listViewQuizzes.Columns.Add("Thời gian (phút)", 120);
            this.listViewQuizzes.Columns.Add("Số câu hỏi", 100);
            this.listViewQuizzes.Columns.Add("Điểm tối đa", 100);
            this.listViewQuizzes.Columns.Add("Lượt làm", 100);
            this.listViewQuizzes.Columns.Add("Trạng thái", 100);

            this.listViewQuizzes.DoubleClick += ListView_DoubleClick;
            this.listViewQuizzes.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 640);
            this.statusStrip.Size = new Size(1180, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnViewResults);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewQuizzes);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}