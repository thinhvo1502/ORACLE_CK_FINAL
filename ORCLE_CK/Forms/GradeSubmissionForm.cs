using ORCLE_CK.Models;
using ORCLE_CK.Services;
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
    public partial class GradeSubmissionForm : Form
    {
        private readonly AssignmentService assignmentService;
        private readonly Submission submission;

        private Label lblStudentInfo;
        private TextBox txtFileUrl;
        private NumericUpDown numGrade;
        private TextBox txtFeedback;
        private Button btnSave;
        private Button btnCancel;

        public GradeSubmissionForm(Submission submission)
        {
            this.submission = submission ?? throw new ArgumentNullException(nameof(submission));
            assignmentService = new AssignmentService();
            InitializeComponent();
            LoadSubmissionData();
        }

        private void LoadSubmissionData()
        {
            lblStudentInfo.Text = $"Học viên: {submission.StudentName}\n" +
                                 $"Bài tập: {submission.AssignmentTitle}\n" +
                                 $"Thời gian nộp: {submission.SubmittedAt:dd/MM/yyyy HH:mm}";

            txtFileUrl.Text = submission.FileUrl ?? "Không có file đính kèm";
            numGrade.Maximum = submission.MaxScore;
            numGrade.DecimalPlaces = 2;

            if (submission.Grade.HasValue)
            {
                numGrade.Value = (decimal)submission.Grade.Value;
                txtFeedback.Text = submission.Feedback ?? "";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal grade = numGrade.Value;
                string feedback = txtFeedback.Text.Trim();

                if (assignmentService.GradeSubmission(submission.SubmissionId, grade, feedback))
                {
                    MessageBox.Show("Chấm điểm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể lưu điểm!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
