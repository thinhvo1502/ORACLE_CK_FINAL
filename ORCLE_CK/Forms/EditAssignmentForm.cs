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
    public partial class EditAssignmentForm : Form
    {
        private readonly AssignmentService assignmentService;
        private readonly Assignment assignmentToEdit;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private DateTimePicker dtpDueDate;
        private CheckBox chkHasDueDate;
        private NumericUpDown numMaxScore;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;

        public EditAssignmentForm(Assignment assignment)
        {
            assignmentToEdit = assignment ?? throw new ArgumentNullException(nameof(assignment));
            assignmentService = new AssignmentService();
            InitializeComponent();
            LoadAssignmentData();
        }

        

        private void LoadAssignmentData()
        {
            txtTitle.Text = assignmentToEdit.Title;
            txtDescription.Text = assignmentToEdit.Description ?? "";
            chkHasDueDate.Checked = assignmentToEdit.DueDate.HasValue;
            if (assignmentToEdit.DueDate.HasValue)
                dtpDueDate.Value = assignmentToEdit.DueDate.Value;
            numMaxScore.Value = assignmentToEdit.MaxScore;
            chkIsActive.Checked = assignmentToEdit.IsActive;

            dtpDueDate.Enabled = chkHasDueDate.Checked;
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

                assignmentToEdit.Title = txtTitle.Text.Trim();
                assignmentToEdit.Description = txtDescription.Text.Trim();
                assignmentToEdit.DueDate = chkHasDueDate.Checked ? dtpDueDate.Value : (DateTime?)null;
                assignmentToEdit.MaxScore = (int)numMaxScore.Value;
                assignmentToEdit.IsActive = chkIsActive.Checked;

                if (assignmentService.UpdateAssignment(assignmentToEdit))
                {
                    MessageBox.Show("Cập nhật bài tập thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật bài tập!", "Lỗi",
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
