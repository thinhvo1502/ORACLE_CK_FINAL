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
    public partial class StudentProgressForm : Form
    {
        private readonly Enrollment enrollment;
        private readonly EnrollmentService enrollmentService;
        private readonly QuizService quizService;

        private Label lblStudentName, lblCourseTitle, lblEnrollmentDate;
        private Label lblProgress, lblCompletedLessons, lblAverageGrade;
        private ProgressBar progressBar;
        private Button btnClose;

        public StudentProgressForm(Enrollment enrollment)
        {
            this.enrollment = enrollment ?? throw new ArgumentNullException(nameof(enrollment));
            enrollmentService = new EnrollmentService();
            quizService = new QuizService();
            InitializeComponent();
            LoadProgressDetails();
        }

        private void LoadProgressDetails()
        {
            try
            {
                lblStudentName.Text = $"Học viên: {enrollment.StudentName}";
                lblCourseTitle.Text = $"Khóa học: {enrollment.CourseTitle}";
                lblEnrollmentDate.Text = $"Ngày đăng ký: {enrollment.EnrolledAt:dd/MM/yyyy}";
                lblProgress.Text = $"Tiến độ: {enrollment.Progress:F1}%";
                //lblCompletedLessons.Text = $"Bài học đã hoàn thành: {enrollment.CompletedLessons}/{enrollment.TotalLessons}";
                //lblAverageGrade.Text = $"Điểm trung bình: {enrollment.AverageGrade:F1}";

                progressBar.Value = (int)enrollment.Progress;

                // Set color based on progress
                if (enrollment.Progress >= 70)
                {
                    progressBar.ForeColor = Color.Green;
                    lblProgress.ForeColor = Color.Green;
                }
                else if (enrollment.Progress >= 50)
                {
                    progressBar.ForeColor = Color.Orange;
                    lblProgress.ForeColor = Color.Orange;
                }
                else
                {
                    progressBar.ForeColor = Color.Red;
                    lblProgress.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading progress details: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải thông tin tiến độ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
