using ORCLE_CK.Models;
using ORCLE_CK.Services;
using ORCLE_CK.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class AddStudentToCourseForm : Form
    {
        private readonly Course course;
        private readonly UserService userService;
        private readonly EnrollmentService enrollmentService;

        private ListView listViewStudents;
        private TextBox txtSearch;
        private Button btnAdd, btnCancel;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public AddStudentToCourseForm(Course course)
        {
            this.course = course ?? throw new ArgumentNullException(nameof(course));
            userService = new UserService();
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadAvailableStudents();
        }

        private void LoadAvailableStudents()
        {
            try
            {
                statusLabel.Text = "Đang tải danh sách học viên...";
                listViewStudents.Items.Clear();

                var students = userService.GetStudents();
                foreach (var student in students)
                {
                    // Skip if already enrolled
                    if (enrollmentService.IsEnrolled(student.UserId, course.CourseId))
                        continue;

                    var item = new ListViewItem(student.FullName);
                    item.SubItems.Add(student.Email);
                    item.Tag = student;
                    listViewStudents.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {listViewStudents.Items.Count} học viên";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading students: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void SearchStudents()
        {
            try
            {
                string searchText = txtSearch.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadAvailableStudents();
                    return;
                }

                statusLabel.Text = "Đang tìm kiếm...";
                listViewStudents.Items.Clear();

                var students = userService.GetStudents();
                foreach (var student in students)
                {
                    if (enrollmentService.IsEnrolled(student.UserId, course.CourseId))
                        continue;

                    if (student.FullName.ToLower().Contains(searchText) ||
                        student.Email.ToLower().Contains(searchText))
                    {
                        var item = new ListViewItem(student.FullName);
                        item.SubItems.Add(student.Email);
                        item.Tag = student;
                        listViewStudents.Items.Add(item);
                    }
                }

                statusLabel.Text = $"Tìm thấy {listViewStudents.Items.Count} học viên";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error searching students: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tìm kiếm học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tìm kiếm";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count == 0) return;

            try
            {
                var student = (User)listViewStudents.SelectedItems[0].Tag;
                if (enrollmentService.EnrollStudent(student.UserId, course.CourseId))
                {
                    MessageBox.Show("Đã thêm học viên vào khóa học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Học viên đã được thêm vào khóa học trước đó!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error adding student: {ex.Message}", ex);
                MessageBox.Show($"Lỗi thêm học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchStudents();
        }
    }
}