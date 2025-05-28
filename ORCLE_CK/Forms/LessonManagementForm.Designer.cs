using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class LessonManagementForm
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
            this.listViewLessons = new ListView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();
            this.btnMoveUp = new Button();
            this.btnMoveDown = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý bài học";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // Buttons
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Location = new Point(20, 20);
            this.btnAdd.Size = new Size(80, 25);
            this.btnAdd.BackColor = Color.Green;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnEdit.Text = "Sửa";
            this.btnEdit.Location = new Point(110, 20);
            this.btnEdit.Size = new Size(80, 25);
            this.btnEdit.BackColor = Color.Orange;
            this.btnEdit.ForeColor = Color.White;
            this.btnEdit.Click += BtnEdit_Click;

            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new Point(200, 20);
            this.btnDelete.Size = new Size(80, 25);
            this.btnDelete.BackColor = Color.Red;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnMoveUp.Text = "Lên";
            this.btnMoveUp.Location = new Point(290, 20);
            this.btnMoveUp.Size = new Size(60, 25);
            this.btnMoveUp.BackColor = Color.Blue;
            this.btnMoveUp.ForeColor = Color.White;
            this.btnMoveUp.Click += BtnMoveUp_Click;

            this.btnMoveDown.Text = "Xuống";
            this.btnMoveDown.Location = new Point(360, 20);
            this.btnMoveDown.Size = new Size(60, 25);
            this.btnMoveDown.BackColor = Color.Blue;
            this.btnMoveDown.ForeColor = Color.White;
            this.btnMoveDown.Click += BtnMoveDown_Click;

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(430, 20);
            this.btnRefresh.Size = new Size(80, 25);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewLessons.Location = new Point(20, 60);
            this.listViewLessons.Size = new Size(940, 480);
            this.listViewLessons.View = View.Details;
            this.listViewLessons.FullRowSelect = true;
            this.listViewLessons.GridLines = true;
            this.listViewLessons.MultiSelect = false;
            this.listViewLessons.Font = new Font("Microsoft Sans Serif", 9F);

            this.listViewLessons.Columns.Add("ID", 60);
            this.listViewLessons.Columns.Add("Thứ tự", 80);
            this.listViewLessons.Columns.Add("Tiêu đề", 300);
            this.listViewLessons.Columns.Add("Nội dung", 300);
            this.listViewLessons.Columns.Add("Video URL", 200);
            this.listViewLessons.Columns.Add("Thời lượng", 100);
            this.listViewLessons.Columns.Add("Trạng thái", 80);

            this.listViewLessons.DoubleClick += ListView_DoubleClick;
            this.listViewLessons.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status Strip
            this.statusStrip.Location = new Point(0, 550);
            this.statusStrip.Size = new Size(980, 22);
            this.statusLabel.Text = "Sẵn sàng";
            this.statusStrip.Items.Add(this.statusLabel);

            // Add controls
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewLessons);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}