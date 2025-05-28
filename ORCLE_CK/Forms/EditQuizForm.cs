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
    public partial class EditQuizForm : Form
    {
        private readonly QuizService quizService;
        private readonly Quiz quizToEdit;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private NumericUpDown numTimeLimit;
        private CheckBox chkHasTimeLimit;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;
        private Button btnManageQuestions;

        public EditQuizForm(Quiz quiz)
        {
            quizToEdit = quiz ?? throw new ArgumentNullException(nameof(quiz));
            quizService = new QuizService();
            InitializeComponent();
            LoadQuizData();
        }

        

        private void LoadQuizData()
        {
            txtTitle.Text = quizToEdit.Title;
            txtDescription.Text = quizToEdit.Description ?? "";
            chkHasTimeLimit.Checked = quizToEdit.TimeLimit.HasValue;
            if (quizToEdit.TimeLimit.HasValue)
                numTimeLimit.Value = quizToEdit.TimeLimit.Value;
            chkIsActive.Checked = quizToEdit.IsActive;

            numTimeLimit.Enabled = chkHasTimeLimit.Checked;
        }

        private void ChkHasTimeLimit_CheckedChanged(object sender, EventArgs e)
        {
            numTimeLimit.Enabled = chkHasTimeLimit.Checked;
        }

        private void BtnManageQuestions_Click(object sender, EventArgs e)
        {
            using var questionForm = new QuizQuestionManagementForm(quizToEdit.QuizId);
            questionForm.ShowDialog();

            // Cập nhật tổng điểm sau khi quản lý câu hỏi
            quizService.UpdateQuizTotalScore(quizToEdit.QuizId);
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

                quizToEdit.Title = txtTitle.Text.Trim();
                quizToEdit.Description = txtDescription.Text.Trim();
                quizToEdit.TimeLimit = chkHasTimeLimit.Checked ? (int)numTimeLimit.Value : (int?)null;
                quizToEdit.IsActive = chkIsActive.Checked;

                if (quizService.UpdateQuiz(quizToEdit))
                {
                    MessageBox.Show("Cập nhật quiz thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật quiz!", "Lỗi",
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
