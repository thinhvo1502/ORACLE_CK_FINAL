using ORCLE_CK.Models;
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
    public partial class AddQuizForm : Form
    {
        private readonly QuizService quizService;
        private readonly int courseId;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private NumericUpDown numTimeLimit;
        private CheckBox chkHasTimeLimit;
        private Button btnSave;
        private Button btnCancel;

        public AddQuizForm(int courseId)
        {
            this.courseId = courseId;
            quizService = new QuizService();
            InitializeComponent();
        }

        

        private void ChkHasTimeLimit_CheckedChanged(object sender, EventArgs e)
        {
            numTimeLimit.Enabled = chkHasTimeLimit.Checked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tiêu đề quiz!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var quiz = new Quiz
                {
                    CourseId = courseId,
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    TimeLimit = chkHasTimeLimit.Checked ? (int)numTimeLimit.Value : (int?)null,
                    TotalScore = 0, // Sẽ được cập nhật khi thêm câu hỏi
                    IsActive = true
                };

                if (quizService.CreateQuiz(quiz))
                {
                    MessageBox.Show("Thêm quiz thành công! Bạn có thể thêm câu hỏi vào quiz này.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể thêm quiz!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
