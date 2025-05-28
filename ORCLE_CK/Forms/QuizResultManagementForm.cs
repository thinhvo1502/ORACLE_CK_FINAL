using ORCLE_CK.Services;
using ORCLE_CK.Models;
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
    public partial class QuizResultManagementForm : Form
    {
        private readonly QuizService quizService;
        private readonly int quizId;
        private ListView listViewResults;
        private Button btnRefresh, btnViewDetail;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public QuizResultManagementForm(int quizId)
        {
            this.quizId = quizId;
            quizService = new QuizService();
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            try
            {
                statusLabel.Text = "Đang tải dữ liệu...";
                listViewResults.Items.Clear();
                //MessageBox.Show("trước");

                var results = quizService.GetQuizResults(quizId);

                //MessageBox.Show("sau");
                foreach (var result in results)
                {
                    var item = new ListViewItem(result.StudentName);
                    item.SubItems.Add(result.Score.ToString("F2"));
                    item.SubItems.Add(result.TotalScore.ToString());
                    item.SubItems.Add($"{result.Percentage:F2}%");
                    item.SubItems.Add($"{result.TimeTaken} phút");
                    item.SubItems.Add(result.TakenAt.ToString("dd/MM/yyyy HH:mm"));
                    item.SubItems.Add(GetStatusText(result));
                    item.Tag = result;

                    // Set color based on score
                    if (result.Percentage >= 70)
                        item.ForeColor = Color.Green;
                    else if (result.Percentage >= 50)
                        item.ForeColor = Color.Orange;
                    else
                        item.ForeColor = Color.Red;

                    listViewResults.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {results.Count} kết quả";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading quiz results: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải kết quả quiz: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private string GetStatusText(QuizResult result)
        {
            if (result.TimeTaken > result.TimeLimit)
                return "Quá thời gian";
            if (result.Percentage >= 70)
                return "Đạt";
            if (result.Percentage >= 50)
                return "Trung bình";
            return "Không đạt";
        }

        private void UpdateButtonStates()
        {
            btnViewDetail.Enabled = listViewResults.SelectedItems.Count > 0;
        }

        private void BtnViewDetail_Click(object sender, EventArgs e)
        {
            if (listViewResults.SelectedItems.Count == 0) return;

            var selectedResult = (QuizResult)listViewResults.SelectedItems[0].Tag;
            using var detailForm = new QuizResultDetailForm(selectedResult);
            detailForm.ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadResults();
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            BtnViewDetail_Click(sender, e);
        }
    }
}
