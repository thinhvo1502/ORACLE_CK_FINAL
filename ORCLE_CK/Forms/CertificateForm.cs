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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class CertificateForm : Form
    {
        private readonly User currentUser;
        private readonly EnrollmentService enrollmentService;

        private ListView certificatesListView;
        private Panel certificatePreviewPanel;
        private Label lblStudentName, lblCourseName, lblCompletionDate, lblGrade;
        private Button btnDownload, btnPrint, btnRefresh;

        public CertificateForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadCertificates();
        }
        private void LoadCertificates()
        {
            try
            {
                certificatesListView.Items.Clear();
                var enrollments = enrollmentService.GetEnrollmentsByStudent(currentUser.UserId);
                var completedCourses = enrollments.Where(e => e.Progress >= 100).ToList();

                foreach (var enrollment in completedCourses)
                {
                    var item = new ListViewItem(enrollment.CourseTitle);
                    item.SubItems.Add(enrollment.CompletedAt?.ToString(AppConstants.DATE_FORMAT) ?? "");
                    item.SubItems.Add(enrollment.FinalGrade?.ToString("F1") ?? "--");
                    item.SubItems.Add("Có sẵn");
                    item.Tag = enrollment;

                    certificatesListView.Items.Add(item);
                }

                if (completedCourses.Count == 0)
                {
                    var item = new ListViewItem("Chưa có chứng chỉ nào");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    certificatesListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading certificates: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách chứng chỉ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CertificatesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (certificatesListView.SelectedItems.Count == 0) return;

            var selectedEnrollment = certificatesListView.SelectedItems[0].Tag as Enrollment;
            if (selectedEnrollment != null)
            {
                UpdateCertificatePreview(selectedEnrollment);
            }
        }

        private void UpdateCertificatePreview(Enrollment enrollment)
        {
            lblStudentName.Text = currentUser.FullName;
            lblCourseName.Text = enrollment.CourseTitle;
            lblCompletionDate.Text = $"Ngày hoàn thành: {enrollment.CompletedAt?.ToString(AppConstants.DATE_FORMAT)}";
            lblGrade.Text = enrollment.FinalGrade.HasValue ?
                $"Điểm số: {enrollment.FinalGrade:F1}/10" : "Điểm số: Đạt";
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCertificates();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng tải xuống đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
