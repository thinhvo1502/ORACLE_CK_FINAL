using ORCLE_CK.Constants;
using ORCLE_CK.Models;
using ORCLE_CK.Services;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace ORCLE_CK.Forms
{
    public partial class AuditLogForm : Form
    {
        private readonly UserService userService;
        private ListView listViewLogs;
        private ComboBox cmbUserFilter;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnRefresh;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public AuditLogForm()
        {
            userService = new UserService();
            InitializeComponent();
            LoadUsers();
            LoadAuditLogs();
        }

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

        private void LoadUsers()
        {
            try
            {
                var users = userService.GetAllUsers();
                cmbUserFilter.Items.Clear();
                cmbUserFilter.Items.Add("Tất cả người dùng");

                foreach (var user in users)
                {
                    cmbUserFilter.Items.Add(user.Username);
                }

                cmbUserFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading users: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAuditLogs()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewLogs.Items.Clear();

                string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            al.timestamp,
                            u.username,
                            al.action,
                            al.details
                        FROM audit_log al
                        JOIN users u ON al.user_id = u.user_id
                        WHERE al.timestamp BETWEEN :from_date AND :to_date + 1";

                    if (cmbUserFilter.SelectedIndex > 0)
                    {
                        query += " AND u.username = :username";
                    }

                    query += " ORDER BY al.timestamp DESC";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(":from_date", OracleDbType.Date).Value = dtpFrom.Value.Date;
                        command.Parameters.Add(":to_date", OracleDbType.Date).Value = dtpTo.Value.Date;

                        if (cmbUserFilter.SelectedIndex > 0)
                        {
                            command.Parameters.Add(":username", OracleDbType.Varchar2).Value = cmbUserFilter.SelectedItem.ToString();
                        }

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(((DateTime)reader["timestamp"]).ToString(AppConstants.DATETIME_FORMAT));
                                item.SubItems.Add(reader["username"].ToString());
                                item.SubItems.Add(reader["action"].ToString());
                                item.SubItems.Add(reader["details"].ToString());
                                listViewLogs.Items.Add(item);
                            }
                        }
                    }
                }

                if (listViewLogs.Items.Count == 0)
                {
                    var item = new ListViewItem("Không có dữ liệu");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    listViewLogs.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {listViewLogs.Items.Count} bản ghi";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading audit logs: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải lịch sử thao tác: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void CmbUserFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }

        private void DtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                dtpTo.Value = dtpFrom.Value;
            }
            LoadAuditLogs();
        }

        private void DtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTo.Value < dtpFrom.Value)
            {
                dtpFrom.Value = dtpTo.Value;
            }
            LoadAuditLogs();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadAuditLogs();
        }
    }
} 