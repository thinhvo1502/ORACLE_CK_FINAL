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
using System.Windows.Forms.Design;

namespace ORCLE_CK.Forms
{
    public partial class QuizManagementForm : Form
    {
        private readonly QuizService quizService;
        private readonly int courseId;
        private ListView listViewQuizzes;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh, btnViewResults;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public QuizManagementForm(int courseId)
        {
            this.courseId = courseId;
            quizService = new QuizService();
            InitializeComponent();
            LoadQuizzes();
        }

        

        private void LoadQuizzes()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewQuizzes.Items.Clear();

                var quizzes = quizService.GetQuizzesByCourse(courseId);

                foreach (var quiz in quizzes)
                {
                    var item = new ListViewItem(quiz.QuizId.ToString());
                    item.SubItems.Add(quiz.Title);
                    item.SubItems.Add(quiz.Description != null ?
                        (quiz.Description.Length > 50 ? quiz.Description.Substring(0, 50) + "..." : quiz.Description) : "");
                    item.SubItems.Add(quiz.TimeLimit?.ToString() ?? "Không giới hạn");
                    item.SubItems.Add(quiz.QuestionCount.ToString());
                    item.SubItems.Add(quiz.TotalScore.ToString());
                    item.SubItems.Add(quiz.AttemptCount.ToString());
                    item.SubItems.Add(quiz.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = quiz;

                    if (!quiz.IsActive)
                        item.ForeColor = Color.Gray;

                    listViewQuizzes.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {quizzes.Count} quiz";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading quizzes: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách quiz: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewQuizzes.SelectedItems.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnViewResults.Enabled = hasSelection;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using var addForm = new AddQuizForm(courseId);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadQuizzes();
                statusLabel.Text = "Thêm quiz thành công!";
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewQuizzes.SelectedItems.Count == 0) return;

            var selectedQuiz = (Quiz)listViewQuizzes.SelectedItems[0].Tag;
            using var editForm = new EditQuizForm(selectedQuiz);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadQuizzes();
                statusLabel.Text = "Cập nhật quiz thành công!";
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewQuizzes.SelectedItems.Count == 0) return;

            var selectedQuiz = (Quiz)listViewQuizzes.SelectedItems[0].Tag;

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa quiz '{selectedQuiz.Title}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (quizService.DeleteQuiz(selectedQuiz.QuizId))
                    {
                        LoadQuizzes();
                        statusLabel.Text = "Xóa quiz thành công!";
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa quiz!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error deleting quiz: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi xóa quiz: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnViewResults_Click(object sender, EventArgs e)
        {
            if (listViewQuizzes.SelectedItems.Count == 0) return;

            var selectedQuiz = (Quiz)listViewQuizzes.SelectedItems[0].Tag;
            using var resultForm = new QuizResultManagementForm(selectedQuiz.QuizId);
            resultForm.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadQuizzes();
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
