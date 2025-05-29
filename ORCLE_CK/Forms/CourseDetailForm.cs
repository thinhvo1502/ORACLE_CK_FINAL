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
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using ORCLE_CK.Data;

namespace ORCLE_CK.Forms
{
    public partial class CourseDetailForm : Form
    {
        private readonly Course course;
        private readonly User currentUser;
        private readonly LessonService lessonService;

        private TabControl tabControl;
        private TabPage tabInfo, tabLessons, tabStudents, tabAssignments;
        private ListView lessonsListView, studentsListView, assignmentsListView;
        private Button btnEditCourse, btnAddLesson, btnAddAssignment;

        public CourseDetailForm(Course course, User user)
        {
            this.course = course ?? throw new ArgumentNullException(nameof(course));
            this.currentUser = user ?? throw new ArgumentNullException(nameof(user));
            lessonService = new LessonService();
            InitializeComponent();
            LoadCourseData();
        }
        private void SetupInfoTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            // Course Title
            var lblTitle = new Label
            {
                Text = course.Title,
                Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(0, 0),
                Size = new Size(800, 30)
            };

            // Course Info
            var lblInfo = new Label
            {
                Text = $"Giảng viên: {course.InstructorName}\n" +
                       $"Ngày tạo: {course.CreatedAt:dd/MM/yyyy}\n" +
                       $"Cập nhật: {course.UpdatedAt?.ToString("dd/MM/yyyy") ?? "Chưa cập nhật"}\n" +
                       $"Số học viên: {course.EnrollmentCount}\n" +
                       $"Trạng thái: {(course.IsActive ? "Hoạt động" : "Vô hiệu")}",
                Font = new Font("Microsoft Sans Serif", 10F),
                Location = new Point(0, 50),
                Size = new Size(400, 120)
            };

            // Description
            var lblDescTitle = new Label
            {
                Text = "Mô tả khóa học:",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Location = new Point(0, 180),
                Size = new Size(200, 25)
            };

            var txtDescription = new TextBox
            {
                Text = course.Description ?? "Chưa có mô tả",
                Location = new Point(0, 210),
                Size = new Size(800, 200),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Microsoft Sans Serif", 10F)
            };

            // Edit Button
            this.btnEditCourse.Text = "Chỉnh sửa khóa học";
            this.btnEditCourse.Location = new Point(0, 430);
            this.btnEditCourse.Size = new Size(150, 35);
            this.btnEditCourse.BackColor = Color.Orange;
            this.btnEditCourse.ForeColor = Color.White;
            this.btnEditCourse.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnEditCourse.Click += BtnEditCourse_Click;

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblInfo);
            panel.Controls.Add(lblDescTitle);
            panel.Controls.Add(txtDescription);
            panel.Controls.Add(this.btnEditCourse);

