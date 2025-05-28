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
    public partial class EditCourseForm : Form
    {
        private readonly CourseService courseService;
        private readonly UserService userService;
        private readonly Course courseToEdit;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private ComboBox cmbInstructor;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;

        public EditCourseForm(Course course)
        {
            courseToEdit = course ?? throw new ArgumentNullException(nameof(course));
            courseService = new CourseService();
            userService = new UserService();
            InitializeComponent();
            LoadInstructors();
            LoadCourseData();
        }
        private void LoadInstructors()
        {
            try
            {
                var instructors = userService.GetInstructors();
                cmbInstructor.DataSource = instructors;
                if (instructors.Any())
                    cmbInstructor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách giảng viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCourseData()
        {
            txtTitle.Text = courseToEdit.Title;
            txtDescription.Text = courseToEdit.Description ?? "";
            chkIsActive.Checked = courseToEdit.IsActive;

            // Set instructor
            foreach (User instructor in cmbInstructor.Items)
            {
                if (instructor.UserId == courseToEdit.InstructorId)
                {
                    cmbInstructor.SelectedItem = instructor;
                    break;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tiêu đề khóa học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbInstructor.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn giảng viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                courseToEdit.Title = txtTitle.Text.Trim();
                courseToEdit.Description = txtDescription.Text.Trim();
                courseToEdit.InstructorId = (int)cmbInstructor.SelectedValue;
                courseToEdit.IsActive = chkIsActive.Checked;

                if (courseService.UpdateCourse(courseToEdit))
                {
                    MessageBox.Show("Cập nhật khóa học thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật khóa học!", "Lỗi",
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
