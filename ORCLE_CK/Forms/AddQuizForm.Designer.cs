using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class AddQuizForm
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
            this.txtTitle = new TextBox();
            this.txtDescription = new TextBox();
            this.numTimeLimit = new NumericUpDown();
            this.chkHasTimeLimit = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Thêm Quiz mới";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label { Text = "Tiêu đề:", Location = new Point(20, 20), Size = new Size(100, 23) };
            this.txtTitle.Location = new Point(130, 20);
            this.txtTitle.Size = new Size(430, 23);

            // Description
            var lblDescription = new Label { Text = "Mô tả:", Location = new Point(20, 60), Size = new Size(100, 23) };
            this.txtDescription.Location = new Point(130, 60);
            this.txtDescription.Size = new Size(430, 150);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;

            // Time Limit
            this.chkHasTimeLimit.Text = "Có giới hạn thời gian";
            this.chkHasTimeLimit.Location = new Point(130, 230);
            this.chkHasTimeLimit.Size = new Size(150, 23);
            this.chkHasTimeLimit.CheckedChanged += ChkHasTimeLimit_CheckedChanged;

            var lblTimeLimit = new Label { Text = "Thời gian (phút):", Location = new Point(20, 270), Size = new Size(100, 23) };
            this.numTimeLimit.Location = new Point(130, 270);
            this.numTimeLimit.Size = new Size(100, 23);
            this.numTimeLimit.Minimum = 1;
            this.numTimeLimit.Maximum = 300;
            this.numTimeLimit.Value = 60;
            this.numTimeLimit.Enabled = false;

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new Point(400, 350);
            this.btnSave.Size = new Size(75, 30);
            this.btnSave.BackColor = Color.Green;
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new Point(485, 350);
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.chkHasTimeLimit);
            this.Controls.Add(lblTimeLimit);
            this.Controls.Add(this.numTimeLimit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            ((System.ComponentModel.ISupportInitialize)(this.numTimeLimit)).BeginInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}