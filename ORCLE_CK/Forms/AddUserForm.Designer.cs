using System.Windows.Forms;
using System;
using System.Drawing;

namespace ORCLE_CK.Forms
{
    partial class AddUserForm
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
            this.txtFullName = new TextBox();
            this.txtUsername = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPassword = new TextBox();
            this.cmbRole = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Thêm người dùng mới";
            this.Size = new System.Drawing.Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9);

            //// Header Panel
            //var headerPanel = new Panel
            //{
            //    Height = 60,
            //    Dock = DockStyle.Top,
            //    BackColor = Color.FromArgb(0, 120, 215)
            //};

            //var titleLabel = new Label
            //{
            //    Text = "Thêm người dùng mới",
            //    Font = new Font("Segoe UI", 16, FontStyle.Bold),
            //    ForeColor = Color.White,
            //    AutoSize = true,
            //    Location = new Point(20, 15)
            //};

            //headerPanel.Controls.Add(titleLabel);

            // Content Panel
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            // Full Name
            var lblFullName = new Label 
            { 
                Text = "Họ tên:", 
                Location = new Point(20, 20), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtFullName.Location = new Point(150, 20);
            this.txtFullName.Size = new Size(280, 25);
            this.txtFullName.Font = new Font("Segoe UI", 10);
            this.txtFullName.BorderStyle = BorderStyle.FixedSingle;

            // Username
            var lblUsername = new Label 
            { 
                Text = "Tên đăng nhập:", 
                Location = new Point(20, 60), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtUsername.Location = new Point(150, 60);
            this.txtUsername.Size = new Size(280, 25);
            this.txtUsername.Font = new Font("Segoe UI", 10);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;

            // Email
            var lblEmail = new Label 
            { 
                Text = "Email:", 
                Location = new Point(20, 100), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtEmail.Location = new Point(150, 100);
            this.txtEmail.Size = new Size(280, 25);
            this.txtEmail.Font = new Font("Segoe UI", 10);
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;

            // Password
            var lblPassword = new Label 
            { 
                Text = "Mật khẩu:", 
                Location = new Point(20, 140), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.txtPassword.Location = new Point(150, 140);
            this.txtPassword.Size = new Size(280, 25);
            this.txtPassword.Font = new Font("Segoe UI", 10);
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.UseSystemPasswordChar = true;

            // Role
            var lblRole = new Label 
            { 
                Text = "Vai trò:", 
                Location = new Point(20, 180), 
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            this.cmbRole.Location = new Point(150, 180);
            this.cmbRole.Size = new Size(280, 25);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new Font("Segoe UI", 10);
            this.cmbRole.FlatStyle = FlatStyle.Flat;
            this.cmbRole.Items.Add(new ComboBoxItem("Học viên","student"));
            this.cmbRole.Items.Add(new ComboBoxItem("Giảng viên", "instructor"));
            this.cmbRole.Items.Add(new ComboBoxItem("Quản trị viên", "admin"));
            this.cmbRole.SelectedIndex = 0;
            this.cmbRole.DisplayMember = "Text";
            this.cmbRole.ValueMember = "Value";

            // Buttons Panel
            var buttonPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(280, 15);
            this.btnSave.Size = new Size(100, 35);
            this.btnSave.BackColor = Color.FromArgb(0, 120, 215);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnSave.Cursor = Cursors.Hand;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(390, 15);
            this.btnCancel.Size = new Size(100, 35);
            this.btnCancel.BackColor = Color.FromArgb(96, 125, 139);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            buttonPanel.Controls.AddRange(new Control[] { this.btnSave, this.btnCancel });

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] 
            { 
                lblFullName, this.txtFullName,
                lblUsername, this.txtUsername,
                lblEmail, this.txtEmail,
                lblPassword, this.txtPassword,
                lblRole, this.cmbRole
            });

            // Add panels to form
            this.Controls.AddRange(new Control[] { contentPanel, buttonPanel });

            this.ResumeLayout(false);
        }


        #endregion
    }
}