using ORCLE_CK.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class QuizResultDetailForm : Form
    {
        private readonly QuizResult result;
        private Label lblStudentName, lblQuizTitle, lblScore, lblTotalScore;
        private Label lblPercentage, lblTimeTaken, lblTakenAt, lblStatus;
        private Button btnClose;

        public QuizResultDetailForm(QuizResult result)
        {
            this.result = result;
            InitializeComponent();
            LoadResultDetails();
        }

        private void LoadResultDetails()
        {
            lblStudentName.Text = $"Học viên: {result.StudentName}";
            lblQuizTitle.Text = $"Bài quiz: {result.QuizTitle}";
            lblScore.Text = $"Điểm: {result.Score:F2}";
            lblTotalScore.Text = $"Điểm tối đa: {result.TotalScore}";
            lblPercentage.Text = $"Phần trăm: {result.Percentage:F2}%";
            lblTimeTaken.Text = $"Thời gian làm: {result.TimeTaken} phút";
            lblTakenAt.Text = $"Ngày làm: {result.TakenAt:dd/MM/yyyy HH:mm}";
            lblStatus.Text = $"Trạng thái: {GetStatusText(result)}";

            // Set color based on score
            if (result.Percentage >= 70)
                lblStatus.ForeColor = Color.Green;
            else if (result.Percentage >= 50)
                lblStatus.ForeColor = Color.Orange;
            else
                lblStatus.ForeColor = Color.Red;
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
} 