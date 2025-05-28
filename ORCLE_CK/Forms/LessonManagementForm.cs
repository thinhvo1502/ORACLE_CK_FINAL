using ORCLE_CK.Constants;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using ORCLE_CK.Services;
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
    public partial class LessonManagementForm : Form
    {
        private readonly LessonService lessonService;
        private readonly int courseId;
        private ListView listViewLessons;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnMoveUp, btnMoveDown;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public LessonManagementForm(int courseId)
        {
            this.courseId = courseId;
            lessonService = new LessonService();
            InitializeComponent();
            LoadLessons();
        }
        private void LoadLessons()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewLessons.Items.Clear();

                var lessons = lessonService.GetLessonsByCourse(courseId);

                foreach (var lesson in lessons)
                {
                    var item = new ListViewItem(lesson.LessonId.ToString());
                    item.SubItems.Add(lesson.OrderNumber.ToString());
                    item.SubItems.Add(lesson.Title);
                    item.SubItems.Add(
    lesson.Content != null
        ? lesson.Content.Substring(0, Math.Min(50, lesson.Content.Length)) + "..."
        : "...");
                    item.SubItems.Add(lesson.VideoUrl ?? "");
                    item.SubItems.Add($"{lesson.Duration} phút");
                    item.SubItems.Add(lesson.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = lesson;

                    if (!lesson.IsActive)
                        item.ForeColor = Color.Gray;

                    listViewLessons.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {lessons.Count} bài học";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading lessons: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewLessons.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnMoveUp.Enabled = hasSelection;
            btnMoveDown.Enabled = hasSelection;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using var addForm = new AddLessonForm(courseId);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadLessons();
                statusLabel.Text = "Thêm bài học thành công!";
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewLessons.SelectedItems.Count == 0) return;

            var selectedLesson = (Lesson)listViewLessons.SelectedItems[0].Tag;
            using var editForm = new EditLessonForm(selectedLesson);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadLessons();
                statusLabel.Text = "Cập nhật bài học thành công!";
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewLessons.SelectedItems.Count == 0) return;

            var selectedLesson = (Lesson)listViewLessons.SelectedItems[0].Tag;

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa bài học '{selectedLesson.Title}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (lessonService.DeleteLesson(selectedLesson.LessonId))
                    {
                        LoadLessons();
                        statusLabel.Text = "Xóa bài học thành công!";
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa bài học!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error deleting lesson: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi xóa bài học: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (listViewLessons.SelectedItems.Count == 0) return;

            var selectedLesson = (Lesson)listViewLessons.SelectedItems[0].Tag;
            try
            {
                if (lessonService.MoveLessonUp(selectedLesson.LessonId))
                {
                    LoadLessons();
                    statusLabel.Text = "Di chuyển bài học thành công!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi di chuyển bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (listViewLessons.SelectedItems.Count == 0) return;

            var selectedLesson = (Lesson)listViewLessons.SelectedItems[0].Tag;
            try
            {
                if (lessonService.MoveLessonDown(selectedLesson.LessonId))
                {
                    LoadLessons();
                    statusLabel.Text = "Di chuyển bài học thành công!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi di chuyển bài học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadLessons();
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
