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
    public partial class AddAssignmentForm : Form
    {
        private readonly AssignmentService assignmentService;
        private readonly int courseId;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private DateTimePicker dtpDueDate;
        private CheckBox chkHasDueDate;
        private NumericUpDown numMaxScore;
        private Button btnSave;
        private Button btnCancel;

        public AddAssignmentForm(int courseId)
        {
            this.courseId = courseId;
            assignmentService = new AssignmentService();
            InitializeComponent();
        }

        

        private void ChkHasDueDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpDueDate.Enabled = chkHasDueDate.Checked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tiêu đề bài tập!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var assignment = new Assignment
                {
                    CourseId = courseId,
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    DueDate = chkHasDueDate.Checked ? dtpDueDate.Value : (DateTime?)null,
                    MaxScore = (int)numMaxScore.Value,
                    IsActive = true
                };

                if (assignmentService.CreateAssignment(assignment))
                {
                    MessageBox.Show("Thêm bài tập thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể thêm bài tập!", "Lỗi",
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
