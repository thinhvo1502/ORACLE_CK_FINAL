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
    public partial class StudentProgressForm : Form
    {
        private readonly User currentUser;
        private readonly EnrollmentService enrollmentService;

        private Panel summaryPanel, detailPanel;
        private ListView progressListView;
        private Label lblTotalCourses, lblCompletedCourses, lblAverageProgress, lblTotalHours;
        private ProgressBar overallProgressBar;
        private ComboBox cmbCourseFilter;
        private Button btnRefresh, btnViewCertificate;

        public StudentProgressForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadProgressData();
        }
        private void LoadProgressData()
        {
            try
            {
                var enrollments = enrollmentService.GetEnrollmentsByStudent(currentUser.UserId);

                // Update summary statistics
                var totalCourses = enrollments.Count;
                var completedCourses = enrollments.Count(e => e.Progress >= 100);
                var averageProgress = enrollments.Count > 0 ? enrollments.Average(e => e.Progress) : 0;
                var totalHours = enrollments.Sum(e => 0); // TODO: Calculate actual hours

                lblTotalCourses.Text = $"Tổng khóa học: {totalCourses}";
                lblCompletedCourses.Text = $"Đã hoàn thành: {completedCourses}";
                lblAverageProgress.Text = $"Tiến độ TB: {averageProgress:F1}%";
                lblTotalHours.Text = $"Tổng giờ học: {totalHours}h";

                overallProgressBar.Value = (int)Math.Min(averageProgress, 100);

                // Load detailed progress
                LoadDetailedProgress(enrollments);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading progress data: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải dữ liệu tiến độ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDetailedProgress(System.Collections.Generic.List<Enrollment> enrollments)
        {
            progressListView.Items.Clear();

            foreach (var enrollment in enrollments)
            {
                var item = new ListViewItem(enrollment.CourseTitle);
                item.SubItems.Add(enrollment.InstructorName);
                item.SubItems.Add(enrollment.EnrolledAt.ToString(AppConstants.DATE_FORMAT));
                item.SubItems.Add($"{enrollment.Progress:F1}%");
                item.SubItems.Add(enrollment.FinalGrade?.ToString("F1") ?? "--");
                item.SubItems.Add(GetStatusText(enrollment.Status));
                item.SubItems.Add(enrollment.CompletedAt?.ToString(AppConstants.DATE_FORMAT) ?? "--");
                item.Tag = enrollment;

                // Color coding based on progress
                if (enrollment.Progress >= 100)
                    item.BackColor = Color.LightGreen;
                else if (enrollment.Progress > 50)
                    item.BackColor = Color.LightYellow;
                else if (enrollment.Progress > 0)
                    item.BackColor = Color.LightBlue;

                progressListView.Items.Add(item);
            }
        }

        private string GetStatusText(EnrollmentStatus status)
        {
            return status switch
            {
                EnrollmentStatus.Active => "Đang học",
                EnrollmentStatus.Completed => "Hoàn thành",
                EnrollmentStatus.Dropped => "Đã bỏ",
                EnrollmentStatus.Suspended => "Tạm dừng",
                _ => "Không xác định"
            };
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadProgressData();
        }

        private void BtnViewCertificate_Click(object sender, EventArgs e)
        {
            using var certificateForm = new CertificateForm(currentUser);
            certificateForm.ShowDialog();
        }

        private void CmbCourseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Implement filtering
            LoadProgressData();
        }
    }
}
