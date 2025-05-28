using ORCLE_CK.Models;
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
    public partial class CourseSelectionForm : Form
    {
        private readonly List<Course> courses;
        private ComboBox cmbCourses;
        private Button btnSelect;
        private Button btnCancel;

        public int SelectedCourseId { get; private set; }

        public CourseSelectionForm(List<Course> courses)
        {
            this.courses = courses ?? throw new ArgumentNullException(nameof(courses));
            InitializeComponent();
            LoadCourses();
        }
        private void LoadCourses()
        {
            cmbCourses.DataSource = courses;
            if (courses.Count > 0)
                cmbCourses.SelectedIndex = 0;
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (cmbCourses.SelectedValue != null)
            {
                SelectedCourseId = (int)cmbCourses.SelectedValue;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
