using System;
using System.Drawing;
using System.Windows.Forms;
using ORCLE_CK.Constants;
using ORCLE_CK.Forms;
using ORCLE_CK.Models;
using ORCLE_CK.Services;
using ORCLE_CK.Utils;

namespace ORCLE_CK.Forms
{
    public partial class MainForm : Form
    {
        private readonly User currentUser;
        private readonly UserService userService;
        private readonly CourseService courseService;

        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel timeLabel;
        private Panel mainPanel;
        private Timer statusTimer;

        public MainForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            userService = new UserService();
            courseService = new CourseService();
            InitializeComponent();
            SetupUserInterface();
            InitializeStatusTimer();
        }


        
        private void SetupUserInterface()
        {
            menuStrip.Items.Clear();

            // System Menu
            var systemMenu = new ToolStripMenuItem("Hệ thống");
            systemMenu.DropDownItems.Add("Thông tin tài khoản", null, AccountInfoMenuItem_Click);
            systemMenu.DropDownItems.Add("Đổi mật khẩu", null, ChangePasswordMenuItem_Click);
            systemMenu.DropDownItems.Add(new ToolStripSeparator());
            systemMenu.DropDownItems.Add("Đăng xuất", null, LogoutMenuItem_Click);
            systemMenu.DropDownItems.Add("Thoát", null, ExitMenuItem_Click);

            // Admin menus
            if (currentUser.Role.ToLower() == "admin")
            {
                var userMenu = new ToolStripMenuItem("Quản lý người dùng");
                userMenu.DropDownItems.Add("Danh sách người dùng", null, UserListMenuItem_Click);
                userMenu.DropDownItems.Add("Thêm người dùng", null, AddUserMenuItem_Click);
                userMenu.DropDownItems.Add("Báo cáo người dùng", null, UserReportMenuItem_Click);
                menuStrip.Items.Add(userMenu);

                var courseMenu = new ToolStripMenuItem("Quản lý khóa học");
                courseMenu.DropDownItems.Add("Danh sách khóa học", null, CourseListMenuItem_Click);
                courseMenu.DropDownItems.Add("Thêm khóa học", null, AddCourseMenuItem_Click);
                courseMenu.DropDownItems.Add("Báo cáo khóa học", null, CourseReportMenuItem_Click);
                menuStrip.Items.Add(courseMenu);

                var reportMenu = new ToolStripMenuItem("Báo cáo");
                reportMenu.DropDownItems.Add("Thống kê tổng quan", null, OverviewReportMenuItem_Click);
                reportMenu.DropDownItems.Add("Báo cáo học viên", null, StudentReportMenuItem_Click);
                reportMenu.DropDownItems.Add("Báo cáo giảng viên", null, InstructorReportMenuItem_Click);
                menuStrip.Items.Add(reportMenu);
            }

            // Instructor menus
            if (currentUser.Role.ToLower() == "instructor" || currentUser.Role.ToLower() == "admin")
            {
                var teachingMenu = new ToolStripMenuItem("Giảng dạy");
                teachingMenu.DropDownItems.Add("Bảng điều khiển", null, InstructorDashboardMenuItem_Click);
                teachingMenu.DropDownItems.Add("Khóa học của tôi", null, MyCoursesMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý bài học", null, LessonManagementMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý học viên", null, StudentManagementMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý bài tập", null, AssignmentManagementMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý quiz", null, QuizManagementMenuItem_Click);
                menuStrip.Items.Add(teachingMenu);
            }

            // Student menus
            if (currentUser.Role.ToLower() == "student")
            {
                var learningMenu = new ToolStripMenuItem("Học tập");
                learningMenu.DropDownItems.Add("Bảng điều khiển", null, StudentDashboardMenuItem_Click);
                learningMenu.DropDownItems.Add("Khóa học của tôi", null, MyEnrolledCoursesMenuItem_Click);
                learningMenu.DropDownItems.Add("Tìm khóa học", null, FindCourseMenuItem_Click);
                learningMenu.DropDownItems.Add("Bài tập của tôi", null, MyAssignmentsMenuItem_Click);
                learningMenu.DropDownItems.Add("Kết quả quiz", null, MyQuizResultsMenuItem_Click);
                learningMenu.DropDownItems.Add("Chứng chỉ", null, MyCertificatesMenuItem_Click);
                menuStrip.Items.Add(learningMenu);
            }

            // Help Menu
            var helpMenu = new ToolStripMenuItem("Trợ giúp");
            helpMenu.DropDownItems.Add("Hướng dẫn sử dụng", null, UserGuideMenuItem_Click);
            helpMenu.DropDownItems.Add("Về chương trình", null, AboutMenuItem_Click);

            menuStrip.Items.Add(systemMenu);
            menuStrip.Items.Add(helpMenu);

            ShowWelcomeScreen();
        }

        private void InitializeStatusTimer()
        {
            statusTimer = new Timer
            {
                Interval = 1000 // Update every second
            };
            statusTimer.Tick += (s, e) => timeLabel.Text = DateTime.Now.ToString(AppConstants.DATETIME_FORMAT);
            statusTimer.Start();
        }

