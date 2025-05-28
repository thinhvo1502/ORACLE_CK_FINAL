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
    public partial class CourseManagementForm : Form
    {
        private readonly CourseService courseService;
        private ListView listViewCourses;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnSearch, btnViewLessons;
        private TextBox txtSearch;
        private ComboBox cmbInstructorFilter;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public CourseManagementForm()
        {
            courseService = new CourseService();
            InitializeComponent();
            LoadCourses();
            LoadInstructors();
        }
        private void LoadCourses()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewCourses.Items.Clear();

                var courses = courseService.GetAllCourses();

                foreach (var course in courses)
                {
                    var item = new ListViewItem(course.CourseId.ToString());
                    item.SubItems.Add(course.Title);
                    item.SubItems.Add(course.Description ?? "");
                    item.SubItems.Add(course.InstructorName ?? "");
                    item.SubItems.Add(course.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add(course.EnrollmentCount.ToString());
                    item.SubItems.Add(course.LessonCount.ToString());
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

        private void LoadInstructors()
        {
            try
            {
                var userService = new UserService();
                var instructors = userService.GetInstructors();

                cmbInstructorFilter.Items.Clear();
                cmbInstructorFilter.Items.Add("Tất cả");

                foreach (var instructor in instructors)
                {
                    cmbInstructorFilter.Items.Add(instructor.FullName);
                }

                cmbInstructorFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading instructors: {ex.Message}", ex);
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewCourses.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnViewLessons.Enabled = hasSelection;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearch_Click(sender, e);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchCourses();
        }

        private void SearchCourses()
        {
            try
            {
                var searchText = txtSearch.Text.Trim().ToLower();
                var selectedInstructor = cmbInstructorFilter.SelectedItem.ToString();

                foreach (ListViewItem item in listViewCourses.Items)
                {
                    var course = (Course)item.Tag;
                    bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                                       course.Title.ToLower().Contains(searchText) ||
                                       (course.Description?.ToLower().Contains(searchText) ?? false);

                    bool matchesInstructor = selectedInstructor == "Tất cả" ||
                                           course.InstructorName == selectedInstructor;

                    item.BackColor = (matchesSearch && matchesInstructor) ? Color.White : Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error searching courses: {ex.Message}", ex);
            }
        }

        private void CmbInstructorFilter_SelectedIndexChanged(object sender, EventArgs e)
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

        private void BtnViewLessons_Click(object sender, EventArgs e)
        {
            if (listViewCourses.SelectedItems.Count == 0) return;

            var selectedCourse = (Course)listViewCourses.SelectedItems[0].Tag;
            using var lessonForm = new LessonManagementForm(selectedCourse.CourseId);
            lessonForm.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCourses();
            LoadInstructors();
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
