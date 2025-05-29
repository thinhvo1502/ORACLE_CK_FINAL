using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class AddStudentToCourseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listViewStudents = new ListView();
            this.txtSearch = new TextBox();
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Thêm học viên vào khóa học";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Search controls
            var lblSearch = new Label { Text = "Tìm kiếm:", Location = new Point(20, 20), Size = new Size(70, 23) };
            this.txtSearch.Location = new Point(100, 20);
            this.txtSearch.Size = new Size(200, 23);
            this.txtSearch.KeyDown += TxtSearch_KeyDown;

            // Buttons
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Location = new Point(500, 19);
            this.btnAdd.Size = new Size(100, 30);
            this.btnAdd.BackColor = Color.Green;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(620, 19);
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // ListView
            this.listViewStudents.Location = new Point(20, 60);
            this.listViewStudents.Size = new Size(740, 480);
            this.listViewStudents.View = View.Details;
            this.listViewStudents.FullRowSelect = true;
            this.listViewStudents.GridLines = true;
            this.listViewStudents.MultiSelect = false;

            this.listViewStudents.Columns.Add("Họ tên", 300);
            this.listViewStudents.Columns.Add("Email", 250);
            this.listViewStudents.Columns.Add("Số điện thoại", 150);

            // Status Strip
            this.statusStrip.Items.Add(this.statusLabel);
            this.statusStrip.SizingGrip = false;
            this.statusLabel.Text = "Sẵn sàng";

            // Add controls
            this.Controls.Add(lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listViewStudents);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
} 