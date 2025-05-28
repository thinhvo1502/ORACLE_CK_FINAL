using ORCLE_CK.Models;
using ORCLE_CK.Services;
using System;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class AddQuizQuestionForm : Form
    {
        private readonly QuizService quizService;
        private readonly int quizId;

        private TextBox txtQuestion;
        private TextBox txtOptionA;
        private TextBox txtOptionB;
        private TextBox txtOptionC;
        private TextBox txtOptionD;
        private ComboBox cmbCorrectAnswer;
        private NumericUpDown numPoints;
        private Button btnSave;
        private Button btnCancel;

        public AddQuizQuestionForm(int quizId)
        {
            this.quizId = quizId;
            quizService = new QuizService();
            InitializeComponent();
            InitializeControls();
        }

        
        private void InitializeControls()
        {
            // Question
            var lblQuestion = new Label
            {
                Text = "Câu hỏi:",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblQuestion);

            txtQuestion = new TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(540, 60),
                Multiline = true
            };
            this.Controls.Add(txtQuestion);

            // Options
            var lblOptions = new Label
            {
                Text = "Các lựa chọn:",
                Location = new System.Drawing.Point(20, 120),
                AutoSize = true
            };
            this.Controls.Add(lblOptions);

            txtOptionA = new TextBox
            {
                Location = new System.Drawing.Point(20, 150),
                Size = new System.Drawing.Size(540, 30)
            };
            this.Controls.Add(txtOptionA);

            txtOptionB = new TextBox
            {
                Location = new System.Drawing.Point(20, 190),
                Size = new System.Drawing.Size(540, 30)
            };
            this.Controls.Add(txtOptionB);

            txtOptionC = new TextBox
            {
                Location = new System.Drawing.Point(20, 230),
                Size = new System.Drawing.Size(540, 30)
            };
            this.Controls.Add(txtOptionC);

            txtOptionD = new TextBox
            {
                Location = new System.Drawing.Point(20, 270),
                Size = new System.Drawing.Size(540, 30)
            };
            this.Controls.Add(txtOptionD);

            // Correct Answer
            var lblCorrectAnswer = new Label
            {
                Text = "Đáp án đúng:",
                Location = new System.Drawing.Point(20, 310),
                AutoSize = true
            };
            this.Controls.Add(lblCorrectAnswer);

            cmbCorrectAnswer = new ComboBox
            {
                Location = new System.Drawing.Point(20, 340),
                Size = new System.Drawing.Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCorrectAnswer.Items.AddRange(new string[] { "A", "B", "C", "D" });
            this.Controls.Add(cmbCorrectAnswer);

            // Points
            var lblPoints = new Label
            {
                Text = "Điểm:",
                Location = new System.Drawing.Point(20, 380),
                AutoSize = true
            };
            this.Controls.Add(lblPoints);

            numPoints = new NumericUpDown
            {
                Location = new System.Drawing.Point(20, 410),
                Size = new System.Drawing.Size(200, 30),
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };
            this.Controls.Add(numPoints);

            // Buttons
            btnSave = new Button
            {
                Text = "Lưu",
                Location = new System.Drawing.Point(360, 410),
                Size = new System.Drawing.Size(90, 30),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new System.Drawing.Point(470, 410),
                Size = new System.Drawing.Size(90, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                MessageBox.Show("Vui lòng nhập câu hỏi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOptionA.Text) || string.IsNullOrWhiteSpace(txtOptionB.Text) ||
                string.IsNullOrWhiteSpace(txtOptionC.Text) || string.IsNullOrWhiteSpace(txtOptionD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các lựa chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbCorrectAnswer.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đáp án đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var question = new QuizQuestion
            {
                QuizId = quizId,
                Question = txtQuestion.Text.Trim(),
                OptionA = txtOptionA.Text.Trim(),
                OptionB = txtOptionB.Text.Trim(),
                OptionC = txtOptionC.Text.Trim(),
                OptionD = txtOptionD.Text.Trim(),
                CorrectAnswer = cmbCorrectAnswer.SelectedItem.ToString(),
                Points = (int)numPoints.Value
            };

            try
            {
                quizService.AddQuizQuestion(question);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm câu hỏi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}