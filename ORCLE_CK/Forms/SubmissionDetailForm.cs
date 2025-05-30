﻿using ORCLE_CK.Models;
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
    public partial class SubmissionDetailForm : Form
    {
        private readonly Submission submission;

        private Label lblStudentInfo;
        private TextBox txtFileUrl;
        private Label lblGradeInfo;
        private TextBox txtFeedback;
        private Button btnClose;

        public SubmissionDetailForm(Submission submission)
        {
            this.submission = submission ?? throw new ArgumentNullException(nameof(submission));
            InitializeComponent();
            LoadSubmissionData();
        }

        private void LoadSubmissionData()
        {
            lblStudentInfo.Text = $"Học viên: {submission.StudentName}\n" +
                                 $"Bài tập: {submission.AssignmentTitle}\n" +
                                 $"Khóa học: {submission.CourseName}\n" +
                                 $"Thời gian nộp: {submission.SubmittedAt:dd/MM/yyyy HH:mm}";

            txtFileUrl.Text = submission.FileUrl ?? "Không có file đính kèm";

            if (submission.Grade.HasValue)
            {
                lblGradeInfo.Text = $"Điểm: {submission.Grade:F2}/{submission.MaxScore} - Trạng thái: {submission.Status}";
                lblGradeInfo.ForeColor = submission.Grade >= submission.MaxScore * 0.7m ? Color.Green : Color.Red;
            }
            else
            {
                lblGradeInfo.Text = "Chưa được chấm điểm";
                lblGradeInfo.ForeColor = Color.Orange;
            }

            txtFeedback.Text = submission.Feedback ?? "Chưa có phản hồi";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
