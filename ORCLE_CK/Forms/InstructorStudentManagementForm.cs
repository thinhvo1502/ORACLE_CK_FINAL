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
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace ORCLE_CK.Forms
{
    public partial class InstructorStudentManagementForm : Form
    {
        private readonly User currentUser;
        private readonly UserService userService;
        private readonly CourseService courseService;

        private ListView listViewStudents;
        private ComboBox cmbCourseFilter;
        private TextBox txtSearch;
        private Button btnRefresh, btnViewProgress, btnSendMessage;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public InstructorStudentManagementForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            userService = new UserService();
            courseService = new CourseService();
            InitializeComponent();
            LoadCourses();
            LoadStudents();
        }
        private void LoadCourses()
        {
            try
            {
                var courses = courseService.GetCoursesByInstructor(currentUser.UserId);

                cmbCourseFilter.Items.Clear();
                cmbCourseFilter.Items.Add("Tất cả khóa học");

                foreach (var course in courses)
                {
                    cmbCourseFilter.Items.Add(course.Title);
                }

                cmbCourseFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading courses: {ex.Message}", ex);
            }
        }

        private void LoadStudents()
        {
            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                listViewStudents.Items.Clear();

                string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        WITH StudentGrades AS (
                            -- Get quiz grades
                            SELECT 
                                e.user_id,
                                e.course_id,
                                AVG(qr.score) as quiz_avg
                            FROM Enrollments e
                            LEFT JOIN Quiz_Results qr ON e.user_id = qr.user_id
                            LEFT JOIN Quizzes q ON qr.quiz_id = q.quiz_id AND q.course_id = e.course_id
                            WHERE e.course_id IN (
                                SELECT course_id FROM Courses WHERE instructor_id = :instructor_id
                            )
                            GROUP BY e.user_id, e.course_id
                        ),
                        AssignmentGrades AS (
                            -- Get assignment grades
                            SELECT 
                                e.user_id,
                                e.course_id,
                                AVG(s.grade) as assignment_avg
                            FROM Enrollments e
                            LEFT JOIN Submissions s ON e.user_id = s.user_id
                            LEFT JOIN Assignments a ON s.assignment_id = a.assignment_id AND a.course_id = e.course_id
                            WHERE e.course_id IN (
                                SELECT course_id FROM Courses WHERE instructor_id = :instructor_id
                            )
                            GROUP BY e.user_id, e.course_id
                        )
                        SELECT 
                            u.full_name,
                            u.email,
                            c.title as course_title,
                            e.enrolled_at,
                            e.progress,
                            ROUND(
        COALESCE(
            (sg.quiz_avg + ag.assignment_avg) / 2.0,
            sg.quiz_avg,
            ag.assignment_avg,
            0
        ), 2
    ) as average_grade

                        FROM Enrollments e
                        JOIN Users u ON e.user_id = u.user_id
                        JOIN Courses c ON e.course_id = c.course_id
                        LEFT JOIN StudentGrades sg ON e.user_id = sg.user_id AND e.course_id = sg.course_id
                        LEFT JOIN AssignmentGrades ag ON e.user_id = ag.user_id AND e.course_id = ag.course_id
                        WHERE c.instructor_id = :instructor_id
                        AND e.status = 'active'
                        ORDER BY u.full_name, c.title";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(":instructor_id", OracleDbType.Int32).Value = currentUser.UserId;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["full_name"].ToString());
                                item.SubItems.Add(reader["email"].ToString());
                                item.SubItems.Add(reader["course_title"].ToString());
                                item.SubItems.Add(((DateTime)reader["enrolled_at"]).ToString("dd/MM/yyyy"));
                                item.SubItems.Add($"{reader["progress"]}%");

                                //item.SubItems.Add("");

                                object avgObj = reader["average_grade"];

                                if (avgObj == DBNull.Value)
                                {
                                    item.SubItems.Add("N/A");
                                }
                                else
                                {
                                    // Chuyển sang decimal hoặc double đều được, tùy độ chính xác bạn muốn
                                    double avg = Convert.ToDouble(avgObj);
                                    item.SubItems.Add(Math.Round(avg, 2).ToString()); // Làm tròn đến 2 chữ số
                                }

                                listViewStudents.Items.Add(item);
                            }
                        }
                    }
                }

                if (listViewStudents.Items.Count == 0)
                {
                var item = new ListViewItem("Chưa có dữ liệu học viên");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                listViewStudents.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {listViewStudents.Items.Count} học viên";
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading students: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách học viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = listViewStudents.SelectedItems.Count > 0;
            btnViewProgress.Enabled = hasSelection;
            btnSendMessage.Enabled = hasSelection;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchStudents();
        }

        private void SearchStudents()
        {
            // TODO: Implement search functionality
        }

        private void CmbCourseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void BtnViewProgress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xem tiến độ đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSendMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng gửi tin nhắn đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
    }
}
