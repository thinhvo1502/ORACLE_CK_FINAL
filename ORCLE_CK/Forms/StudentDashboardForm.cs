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
    public partial class StudentDashboardForm : Form
    {
        private readonly User currentUser;
        private readonly EnrollmentService enrollmentService;
        private readonly CourseService courseService;

        private Panel statsPanel, recentCoursesPanel, progressPanel;
        private ListView recentCoursesListView, progressListView;
        private Label lblTotalCourses, lblCompletedCourses, lblInProgressCourses, lblAverageProgress;
        private Button btnViewAllCourses, btnFindCourses, btnViewProgress;

        public StudentDashboardForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            enrollmentService = new EnrollmentService();
            courseService = new CourseService();
            InitializeComponent();
            LoadDashboardData();
        }
        private void LoadDashboardData()
        {
            try
            {
                //MessageBox.Show("ha");
                var enrollments = enrollmentService.GetEnrollmentsByStudent(currentUser.UserId);
                //MessageBox.Show("YA");
                var enrolledCourses = enrollmentService.GetEnrolledCourses(currentUser.UserId);
                //MessageBox.Show("haha");
                // Update statistics
                var totalCourses = enrollments.Count;
                var completedCourses = enrollments.Count(e => e.Progress >= 100);
                var inProgressCourses = enrollments.Count(e => e.Progress > 0 && e.Progress < 100);
                var averageProgress = enrollments.Count > 0 ? enrollments.Average(e => e.Progress) : 0;

                lblTotalCourses.Text = $"Tổng khóa học: {totalCourses}";
                lblCompletedCourses.Text = $"Đã hoàn thành: {completedCourses}";
                lblInProgressCourses.Text = $"Đang học: {inProgressCourses}";
                lblAverageProgress.Text = $"Tiến độ TB: {averageProgress:F1}%";

                // Load recent courses
                LoadRecentCourses(enrolledCourses.Take(5).ToList());

                // Load progress data
                LoadProgressData(enrollments.Take(5).ToList());
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading dashboard data: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecentCourses(System.Collections.Generic.List<Course> courses)
        {
            recentCoursesListView.Items.Clear();

            foreach (var course in courses)
            {
                var enrollment = enrollmentService.GetEnrollment(currentUser.UserId, course.CourseId);

                var item = new ListViewItem(course.Title);
                item.SubItems.Add(course.InstructorName ?? "");
                item.SubItems.Add($"{enrollment?.Progress ?? 0:F1}%");
                item.SubItems.Add(enrollment?.Status.ToString() ?? "");
                item.Tag = course;

                // Color coding based on progress
                if (enrollment?.Progress >= 100)
                    item.BackColor = Color.LightGreen;
                else if (enrollment?.Progress > 0)
                    item.BackColor = Color.LightYellow;

                recentCoursesListView.Items.Add(item);
            }
        }

        private void LoadProgressData(System.Collections.Generic.List<Enrollment> enrollments)
        {
            progressListView.Items.Clear();

            foreach (var enrollment in enrollments)
            {
                var item = new ListViewItem(enrollment.CourseTitle);
                item.SubItems.Add("--"); // Lesson count - TODO: implement
                item.SubItems.Add("--"); // Completed lessons - TODO: implement
                item.SubItems.Add($"{enrollment.Progress:F1}%");
                item.SubItems.Add(enrollment.FinalGrade?.ToString("F1") ?? "--");
                item.Tag = enrollment;

                progressListView.Items.Add(item);
            }
        }

        private void BtnViewAllCourses_Click(object sender, EventArgs e)
        {
            using var courseListForm = new StudentCourseListForm(currentUser);
            courseListForm.ShowDialog();
        }

        private void BtnFindCourses_Click(object sender, EventArgs e)
        {
            using var findCoursesForm = new CourseEnrollmentForm(currentUser);
            findCoursesForm.ShowDialog();
        }

        private void BtnViewProgress_Click(object sender, EventArgs e)
        {
            //using var progressForm = new StudentProgressForm(currentUser);
            //progressForm.ShowDialog();
        }

        private void RecentCoursesListView_DoubleClick(object sender, EventArgs e)
        {
            if (recentCoursesListView.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)recentCoursesListView.SelectedItems[0].Tag;
            using var courseViewerForm = new CourseViewerForm(selectedCourse, currentUser);
            courseViewerForm.ShowDialog();
        }

    }
}
