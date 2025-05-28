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
    public partial class InstructorCourseManagementForm : Form
    {
        private readonly User currentUser;
        private readonly CourseService courseService;
        private readonly LessonService lessonService;

        private ListView listViewCourses;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnViewLessons, btnViewStudents;
        private TextBox txtSearch;
        private ComboBox cmbStatusFilter;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public InstructorCourseManagementForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            courseService = new CourseService();
            lessonService = new LessonService();
            InitializeComponent();
            LoadCourses();
        }
        private void LoadCourses()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewCourses.Items.Clear();

                var courses = courseService.GetCoursesByInstructor(currentUser.UserId);

                foreach (var course in courses)
                {
                    var lessons = lessonService.GetLessonsByCourse(course.CourseId);

                    var item = new ListViewItem(course.CourseId.ToString());
                    item.SubItems.Add(course.Title);
                    if (!string.IsNullOrEmpty(course.Description))
                    {
                        string shortDesc = course.Description.Length > 50
                            ? course.Description.Substring(0, 50) + "..."
                            : course.Description;
                        item.SubItems.Add(shortDesc);
                    }
                    else
                    {
                        item.SubItems.Add("...");
                    }
                    item.SubItems.Add(course.EnrollmentCount.ToString());
                    item.SubItems.Add(lessons.Count.ToString());
                    item.SubItems.Add(course.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add(course.UpdatedAt?.ToString(AppConstants.DATE_FORMAT) ?? "");
                    item.SubItems.Add(course.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = course;

                    if (!course.IsActive)
                        item.ForeColor = Color.Gray;

                    listViewCourses.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {courses.Count} khóa học";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading courses: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewCourses.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnViewLessons.Enabled = hasSelection;
            btnViewStudents.Enabled = hasSelection;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchCourses();
        }

        private void SearchCourses()
        {
            try
            {
                var searchText = txtSearch.Text.Trim().ToLower();
                var selectedStatus = cmbStatusFilter.SelectedItem.ToString();

                foreach (ListViewItem item in listViewCourses.Items)
                {
                    var course = (Course)item.Tag;
                    bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                       course.Title.ToLower().Contains(searchText) ||
                                       (course.Description?.ToLower().Contains(searchText) ?? false);

                    bool matchesStatus = selectedStatus == "Tất cả" ||
                                       (selectedStatus == "Hoạt động" && course.IsActive) ||
                                       (selectedStatus == "Vô hiệu" && !course.IsActive);

                    item.BackColor = (matchesSearch && matchesStatus) ? Color.White : Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error searching courses: {ex.Message}", ex);
            }
        }

        private void CmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCourses();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using var addForm = new AddCourseForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadCourses();
                statusLabel.Text = MessageConstants.COURSE_CREATED_SUCCESS;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewCourses.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)listViewCourses.SelectedItems[0].Tag;
            using var editForm = new EditCourseForm(selectedCourse);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadCourses();
                statusLabel.Text = MessageConstants.COURSE_UPDATED_SUCCESS;
            }
        }

        private void BtnViewLessons_Click(object sender, EventArgs e)
        {
            if (listViewCourses.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)listViewCourses.SelectedItems[0].Tag;
            using var lessonForm = new LessonManagementForm(selectedCourse.CourseId);
            lessonForm.ShowDialog();
            LoadCourses();
        }

        private void BtnViewStudents_Click(object sender, EventArgs e)
        {
            if (listViewCourses.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)listViewCourses.SelectedItems[0].Tag;
            using var studentForm = new CourseStudentManagementForm(selectedCourse);
            studentForm.ShowDialog();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewCourses.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)listViewCourses.SelectedItems[0].Tag;

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khóa học '{selectedCourse.Title}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (courseService.DeleteCourse(selectedCourse.CourseId))
                    {
                        LoadCourses();
                        statusLabel.Text = MessageConstants.COURSE_DELETED_SUCCESS;
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa khóa học!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error deleting course: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi xóa khóa học: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            BtnEdit_Click(sender, e);
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
    }
}
