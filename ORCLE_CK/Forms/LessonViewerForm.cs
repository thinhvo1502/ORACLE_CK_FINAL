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
    public partial class LessonViewerForm : Form
    {
        private readonly Lesson lesson;
        private readonly User currentUser;
        private readonly EnrollmentService enrollmentService;

        private Panel contentPanel;
        private Label lblTitle, lblDuration;
        private TextBox txtContent;
        private TextBox txtVideoUrl;
        private Button btnMarkComplete, btnPrevious, btnNext;
        private CheckBox chkCompleted;

        public LessonViewerForm(Lesson lesson, User user)
        {
            this.lesson = lesson ?? throw new ArgumentNullException(nameof(lesson));
            this.currentUser = user ?? throw new ArgumentNullException(nameof(user));
            enrollmentService = new EnrollmentService();
            InitializeComponent();
            LoadLessonContent();
        }
        private void LoadLessonContent()
        {
            // TODO: Load lesson completion status
            // For now, just display the lesson content
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            // TODO: Navigate to previous lesson
            MessageBox.Show("Chức năng đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            // TODO: Navigate to next lesson
            MessageBox.Show("Chức năng đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnMarkComplete_Click(object sender, EventArgs e)
        {
            if (chkCompleted.Checked)
            {
                try
                {
                    // TODO: Mark lesson as completed and update progress
                    MessageBox.Show("Đã đánh dấu hoàn thành bài học!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error marking lesson complete: {ex.Message}", ex);
                    MessageBox.Show($"Lỗi cập nhật tiến độ: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng đánh dấu hoàn thành bài học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
