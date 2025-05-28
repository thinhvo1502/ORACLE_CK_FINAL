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
    public partial class AssignmentManagementForm : Form
    {
        private readonly AssignmentService assignmentService;
        private readonly int courseId;
        private ListView listViewAssignments;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnViewSubmissions;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public AssignmentManagementForm(int courseId)
        {
            this.courseId = courseId;
            assignmentService = new AssignmentService();
            InitializeComponent();
            LoadAssignments();
        }

        

        private void LoadAssignments()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewAssignments.Items.Clear();

                var assignments = assignmentService.GetAssignmentsByCourse(courseId);

                foreach (var assignment in assignments)
                {
                    var item = new ListViewItem(assignment.AssignmentId.ToString());
                    item.SubItems.Add(assignment.Title);
                    item.SubItems.Add(
                        assignment.Description == null
                            ? "..."
                            : assignment.Description.Substring(0, Math.Min(50, assignment.Description.Length)) + "..."
                    );
                    item.SubItems.Add(assignment.DueDate?.ToString("dd/MM/yyyy HH:mm") ?? "Không giới hạn");
                    item.SubItems.Add(assignment.MaxScore.ToString());
                    item.SubItems.Add(assignment.SubmissionCount.ToString());
                    item.SubItems.Add(assignment.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = assignment;

                    if (!assignment.IsActive)
                        item.ForeColor = Color.Gray;
                    else if (assignment.DueDate.HasValue && assignment.DueDate < DateTime.Now)
                        item.ForeColor = Color.Red;

                    listViewAssignments.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {assignments.Count} bài tập";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading assignments: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách bài tập: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewAssignments.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnViewSubmissions.Enabled = hasSelection;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using var addForm = new AddAssignmentForm(courseId);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadAssignments();
                statusLabel.Text = "Thêm bài tập thành công!";
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewAssignments.SelectedItems.Count == 0) return;

            var selectedAssignment = (Assignment)listViewAssignments.SelectedItems[0].Tag;
            using var editForm = new EditAssignmentForm(selectedAssignment);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadAssignments();
                statusLabel.Text = "Cập nhật bài tập thành công!";
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewAssignments.SelectedItems.Count == 0) return;

            var selectedAssignment = (Assignment)listViewAssignments.SelectedItems[0].Tag;

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa bài tập '{selectedAssignment.Title}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (assignmentService.DeleteAssignment(selectedAssignment.AssignmentId))
                    {
                        LoadAssignments();
                        statusLabel.Text = "Xóa bài tập thành công!";
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa bài tập!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error deleting assignment: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi xóa bài tập: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnViewSubmissions_Click(object sender, EventArgs e)
        {
            if (listViewAssignments.SelectedItems.Count == 0) return;

            var selectedAssignment = (Assignment)listViewAssignments.SelectedItems[0].Tag;
            using var submissionForm = new SubmissionManagementForm(selectedAssignment.AssignmentId);
            submissionForm.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadAssignments();
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
