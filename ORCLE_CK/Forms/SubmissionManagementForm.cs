using ORCLE_CK.Models;
using ORCLE_CK.Services;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class SubmissionManagementForm : Form
    {
        private readonly AssignmentService assignmentService;
        private readonly int assignmentId;
        private ListView listViewSubmissions;
        private Button btnGrade, btnRefresh, btnViewDetails;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public SubmissionManagementForm(int assignmentId)
        {
            this.assignmentId = assignmentId;
            assignmentService = new AssignmentService();
            InitializeComponent();
            LoadSubmissions();
        }

        

        private void LoadSubmissions()
        {
            try
            {
                statusLabel.Text = "Đang tải dữ liệu...";
                listViewSubmissions.Items.Clear();

                var submissions = assignmentService.GetSubmissionsByAssignment(assignmentId);

                foreach (var submission in submissions)
                {
                    var item = new ListViewItem(submission.SubmissionId.ToString());
                    item.SubItems.Add(submission.StudentName);
                    item.SubItems.Add(
                        submission.Content == null
                            ? "..."
                            : submission.Content.Substring(0, Math.Min(50, submission.Content.Length)) + "..."
                    );
                    item.SubItems.Add(submission.SubmittedAt.ToString("dd/MM/yyyy HH:mm"));
                    item.SubItems.Add(submission.Score?.ToString() ?? "Chưa chấm");
                    item.SubItems.Add(submission.MaxScore.ToString());
                    item.SubItems.Add(
                        submission.Feedback == null
                            ? "..."
                            : submission.Feedback.Substring(0, Math.Min(30, submission.Feedback.Length)) + "..."
                    );
                    item.SubItems.Add(submission.Status);
                    item.Tag = submission;

                    if (submission.Status == "Graded")
                        item.ForeColor = Color.Green;
                    else if (submission.Status == "Late")
                        item.ForeColor = Color.Red;

                    listViewSubmissions.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {submissions.Count} bài nộp";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading submissions: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách bài nộp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewSubmissions.SelectedItems.Count > 0;
            btnGrade.Enabled = hasSelection;
            btnViewDetails.Enabled = hasSelection;
        }

        private void BtnGrade_Click(object sender, EventArgs e)
        {
            if (listViewSubmissions.SelectedItems.Count == 0) return;

            var selectedSubmission = (Submission)listViewSubmissions.SelectedItems[0].Tag;
            using var gradeForm = new GradeSubmissionForm(selectedSubmission);
            if (gradeForm.ShowDialog() == DialogResult.OK)
            {
                LoadSubmissions();
                statusLabel.Text = "Chấm điểm thành công!";
            }
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (listViewSubmissions.SelectedItems.Count == 0) return;

            var selectedSubmission = (Submission)listViewSubmissions.SelectedItems[0].Tag;
            using var detailForm = new SubmissionDetailForm(selectedSubmission);
            detailForm.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadSubmissions();
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            BtnViewDetails_Click(sender, e);
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
    }
}
