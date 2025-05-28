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
    public partial class InstructorStudentManagementForm : Form
    {
        private readonly User currentUser;
        private readonly UserService userService;
        private readonly CourseService courseService;

        private ListView listViewStudents;
        private ComboBox cmbCourseFilter;
        private TextBox txtSearch;
        private Button btnRefresh, btnViewProgress, btnSendMessage;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public InstructorStudentManagementForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            userService = new UserService();
            courseService = new CourseService();
            InitializeComponent();
            LoadCourses();
            LoadStudents();
        }
        private void LoadCourses()
        {
            try
            {
                var courses = courseService.GetCoursesByInstructor(currentUser.UserId);

                cmbCourseFilter.Items.Clear();
                cmbCourseFilter.Items.Add("Tất cả khóa học");

                foreach (var course in courses)
                {
                    cmbCourseFilter.Items.Add(course.Title);
                }

                cmbCourseFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading courses: {ex.Message}", ex);
            }
        }

        private void LoadStudents()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewStudents.Items.Clear();

                // TODO: Implement enrollment service to get students by instructor
                // For now, show placeholder data
                var item = new ListViewItem("Chưa có dữ liệu học viên");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                listViewStudents.Items.Add(item);

                statusLabel.Text = "Chưa có dữ liệu học viên";
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

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewStudents.SelectedItems.Count > 0;
            btnViewProgress.Enabled = hasSelection;
            btnSendMessage.Enabled = hasSelection;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchStudents();
        }

        private void SearchStudents()
        {
            // TODO: Implement search functionality
        }

        private void CmbCourseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void BtnViewProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xem tiến độ đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSendMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng gửi tin nhắn đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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