        private void ShowWelcomeScreen()
        {
            mainPanel.Controls.Clear();

            var welcomePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var welcomeLabel = new Label
            {
                Text = $"Chào mừng {currentUser.FullName}!\n\n" +
                       $"Vai trò: {currentUser.RoleDisplayName}\n\n" +
                       $"Hôm nay là {DateTime.Now:dddd, dd/MM/yyyy}\n\n" +
                       "Vui lòng chọn chức năng từ menu trên.",
                Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.DarkBlue
            };

            welcomePanel.Controls.Add(welcomeLabel);
            mainPanel.Controls.Add(welcomePanel);
        }

        // Event Handlers
        private void AccountInfoMenuItem_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountInfoForm(currentUser);
            accountForm.ShowDialog();
        }

        private void ChangePasswordMenuItem_Click(object sender, EventArgs e)
        {
            using var changePasswordForm = new ChangePasswordForm(currentUser.UserId, false);
            changePasswordForm.ShowDialog();
        }

        private void UserListMenuItem_Click(object sender, EventArgs e)
        {
            using var userManagementForm = new UserManagementForm();
            userManagementForm.ShowDialog();
        }

        private void AddUserMenuItem_Click(object sender, EventArgs e)
        {
            using var addUserForm = new AddUserForm();
            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                statusLabel.Text = MessageConstants.USER_CREATED_SUCCESS;
            }
        }

        private void UserReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CourseListMenuItem_Click(object sender, EventArgs e)
        {
            using var courseManagementForm = new CourseManagementForm();
            courseManagementForm.ShowDialog();
        }

        private void AddCourseMenuItem_Click(object sender, EventArgs e)
        {
            using var addCourseForm = new AddCourseForm();
            if (addCourseForm.ShowDialog() == DialogResult.OK)
            {
                statusLabel.Text = MessageConstants.COURSE_CREATED_SUCCESS;
            }
        }

        private void CourseReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OverviewReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StudentReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InstructorReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Instructor Event Handlers
        private void InstructorDashboardMenuItem_Click(object sender, EventArgs e)
        {
            using var dashboardForm = new InstructorDashboardForm(currentUser);
            dashboardForm.ShowDialog();
        }

        private void MyCoursesMenuItem_Click(object sender, EventArgs e)
        {
            using var courseManagementForm = new InstructorCourseManagementForm(currentUser);
            courseManagementForm.ShowDialog();
        }

        private void LessonManagementMenuItem_Click(object sender, EventArgs e)
        {
            var courses = courseService.GetCoursesByInstructor(currentUser.UserId);
            if (courses.Count == 0)
            {
                MessageBox.Show("Bạn chưa có khóa học nào để quản lý bài học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Show course selection dialog
            using var courseSelectionForm = new CourseSelectionForm(courses);
            if (courseSelectionForm.ShowDialog() == DialogResult.OK)
            {
                using var lessonForm = new LessonManagementForm(courseSelectionForm.SelectedCourseId);
                lessonForm.ShowDialog();
            }
        }

        private void StudentManagementMenuItem_Click(object sender, EventArgs e)
        {
            using var studentManagementForm = new InstructorStudentManagementForm(currentUser);
            studentManagementForm.ShowDialog();
        }

        private void AssignmentManagementMenuItem_Click(object sender, EventArgs e)
        {
            var courses = courseService.GetCoursesByInstructor(currentUser.UserId);
            if (courses.Count == 0)
            {
                MessageBox.Show("Bạn chưa có khóa học nào để quản lý bài tập!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using var courseSelectionForm = new CourseSelectionForm(courses);
            if (courseSelectionForm.ShowDialog() == DialogResult.OK)
            {
                using var assignmentForm = new AssignmentManagementForm(courseSelectionForm.SelectedCourseId);
                assignmentForm.ShowDialog();
            }
        }

        private void QuizManagementMenuItem_Click(object sender, EventArgs e)
        {
            var courses = courseService.GetCoursesByInstructor(currentUser.UserId);
            if (courses.Count == 0)
            {
                MessageBox.Show("Bạn chưa có khóa học nào để quản lý quiz!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var courseSelectionForm = new CourseSelectionForm(courses);
            if (courseSelectionForm.ShowDialog() == DialogResult.OK)
            {
                using var quizForm = new QuizManagementForm(courseSelectionForm.SelectedCourseId);
                quizForm.ShowDialog();
            }
        }

        private void MyEnrolledCoursesMenuItem_Click(object sender, EventArgs e)
        {
            using var studentCourseListForm = new StudentCourseListForm(currentUser);
            studentCourseListForm.ShowDialog();
        }

        private void FindCourseMenuItem_Click(object sender, EventArgs e)
        {
            using var courseEnrollmentForm = new CourseEnrollmentForm(currentUser);
            courseEnrollmentForm.ShowDialog();
        }

        private void MyAssignmentsMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyQuizResultsMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyCertificatesMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StudentDashboardMenuItem_Click(object sender, EventArgs e)
        {
            using var studentDashboardForm = new StudentDashboardForm(currentUser);
            studentDashboardForm.ShowDialog();
        }

        private void UserGuideMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MessageConstants.LOGOUT_CONFIRMATION, "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logger.LogInfo($"User {currentUser.Username} logged out");
                this.Hide();

                using var loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Restart with new user
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MessageConstants.EXIT_CONFIRMATION, "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logger.LogInfo("Application exiting");
                Application.Exit();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ExitMenuItem_Click(this, EventArgs.Empty);
            }
            else
            {
                statusTimer?.Stop();
                statusTimer?.Dispose();
                base.OnFormClosing(e);
            }
        }
    }
}
