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
    public partial class CourseStudentManagementForm : Form
    {
        private readonly Course course;
        private readonly UserService userService;

        private ListView listViewStudents;
        private Button btnRefresh, btnViewProgress, btnRemoveStudent, btnAddStudent;
        private TextBox txtSearch;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public CourseStudentManagementForm(Course course)
        {
            this.course = course ?? throw new ArgumentNullException(nameof(course));
            userService = new UserService();
            InitializeComponent();
            LoadStudents();
        }
        private void LoadStudents()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewStudents.Items.Clear();

                // TODO: Implement enrollment service to get students for this course
                // For now, show placeholder data
                var item = new ListViewItem("Chưa có học viên đăng ký");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                listViewStudents.Items.Add(item);

                statusLabel.Text = "Chưa có học viên đăng ký";
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
            btnRemoveStudent.Enabled = hasSelection;
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

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thêm học viên đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnViewProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xem tiến độ đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count == 0) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa học viên này khỏi khóa học?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("Chức năng xóa học viên đang được phát triển!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
