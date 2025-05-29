using System;
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
        
        private Panel panelButtons;


        private void InitializeComponent()
        {
            this.Text = "Quản lý bài học";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 9F);
            this.BackColor = Color.White;

            // Panel chứa các nút
            panelButtons = new Panel
            {
                Location = new Point(20, 10),
                Size = new Size(940, 50),
                BackColor = Color.WhiteSmoke
            };

            // Các nút
            btnAdd = CreateStyledButton("Thêm", Color.FromArgb(40, 167, 69), new EventHandler(BtnAdd_Click));
            btnEdit = CreateStyledButton("Sửa", Color.FromArgb(255, 193, 7), new EventHandler(BtnEdit_Click));
            btnDelete = CreateStyledButton("Xóa", Color.FromArgb(220, 53, 69), new EventHandler(BtnDelete_Click));
            btnMoveUp = CreateStyledButton("Lên", Color.FromArgb(0, 123, 255), new EventHandler(BtnMoveUp_Click));
            btnMoveDown = CreateStyledButton("Xuống", Color.FromArgb(0, 123, 255), new EventHandler(BtnMoveDown_Click));
            btnRefresh = CreateStyledButton("Làm mới", Color.FromArgb(108, 117, 125), new EventHandler(BtnRefresh_Click));

            int spacing = 10;
            int x = 0;
            foreach (var btn in new[] { btnAdd, btnEdit, btnDelete, btnMoveUp, btnMoveDown, btnRefresh })
            {
                btn.Location = new Point(x, 10);
                panelButtons.Controls.Add(btn);
                x += btn.Width + spacing;
            }

            // ListView hiển thị danh sách bài học
            listViewLessons = new ListView
            {
                Location = new Point(20, 70),
                Size = new Size(940, 440),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.White,
                ForeColor = Color.Black,
                HeaderStyle = ColumnHeaderStyle.Clickable
            };
            listViewLessons.Columns.Add("ID", 60);
            listViewLessons.Columns.Add("Thứ tự", 80);
            listViewLessons.Columns.Add("Tiêu đề", 300);
            listViewLessons.Columns.Add("Nội dung", 300);
            listViewLessons.Columns.Add("Video URL", 200);
            listViewLessons.Columns.Add("Thời lượng", 100);
            listViewLessons.Columns.Add("Trạng thái", 80);
            listViewLessons.DoubleClick += ListView_DoubleClick;
            listViewLessons.SelectedIndexChanged += ListView_SelectedIndexChanged;

            // Status strip
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel("Sẵn sàng");
            statusStrip.Items.Add(statusLabel);
            statusStrip.Location = new Point(0, 530);
            statusStrip.Size = new Size(980, 22);

            // Thêm các control vào Form
            this.Controls.Add(panelButtons);
            this.Controls.Add(listViewLessons);
            this.Controls.Add(statusStrip);
        }

        private Button CreateStyledButton(string text, Color backColor, EventHandler clickHandler)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(90, 30),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += clickHandler;
            return btn;
        }

        #endregion
    }
}