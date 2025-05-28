using ORCLE_CK.Constants;
using ORCLE_CK.Models;
using ORCLE_CK.Services;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class CourseStudentManagementForm : Form
    {
        private readonly Course course;
        private readonly UserService userService;
        private readonly EnrollmentService enrollmentService;

        private ListView listViewStudents;
        private Button btnRefresh, btnViewProgress, btnRemoveStudent, btnAddStudent;
        private TextBox txtSearch;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public CourseStudentManagementForm(Course course)
        {
            this.course = course ?? throw new ArgumentNullException(nameof(course));
            userService = new UserService();
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewStudents.Items.Clear();

                var enrollments = enrollmentService.GetEnrollmentsByCourse(course.CourseId);
                
                if (enrollments.Count == 0)
                {
                    var item = new ListViewItem("Chưa có học viên đăng ký");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    listViewStudents.Items.Add(item);
                    statusLabel.Text = "Chưa có học viên đăng ký";
                }
                else
                {
                    foreach (var enrollment in enrollments)
                    {
                        var item = new ListViewItem(enrollment.StudentName);
                        item.SubItems.Add(enrollment.EnrolledAt.ToString("dd/MM/yyyy"));
                        item.SubItems.Add($"{enrollment.Progress:F1}%");
                        item.SubItems.Add(GetStatusText(enrollment.Status));
                        item.SubItems.Add(enrollment.CompletedAt?.ToString("dd/MM/yyyy") ?? "Chưa hoàn thành");
                        item.SubItems.Add(enrollment.FinalGrade?.ToString("F1") ?? "Chưa có");
                        item.Tag = enrollment;

                        // Set color based on status
                        switch (enrollment.Status)
                        {
                            case EnrollmentStatus.Completed:
                                item.ForeColor = Color.Green;
                                break;
                            case EnrollmentStatus.Active:
                                item.ForeColor = Color.Blue;
                                break;
                            case EnrollmentStatus.Dropped:
                                item.ForeColor = Color.Red;
                                break;
                            case EnrollmentStatus.Suspended:
                                item.ForeColor = Color.Orange;
                                break;
                        }

                        listViewStudents.Items.Add(item);
                    }
                    statusLabel.Text = $"Đã tải {enrollments.Count} học viên";
                }

                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading students: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private string GetStatusText(EnrollmentStatus status)
        {
            return status switch
            {
                EnrollmentStatus.Active => "Đang học",
                EnrollmentStatus.Completed => "Hoàn thành",
                EnrollmentStatus.Dropped => "Đã rời khóa",
                EnrollmentStatus.Suspended => "Tạm dừng",
                _ => "Không xác định"
            };
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewStudents.SelectedItems.Count > 0;
            btnViewProgress.Enabled = hasSelection;
            btnRemoveStudent.Enabled = hasSelection;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchStudents();
        }

        private void SearchStudents()
        {
            try
            {
                string searchText = txtSearch.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadStudents();
                    return;
                }

                statusLabel.Text = "Đang tìm kiếm...";
                listViewStudents.Items.Clear();

                var enrollments = enrollmentService.GetEnrollmentsByCourse(course.CourseId);
                var filteredEnrollments = enrollments.FindAll(e => 
                    e.StudentName.ToLower().Contains(searchText));

                foreach (var enrollment in filteredEnrollments)
                {
                    var item = new ListViewItem(enrollment.StudentName);
                    item.SubItems.Add(enrollment.EnrolledAt.ToString("dd/MM/yyyy"));
                    item.SubItems.Add($"{enrollment.Progress:F1}%");
                    item.SubItems.Add(GetStatusText(enrollment.Status));
                    item.SubItems.Add(enrollment.CompletedAt?.ToString("dd/MM/yyyy") ?? "Chưa hoàn thành");
                    item.SubItems.Add(enrollment.FinalGrade?.ToString("F1") ?? "Chưa có");
                    item.Tag = enrollment;

                    switch (enrollment.Status)
                    {
                        case EnrollmentStatus.Completed:
                            item.ForeColor = Color.Green;
                            break;
                        case EnrollmentStatus.Active:
                            item.ForeColor = Color.Blue;
                            break;
                        case EnrollmentStatus.Dropped:
                            item.ForeColor = Color.Red;
                            break;
                        case EnrollmentStatus.Suspended:
                            item.ForeColor = Color.Orange;
                            break;
                    }

                    listViewStudents.Items.Add(item);
                }

                statusLabel.Text = $"Tìm thấy {filteredEnrollments.Count} học viên";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error searching students: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tìm kiếm học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tìm kiếm";
            }
        }

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                using var addForm = new AddStudentToCourseForm(course);
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error adding student: {ex.Message}", ex);
                MessageBox.Show($"Lỗi thêm học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewProgress_Click(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count == 0) return;

            try
            {
                var enrollment = (Enrollment)listViewStudents.SelectedItems[0].Tag;
                using var progressForm = new StudentProgressForm(enrollment);
                progressForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error viewing progress: {ex.Message}", ex);
                MessageBox.Show($"Lỗi xem tiến độ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count == 0) return;

            try
            {
                var enrollment = (Enrollment)listViewStudents.SelectedItems[0].Tag;
                if (MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa học viên {enrollment.StudentName} khỏi khóa học này?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (enrollmentService.RemoveStudentFromCourse(enrollment.EnrollmentId))
                    {
                        MessageBox.Show("Đã xóa học viên khỏi khóa học thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudents();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa học viên khỏi khóa học!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error removing student: {ex.Message}", ex);
                MessageBox.Show($"Lỗi xóa học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
    }
}
