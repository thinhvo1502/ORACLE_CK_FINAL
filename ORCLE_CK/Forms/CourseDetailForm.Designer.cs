using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CourseDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabInfo = new TabPage("Thông tin");
            this.tabLessons = new TabPage("Bài học");
            this.tabStudents = new TabPage("Học viên");
            this.tabAssignments = new TabPage("Bài tập");
            this.lessonsListView = new ListView();
            this.studentsListView = new ListView();
            this.assignmentsListView = new ListView();
            this.btnEditCourse = new Button();
            this.btnAddLesson = new Button();
            this.btnAddAssignment = new Button();

            this.SuspendLayout();

            // Form
            this.Text = $"Chi tiết khóa học: {course.Title}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // Tab Control
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Font = new Font("Microsoft Sans Serif", 10F);

            // Info Tab
            SetupInfoTab();

            // Lessons Tab
            SetupLessonsTab();

            // Students Tab
            SetupStudentsTab();

            // Assignments Tab
            SetupAssignmentsTab();

            // Add tabs
            this.tabControl.TabPages.Add(this.tabInfo);
            this.tabControl.TabPages.Add(this.tabLessons);
            this.tabControl.TabPages.Add(this.tabStudents);
            this.tabControl.TabPages.Add(this.tabAssignments);

            this.Controls.Add(this.tabControl);

            this.ResumeLayout(false);
        }

        #endregion
    }
}