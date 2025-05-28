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
    public partial class EditLessonForm : Form
    {
        private readonly LessonService lessonService;
        private readonly Lesson lessonToEdit;

        private TextBox txtTitle;
        private TextBox txtContent;
        private TextBox txtVideoUrl;
        private NumericUpDown numDuration;
        private NumericUpDown numOrderNumber;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;

        public EditLessonForm(Lesson lesson)
        {
            lessonToEdit = lesson ?? throw new ArgumentNullException(nameof(lesson));
            lessonService = new LessonService();
            InitializeComponent();
            LoadLessonData();
        }
        private void LoadLessonData()
        {
            txtTitle.Text = lessonToEdit.Title;
            txtContent.Text = lessonToEdit.Content ?? "";
            txtVideoUrl.Text = lessonToEdit.VideoUrl ?? "";
            numDuration.Value = lessonToEdit.Duration;
            numOrderNumber.Value = lessonToEdit.OrderNumber;
            chkIsActive.Checked = lessonToEdit.IsActive;
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

                lessonToEdit.Title = txtTitle.Text.Trim();
                lessonToEdit.Content = txtContent.Text.Trim();
                lessonToEdit.VideoUrl = string.IsNullOrWhiteSpace(txtVideoUrl.Text) ? null : txtVideoUrl.Text.Trim();
                lessonToEdit.Duration = (int)numDuration.Value;
                lessonToEdit.OrderNumber = (int)numOrderNumber.Value;
                lessonToEdit.IsActive = chkIsActive.Checked;

                if (lessonService.UpdateLesson(lessonToEdit))
                {
                    MessageBox.Show("Cập nhật bài học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật bài học!", "Lỗi",
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
