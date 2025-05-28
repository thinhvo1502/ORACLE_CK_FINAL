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
    public partial class StudentCourseListForm : Form
    {
        private readonly User currentUser;
        private readonly EnrollmentService enrollmentService;

        private ListView coursesListView;
        private ComboBox cmbStatus;
        private TextBox txtSearch;
        private Button btnSearch, btnViewCourse, btnRefresh;

        public StudentCourseListForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadCourses();
        }
        private void LoadCourses()
        {
            try
            {
                coursesListView.Items.Clear();
                var enrollments = enrollmentService.GetEnrollmentsByStudent(currentUser.UserId);

                foreach (var enrollment in enrollments)
                {
                    var item = new ListViewItem(enrollment.CourseTitle);
                    item.SubItems.Add(enrollment.InstructorName);
                    item.SubItems.Add(enrollment.EnrolledAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add($"{enrollment.Progress:F1}%");
                    item.SubItems.Add(GetStatusText(enrollment.Status));
                    item.SubItems.Add(enrollment.FinalGrade?.ToString("F1") ?? "--");
                    item.SubItems.Add(enrollment.CompletedAt?.ToString(AppConstants.DATE_FORMAT) ?? "--");
                    item.Tag = enrollment;

                    // Color coding
                    if (enrollment.Progress >= 100)
                        item.BackColor = Color.LightGreen;
                    else if (enrollment.Progress > 0)
                        item.BackColor = Color.LightYellow;

                    coursesListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading courses: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // TODO: Implement search functionality
            LoadCourses();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void BtnViewCourse_Click(object sender, EventArgs e)
        {
            ViewSelectedCourse();
        }

        private void CoursesListView_DoubleClick(object sender, EventArgs e)
        {
            ViewSelectedCourse();
        }

        private void ViewSelectedCourse()
        {
            if (coursesListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học để xem!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedEnrollment = (Enrollment)coursesListView.SelectedItems[0].Tag;

            // Get course details
            var courseService = new CourseService();
            var course = courseService.GetCourseById(selectedEnrollment.CourseId);

            if (course != null)
            {
                using var courseViewerForm = new CourseViewerForm(course, currentUser);
                courseViewerForm.ShowDialog();
                LoadCourses(); // Refresh after viewing
            }
        }

    }
}
