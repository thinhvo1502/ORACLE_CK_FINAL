using ORCLE_CK.Models;
using ORCLE_CK.Services;
using System;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class EditQuizQuestionForm : Form
    {
        private readonly QuizService quizService;
        private readonly QuizQuestion questionToEdit;

        private TextBox txtQuestion;
        private TextBox txtOptionA;
        private TextBox txtOptionB;
        private TextBox txtOptionC;
        private TextBox txtOptionD;
        private ComboBox cmbCorrectAnswer;
        private NumericUpDown numPoints;
        private NumericUpDown numOrder;
        private Button btnSave;
        private Button btnCancel;

        public EditQuizQuestionForm(QuizQuestion question)
        {
            questionToEdit = question ?? throw new ArgumentNullException(nameof(question));
            quizService = new QuizService();
            InitializeComponent();
            LoadQuestionData();
        }

        

        private void LoadQuestionData()
        {
            txtQuestion.Text = questionToEdit.Question;
            txtOptionA.Text = questionToEdit.OptionA ?? "";
            txtOptionB.Text = questionToEdit.OptionB ?? "";
            txtOptionC.Text = questionToEdit.OptionC ?? "";
            txtOptionD.Text = questionToEdit.OptionD ?? "";
            cmbCorrectAnswer.SelectedItem = questionToEdit.CorrectAnswer;
            numPoints.Value = questionToEdit.Points;
            numOrder.Value = questionToEdit.OrderNumber;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtQuestion.Text))
                {
                    MessageBox.Show("Vui lòng nhập câu hỏi!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbCorrectAnswer.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn đáp án đúng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                questionToEdit.Question = txtQuestion.Text.Trim();
                questionToEdit.OptionA = string.IsNullOrWhiteSpace(txtOptionA.Text) ? null : txtOptionA.Text.Trim();
                questionToEdit.OptionB = string.IsNullOrWhiteSpace(txtOptionB.Text) ? null : txtOptionB.Text.Trim();
                questionToEdit.OptionC = string.IsNullOrWhiteSpace(txtOptionC.Text) ? null : txtOptionC.Text.Trim();
                questionToEdit.OptionD = string.IsNullOrWhiteSpace(txtOptionD.Text) ? null : txtOptionD.Text.Trim();
                questionToEdit.CorrectAnswer = cmbCorrectAnswer.SelectedItem.ToString();
                questionToEdit.Points = (int)numPoints.Value;
                questionToEdit.OrderNumber = (int)numOrder.Value;

                // TODO: Gọi QuizService để lưu lại dữ liệu

                MessageBox.Show("Cập nhật câu hỏi thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
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
