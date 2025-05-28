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
    public partial class CourseViewerForm : Form
    {
        private readonly Course course;
        private readonly User currentUser;
        private readonly LessonService lessonService;
        private readonly EnrollmentService enrollmentService;

        private Panel infoPanel, lessonsPanel;
        private ListView lessonsListView;
        private Label lblTitle, lblInstructor, lblDescription, lblProgress;
        private ProgressBar progressBar;
        private Button btnStartLesson, btnViewProgress;

        public CourseViewerForm(Course course, User user)
        {
            this.course = course ?? throw new ArgumentNullException(nameof(course));
            this.currentUser = user ?? throw new ArgumentNullException(nameof(user));
            lessonService = new LessonService();
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadCourseData();
        }
        private void LoadCourseData()
        {
            try
            {
                // Load enrollment data
                var enrollment = enrollmentService.GetEnrollment(currentUser.UserId, course.CourseId);
                if (enrollment != null)
                {
                    progressBar.Value = (int)Math.Min(enrollment.Progress, 100);
                    lblProgress.Text = $"Tiến độ học tập: {enrollment.Progress:F1}%";
                }

                // Load lessons
                LoadLessons();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading course data: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải dữ liệu khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLessons()
        {
            try
            {
                lessonsListView.Items.Clear();
                var lessons = lessonService.GetLessonsByCourse(course.CourseId);

                foreach (var lesson in lessons)
                {
                    var item = new ListViewItem(lesson.OrderNumber.ToString());
                    item.SubItems.Add(lesson.Title);
                    item.SubItems.Add($"{lesson.Duration} phút");
                    item.SubItems.Add(lesson.IsActive ? "Có thể học" : "Chưa mở");
                    item.SubItems.Add("Chưa hoàn thành"); // TODO: Track lesson completion
                    item.Tag = lesson;

                    if (!lesson.IsActive)
                        item.ForeColor = Color.Gray;

                    lessonsListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading lessons: {ex.Message}", ex);
            }
        }

        private void BtnStartLesson_Click(object sender, EventArgs e)
        {
            StartSelectedLesson();
        }

        private void BtnViewProgress_Click(object sender, EventArgs e)
        {
            //using var progressForm = new StudentProgressForm(currentUser);
            //progressForm.ShowDialog();
        }

        private void LessonsListView_DoubleClick(object sender, EventArgs e)
        {
            StartSelectedLesson();
        }

        private void StartSelectedLesson()
        {
            if (lessonsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bài học để bắt đầu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedLesson = (Lesson)lessonsListView.SelectedItems[0].Tag;

            if (!selectedLesson.IsActive)
            {
                MessageBox.Show("Bài học này chưa được mở!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var lessonViewerForm = new LessonViewerForm(selectedLesson, currentUser);
            if (lessonViewerForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh course data after completing lesson
                LoadCourseData();
            }
        }
    }
}
