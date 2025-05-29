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
    public partial class AuditLogFor : Form
    {
        private readonly UserService userService;
        private ListView listViewLogs;
        private ComboBox cmbUserFilter;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnRefresh;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public AuditLogFor()
        {
            userService = new UserService();
            InitializeComponent();
            LoadUsers();
            LoadAuditLogs();
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
                            al.log_timestamp,
                            u.username,
                            al.operation,
                            al.details
                        FROM audit_log al
                        JOIN users u ON al.user_id = u.user_id
                        WHERE al.log_timestamp BETWEEN :from_date AND :to_date + 1";

                    if (cmbUserFilter.SelectedIndex > 0)
                    {
                        query += " AND u.username = :username";
                    }

                    query += " ORDER BY al.log_timestamp DESC";

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
                                var item = new ListViewItem(((DateTime)reader["log_timestamp"]).ToString(AppConstants.DATETIME_FORMAT));
                                item.SubItems.Add(reader["username"].ToString());
                                item.SubItems.Add(reader["operation"].ToString());
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