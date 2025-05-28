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
    public partial class CourseEnrollmentForm : Form
    {
        private readonly User currentUser;
        private readonly CourseService courseService;
        private readonly EnrollmentService enrollmentService;

        private ListView coursesListView;
        private TextBox txtSearch;
        private ComboBox cmbInstructor;
        private Button btnSearch, btnEnroll, btnRefresh;

        public CourseEnrollmentForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            courseService = new CourseService();
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadAvailableCourses();
            LoadInstructors();
        }
        private void LoadAvailableCourses()
        {
            try
            {
                coursesListView.Items.Clear();
                var allCourses = courseService.GetAllCourses();
                var enrolledCourseIds = enrollmentService.GetEnrolledCourses(currentUser.UserId)
                    .Select(c => c.CourseId).ToList();

                // Filter out already enrolled courses
                var availableCourses = allCourses.Where(c =>
                    c.IsActive && !enrolledCourseIds.Contains(c.CourseId)).ToList();

                foreach (var course in availableCourses)
                {
                    var item = new ListViewItem(course.Title);
                    item.SubItems.Add(course.InstructorName ?? "");
                    item.SubItems.Add(course.Description ?? "");
                    item.SubItems.Add(course.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add("Có thể đăng ký");
                    item.Tag = course;

                    coursesListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading available courses: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInstructors()
        {
            try
            {
                var userService = new UserService();
                var instructors = userService.GetInstructors();

                cmbInstructor.Items.Clear();
                cmbInstructor.Items.Add("Tất cả giảng viên");

                foreach (var instructor in instructors)
                {
                    cmbInstructor.Items.Add(instructor.FullName);
                }

                cmbInstructor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading instructors: {ex.Message}", ex);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // TODO: Implement search functionality
            LoadAvailableCourses();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadAvailableCourses();
        }

        private void BtnEnroll_Click(object sender, EventArgs e)
        {
            EnrollInSelectedCourse();
        }

        private void CoursesListView_DoubleClick(object sender, EventArgs e)
        {
            EnrollInSelectedCourse();
        }

        private void EnrollInSelectedCourse()
        {
            if (coursesListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học để đăng ký!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedCourse = (Course)coursesListView.SelectedItems[0].Tag;

            var result = MessageBox.Show(
                $"Bạn có muốn đăng ký khóa học '{selectedCourse.Title}' không?",
                "Xác nhận đăng ký",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var success = enrollmentService.EnrollStudent(currentUser.UserId, selectedCourse.CourseId);

                    if (success)
                    {
                        MessageBox.Show("Đăng ký khóa học thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAvailableCourses(); // Refresh the list
                    }
                    else
                    {
                        MessageBox.Show("Đăng ký khóa học thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error enrolling in course: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi đăng ký khóa học: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        }
}
