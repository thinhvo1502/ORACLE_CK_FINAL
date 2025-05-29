using System.Drawing;
using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class AuditLogFor
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
            this.listViewLogs = new ListView();
            this.cmbUserFilter = new ComboBox();
            this.dtpFrom = new DateTimePicker();
            this.dtpTo = new DateTimePicker();
            this.btnRefresh = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.SuspendLayout();

            // Form
            this.Text = "Lịch sử thao tác";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            // User Filter
            var lblUser = new Label { Text = "Người dùng:", Location = new Point(20, 20), Size = new Size(80, 23) };
            this.cmbUserFilter.Location = new Point(100, 20);
            this.cmbUserFilter.Size = new Size(200, 23);
            this.cmbUserFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbUserFilter.SelectedIndexChanged += CmbUserFilter_SelectedIndexChanged;

            // Date Range
            var lblFrom = new Label { Text = "Từ ngày:", Location = new Point(320, 20), Size = new Size(60, 23) };
            this.dtpFrom.Location = new Point(380, 20);
            this.dtpFrom.Size = new Size(150, 23);
            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Value = DateTime.Today.AddDays(-7);
            this.dtpFrom.ValueChanged += DtpFrom_ValueChanged;

            var lblTo = new Label { Text = "Đến ngày:", Location = new Point(550, 20), Size = new Size(60, 23) };
            this.dtpTo.Location = new Point(610, 20);
            this.dtpTo.Size = new Size(150, 23);
            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Value = DateTime.Today;
            this.dtpTo.ValueChanged += DtpTo_ValueChanged;

            // Refresh Button
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(780, 19);
            this.btnRefresh.Size = new Size(80, 25);
            this.btnRefresh.Click += BtnRefresh_Click;

            // ListView
            this.listViewLogs.View = View.Details;
            this.listViewLogs.FullRowSelect = true;
            this.listViewLogs.GridLines = true;
            this.listViewLogs.Location = new Point(20, 60);
            this.listViewLogs.Size = new Size(940, 480);
            this.listViewLogs.Columns.Add("Thời gian", 150);
            this.listViewLogs.Columns.Add("Người dùng", 150);
            this.listViewLogs.Columns.Add("Hành động", 200);
            this.listViewLogs.Columns.Add("Chi tiết", 400);

            // Status Strip
            this.statusStrip.Items.Add(this.statusLabel);
            this.statusStrip.SizingGrip = false;

            // Add controls
            this.Controls.Add(lblUser);
            this.Controls.Add(this.cmbUserFilter);
            this.Controls.Add(lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listViewLogs);
            this.Controls.Add(this.statusStrip);

            this.ResumeLayout(false);
        }

        #endregion
    }
}