            this.tabInfo.Controls.Add(panel);
        }

        private void SetupLessonsTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            // Add Lesson Button
            this.btnAddLesson.Text = "Thêm bài học";
            this.btnAddLesson.Location = new Point(0, 0);
            this.btnAddLesson.Size = new Size(120, 30);
            this.btnAddLesson.BackColor = Color.Green;
            this.btnAddLesson.ForeColor = Color.White;
            this.btnAddLesson.Click += BtnAddLesson_Click;

            // Lessons ListView
            this.lessonsListView.Location = new Point(0, 40);
            this.lessonsListView.Size = new Size(920, 500);
            this.lessonsListView.View = View.Details;
            this.lessonsListView.FullRowSelect = true;
            this.lessonsListView.GridLines = true;
            this.lessonsListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.lessonsListView.Columns.Add("Thứ tự", 80);
            this.lessonsListView.Columns.Add("Tiêu đề", 300);
            this.lessonsListView.Columns.Add("Thời lượng", 100);
            this.lessonsListView.Columns.Add("Ngày tạo", 120);
            this.lessonsListView.Columns.Add("Trạng thái", 80);

            this.lessonsListView.DoubleClick += LessonsListView_DoubleClick;

            panel.Controls.Add(this.btnAddLesson);
            panel.Controls.Add(this.lessonsListView);

            this.tabLessons.Controls.Add(panel);
        }

        private void SetupStudentsTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            // Students ListView
            this.studentsListView.Location = new Point(0, 0);
            this.studentsListView.Size = new Size(920, 540);
            this.studentsListView.View = View.Details;
            this.studentsListView.FullRowSelect = true;
            this.studentsListView.GridLines = true;
            this.studentsListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.studentsListView.Columns.Add("Họ tên", 200);
            this.studentsListView.Columns.Add("Email", 200);
            this.studentsListView.Columns.Add("Ngày đăng ký", 120);
            this.studentsListView.Columns.Add("Tiến độ", 100);
            this.studentsListView.Columns.Add("Trạng thái", 100);

            panel.Controls.Add(this.studentsListView);

            this.tabStudents.Controls.Add(panel);
        }

        private void SetupAssignmentsTab()
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            // Add Assignment Button
            this.btnAddAssignment.Text = "Thêm bài tập";
            this.btnAddAssignment.Location = new Point(0, 0);
            this.btnAddAssignment.Size = new Size(120, 30);
            this.btnAddAssignment.BackColor = Color.Blue;
            this.btnAddAssignment.ForeColor = Color.White;
            this.btnAddAssignment.Click += BtnAddAssignment_Click;

            // Assignments ListView
            this.assignmentsListView.Location = new Point(0, 40);
            this.assignmentsListView.Size = new Size(920, 500);
            this.assignmentsListView.View = View.Details;
            this.assignmentsListView.FullRowSelect = true;
            this.assignmentsListView.GridLines = true;
            this.assignmentsListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.assignmentsListView.Columns.Add("Tiêu đề", 250);
            this.assignmentsListView.Columns.Add("Hạn nộp", 120);
            this.assignmentsListView.Columns.Add("Điểm tối đa", 100);
            this.assignmentsListView.Columns.Add("Số bài nộp", 100);
            this.assignmentsListView.Columns.Add("Trạng thái", 80);

            panel.Controls.Add(this.btnAddAssignment);
            panel.Controls.Add(this.assignmentsListView);

            this.tabAssignments.Controls.Add(panel);
        }

        private void LoadCourseData()
        {
            LoadLessons();
            LoadStudents();
            LoadAssignments();
        }

        private void LoadLessons()
        {
            try
            {
                lessonsListView.Items.Clear();
                var lessons = lessonService.GetLessonsByCourse(course.CourseId);

                foreach (var lesson in lessons)
                {
                    var item = new ListViewItem(lesson.OrderNumber.ToString());
                    item.SubItems.Add(lesson.Title);
                    item.SubItems.Add($"{lesson.Duration} phút");
                    item.SubItems.Add(lesson.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add(lesson.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = lesson;

                    if (!lesson.IsActive)
                        item.ForeColor = Color.Gray;

                    lessonsListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading lessons: {ex.Message}", ex);
            }
        }

        private void LoadStudents()
        {
            try
            {
                studentsListView.Items.Clear();
                string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    DatabaseConnection.setUp(connection);

                    string query = @"
                        SELECT 
                            u.full_name,
                            u.email,
                            e.enrolled_at,
                            e.progress,
                            e.status
                        FROM Enrollments e
                        JOIN Users u ON e.user_id = u.user_id
                        WHERE e.course_id = :courseId
                        ORDER BY u.full_name";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = course.CourseId;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["full_name"].ToString());
                                item.SubItems.Add(reader["email"].ToString());
                                item.SubItems.Add(((DateTime)reader["enrolled_at"]).ToString("dd/MM/yyyy"));
                                item.SubItems.Add($"{reader["progress"]}%");
                                item.SubItems.Add(reader["status"].ToString() == "active" ? "Đang học" : "Đã kết thúc");
                                studentsListView.Items.Add(item);
                            }
                        }
                    }
                }

                if (studentsListView.Items.Count == 0)
                {
                    var item = new ListViewItem("Chưa có học viên đăng ký");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    studentsListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading students: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAssignments()
        {
            try
            {
                assignmentsListView.Items.Clear();
                var assignments = new AssignmentService().GetAssignmentsByCourse(course.CourseId);

                foreach (var assignment in assignments)
                {
                    var item = new ListViewItem(assignment.Title);
                    item.SubItems.Add(assignment.DueDate?.ToString("dd/MM/yyyy") ?? "Không có hạn");
                    item.SubItems.Add(assignment.MaxScore.ToString());
                    item.SubItems.Add(assignment.SubmissionCount.ToString());
                    item.SubItems.Add(assignment.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = assignment;

                    if (!assignment.IsActive)
                        item.ForeColor = Color.Gray;

                    assignmentsListView.Items.Add(item);
                }

                if (assignmentsListView.Items.Count == 0)
                {
                    var item = new ListViewItem("Chưa có bài tập");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    assignmentsListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading assignments: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách bài tập: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditCourse_Click(object sender, EventArgs e)
        {
            using var editForm = new EditCourseForm(course);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Cập nhật khóa học thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BtnAddLesson_Click(object sender, EventArgs e)
        {
            using var addForm = new AddLessonForm(course.CourseId);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadLessons();
            }
        }

        private void BtnAddAssignment_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LessonsListView_DoubleClick(object sender, EventArgs e)
        {
            if (lessonsListView.SelectedItems.Count == 0) return;

            var selectedLesson = (Lesson)lessonsListView.SelectedItems[0].Tag;
            using var editForm = new EditLessonForm(selectedLesson);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadLessons();
            }
        }
    }
}
