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
    public partial class QuizQuestionManagementForm : Form
    {
        private readonly QuizService quizService;
        private readonly int quizId;
        private ListView listViewQuestions;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh;

        public QuizQuestionManagementForm(int quizId)
        {
            this.quizId = quizId;
            quizService = new QuizService();
            InitializeComponent();
            LoadQuestions();
        }

        

        private void LoadQuestions()
        {
            listViewQuestions.Items.Clear();
            var questions = quizService.GetQuizQuestions(quizId);

            foreach (var question in questions)
            {
                var item = new ListViewItem(question.OrderNumber.ToString());
                item.SubItems.Add(question.Question.Length > 50 ?
                    question.Question.Substring(0, 50) + "..." : question.Question);
                item.SubItems.Add(question.OptionA ?? "");
                item.SubItems.Add(question.OptionB ?? "");
                item.SubItems.Add(question.OptionC ?? "");
                item.SubItems.Add(question.OptionD ?? "");
                item.SubItems.Add(question.CorrectAnswer);
                item.SubItems.Add(question.Points.ToString());
                item.Tag = question;

                listViewQuestions.Items.Add(item);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using var addForm = new AddQuizQuestionForm(quizId);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestions();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listViewQuestions.SelectedItems.Count == 0) return;

            var selectedQuestion = (QuizQuestion)listViewQuestions.SelectedItems[0].Tag;
            using var editForm = new EditQuizQuestionForm(selectedQuestion);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestions();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listViewQuestions.SelectedItems.Count == 0) return;

            var selectedQuestion = (QuizQuestion)listViewQuestions.SelectedItems[0].Tag;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa câu hỏi này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Implementation for delete question
                LoadQuestions();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadQuestions();
        }
    }
}
