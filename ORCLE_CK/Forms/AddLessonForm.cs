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
    public partial class AddLessonForm : Form
    {
        private readonly LessonService lessonService;
        private readonly int courseId;

        private TextBox txtTitle;
        private TextBox txtContent;
        private TextBox txtVideoUrl;
        private NumericUpDown numDuration;
        private NumericUpDown numOrderNumber;
        private Button btnSave;
        private Button btnCancel;

        public AddLessonForm(int courseId)
        {
            this.courseId = courseId;
            lessonService = new LessonService();
            // Set default order number
            //this.Load += (s, e) => {
            //    this.numOrderNumber.Value = lessonService.GetNextOrderNumber(courseId);
            //};
            InitializeComponent();
        }

        

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tiêu đề bài học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var lesson = new Lesson
                {
                    CourseId = courseId,
                    Title = txtTitle.Text.Trim(),
                    Content = txtContent.Text.Trim(),
                    VideoUrl = string.IsNullOrWhiteSpace(txtVideoUrl.Text) ? null : txtVideoUrl.Text.Trim(),
                    Duration = (int)numDuration.Value,
                    OrderNumber = (int)numOrderNumber.Value,
                    IsActive = true
                };

                if (lessonService.CreateLesson(lesson))
                {
                    MessageBox.Show("Thêm bài học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể thêm bài học!", "Lỗi",
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
