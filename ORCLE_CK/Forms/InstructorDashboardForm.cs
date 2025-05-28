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
    public partial class InstructorDashboardForm : Form
    {
        private readonly User currentUser;
        private readonly CourseService courseService;
        private readonly UserService userService;
        private readonly LessonService lessonService;

        private Panel statsPanel;
        private ListView recentCoursesListView;
        private ListView recentStudentsListView;
        private Button btnViewAllCourses, btnCreateCourse, btnViewStudents;

        public InstructorDashboardForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            courseService = new CourseService();
            userService = new UserService();
            lessonService = new LessonService();
            InitializeComponent();
            LoadDashboardData();
        }
        private void LoadDashboardData()
        {
            try
            {
                LoadStatistics();
                LoadRecentCourses();
                LoadRecentStudents();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading dashboard data: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            var courses = courseService.GetCoursesByInstructor(currentUser.UserId);
            var totalCourses = courses.Count;
            var activeCourses = courses.Count(c => c.IsActive);
            var totalStudents = courses.Sum(c => c.EnrollmentCount);
            var totalLessons = 0;

            foreach (var course in courses)
            {
                var lessons = lessonService.GetLessonsByCourse(course.CourseId);
                totalLessons += lessons.Count;
            }

            // Clear existing controls
            statsPanel.Controls.Clear();

            // Create stat cards
            CreateStatCard("Tổng khóa học", totalCourses.ToString(), Color.Blue, new Point(20, 20));
            CreateStatCard("Khóa học hoạt động", activeCourses.ToString(), Color.Green, new Point(200, 20));
            CreateStatCard("Tổng học viên", totalStudents.ToString(), Color.Orange, new Point(380, 20));
            CreateStatCard("Tổng bài học", totalLessons.ToString(), Color.Purple, new Point(560, 20));
        }

        private void CreateStatCard(string title, string value, Color color, Point location)
        {
            var card = new Panel
            {
                Location = location,
                Size = new Size(150, 80),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(130, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblValue = new Label
            {
                Text = value,
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold),
                Location = new Point(10, 35),
                Size = new Size(130, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            statsPanel.Controls.Add(card);
        }

        private void LoadRecentCourses()
        {
            recentCoursesListView.Items.Clear();

            var courses = courseService.GetCoursesByInstructor(currentUser.UserId)
                .OrderByDescending(c => c.CreatedAt)
                .Take(10);

            foreach (var course in courses)
            {
                var lessons = lessonService.GetLessonsByCourse(course.CourseId);

                var item = new ListViewItem(course.Title);
                item.SubItems.Add(course.EnrollmentCount.ToString());
                item.SubItems.Add(lessons.Count.ToString());
                item.SubItems.Add(course.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                item.SubItems.Add(course.IsActive ? "Hoạt động" : "Vô hiệu");
                item.Tag = course;

                if (!course.IsActive)
                    item.ForeColor = Color.Gray;

                recentCoursesListView.Items.Add(item);
            }
        }

        private void LoadRecentStudents()
        {
            recentStudentsListView.Items.Clear();

            // TODO: Implement enrollment service to get recent enrollments
            // For now, show placeholder data
            var item = new ListViewItem("Chưa có dữ liệu");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            recentStudentsListView.Items.Add(item);
        }

        private void BtnCreateCourse_Click(object sender, EventArgs e)
        {
            using var addCourseForm = new AddCourseForm();
            if (addCourseForm.ShowDialog() == DialogResult.OK)
            {
                LoadDashboardData();
                MessageBox.Show("Tạo khóa học thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnViewAllCourses_Click(object sender, EventArgs e)
        {
            using var courseManagementForm = new InstructorCourseManagementForm(currentUser);
            courseManagementForm.ShowDialog();
            LoadDashboardData();
        }

        private void BtnViewStudents_Click(object sender, EventArgs e)
        {
            using var studentManagementForm = new InstructorStudentManagementForm(currentUser);
            studentManagementForm.ShowDialog();
        }

        private void RecentCoursesListView_DoubleClick(object sender, EventArgs e)
        {
            if (recentCoursesListView.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)recentCoursesListView.SelectedItems[0].Tag;
            using var courseDetailForm = new CourseDetailForm(selectedCourse, currentUser);
            courseDetailForm.ShowDialog();
            LoadDashboardData();
        }
    }
}
