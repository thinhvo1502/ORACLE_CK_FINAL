using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class QuizQuestionManagementForm
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
            this.listViewQuestions = new ListView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnRefresh = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Quản lý câu hỏi Quiz";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Buttons
            this.btnAdd.Text = "Thêm câu hỏi";
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

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(310, 20);
            this.btnRefresh.Size = new Size(80, 30);
            this.btnRefresh.BackColor = Color.Gray;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewQuestions.Location = new Point(20, 70);
            this.listViewQuestions.Size = new Size(940, 480);
            this.listViewQuestions.View = View.Details;
            this.listViewQuestions.FullRowSelect = true;
            this.listViewQuestions.GridLines = true;
            this.listViewQuestions.MultiSelect = false;

            this.listViewQuestions.Columns.Add("STT", 50);
            this.listViewQuestions.Columns.Add("Câu hỏi", 200);
            this.listViewQuestions.Columns.Add("Đáp án A", 150);
            this.listViewQuestions.Columns.Add("Đáp án B", 150);
            this.listViewQuestions.Columns.Add("Đáp án C", 150);
            this.listViewQuestions.Columns.Add("Đáp án D", 150);
            this.listViewQuestions.Columns.Add("Đáp án đúng", 100);
            this.listViewQuestions.Columns.Add("Điểm", 80);

            // Add controls
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewQuestions);

            this.ResumeLayout(false);
        }

        #endregion
    }
}