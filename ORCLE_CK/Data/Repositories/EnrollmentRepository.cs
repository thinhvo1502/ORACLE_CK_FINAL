using Oracle.ManagedDataAccess.Client;
using ORCLE_CK.Models;
using ORCLE_CK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Data.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public List<Enrollment> GetByStudentId(int studentId)
        {
            var enrollments = new List<Enrollment>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = @"
                SELECT e.enrollment_id, e.user_id, e.course_id, e.enrolled_at, 
                       e.completed_at, e.status, e.progress, e.final_grade,
                       u.full_name as student_name, c.title as course_title,
                       i.full_name as instructor_name
                FROM enrollments e
                JOIN users u ON e.user_id = u.user_id
                JOIN courses c ON e.course_id = c.course_id
                JOIN users i ON c.instructor_id = i.user_id
                WHERE e.user_id = :studentId
                ORDER BY e.enrolled_at DESC";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":studentId", studentId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                enrollments.Add(new Enrollment
                {
                    EnrollmentId = Convert.ToInt32(reader["enrollment_id"]),
                    UserId = Convert.ToInt32(reader["user_id"]),
                    CourseId = Convert.ToInt32(reader["course_id"]),
                    EnrolledAt = Convert.ToDateTime(reader["enrolled_at"]),

                    CompletedAt = reader["completed_at"] == DBNull.Value
        ? (DateTime?)null
        : Convert.ToDateTime(reader["completed_at"]),

                    Status = Enum.TryParse<EnrollmentStatus>(reader["status"]?.ToString(), out var status)
            ? status
            : EnrollmentStatus.Active, // hoặc throw nếu bạn muốn bắt lỗi
                    Progress = Convert.ToDecimal(reader["progress"]),

                    FinalGrade = reader["final_grade"] == DBNull.Value
        ? (decimal?)null
        : Convert.ToDecimal(reader["final_grade"]),

                    StudentName = reader["student_name"]?.ToString(),
                    CourseTitle = reader["course_title"]?.ToString(),
                    InstructorName = reader["instructor_name"]?.ToString()
                });


            }

            return enrollments;
        }

        public List<Course> GetEnrolledCourses(int studentId)
        {
            var courses = new List<Course>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = @"
                SELECT c.course_id, c.title, c.description, c.instructor_id,
                       c.created_at, c.updated_at, c.is_active,
                       i.full_name as instructor_name,
                       e.progress, e.status as enrollment_status
                FROM courses c
                JOIN enrollments e ON c.course_id = e.course_id
                JOIN users i ON c.instructor_id = i.user_id
                WHERE e.user_id = :studentId AND e.status = 'active'
                ORDER BY e.enrolled_at DESC";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":studentId", studentId);
            //MessageBox.Show("huhu");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                
                courses.Add(new Course
                {
                    CourseId = Convert.ToInt32(reader["course_id"]),
                    Title = reader["title"]?.ToString(),
                    Description = reader.IsDBNull(reader.GetOrdinal("description"))
        ? null
        : reader["description"].ToString(),
                    InstructorId = Convert.ToInt32(reader["instructor_id"]),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    UpdatedAt = reader.IsDBNull(reader.GetOrdinal("updated_at"))
    ? (DateTime?)null
    : reader.GetDateTime(reader.GetOrdinal("updated_at")),
                    IsActive = Convert.ToInt32(reader["is_active"]) == 1,
                    InstructorName = reader["instructor_name"]?.ToString()
                });


            }

            return courses;
        }

        public bool IsEnrolled(int studentId, int courseId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = "SELECT COUNT(*) FROM enrollments WHERE user_id = :studentId AND course_id = :courseId";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":studentId", studentId);
            command.Parameters.Add(":courseId", courseId);

            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        public bool Create(Enrollment enrollment)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = @"
                INSERT INTO enrollments (user_id, course_id, enrolled_at, status, progress)
                VALUES (:userId, :courseId, :enrolledAt, :status, :progress)";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":userId", enrollment.UserId);
            command.Parameters.Add(":courseId", enrollment.CourseId);
            command.Parameters.Add(":enrolledAt", enrollment.EnrolledAt);
            command.Parameters.Add(":status", "active");
            command.Parameters.Add(":progress", enrollment.Progress);

            return command.ExecuteNonQuery() > 0;
        }

        public bool UpdateProgress(int studentId, int courseId, decimal progress)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = @"
                UPDATE enrollments 
                SET progress = :progress,
                    completed_at = CASE WHEN :progress >= 100 THEN SYSDATE ELSE completed_at END
                WHERE user_id = :studentId AND course_id = :courseId";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":progress", progress);
            command.Parameters.Add(":studentId", studentId);
            command.Parameters.Add(":courseId", courseId);

            return command.ExecuteNonQuery() > 0;
        }

        public Enrollment GetByStudentAndCourse(int studentId, int courseId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = @"
                SELECT enrollment_id, user_id, course_id, enrolled_at, 
                       completed_at, status, progress, final_grade
                FROM enrollments 
                WHERE user_id = :studentId AND course_id = :courseId";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":studentId", studentId);
            command.Parameters.Add(":courseId", courseId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Enrollment
                {
                    EnrollmentId = reader.GetInt32(reader.GetOrdinal("enrollment_id")),
                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                    CourseId = reader.GetInt32(reader.GetOrdinal("course_id")),
                    EnrolledAt = reader.GetDateTime(reader.GetOrdinal("enrolled_at")),

                    CompletedAt = reader.IsDBNull(reader.GetOrdinal("completed_at"))
        ? (DateTime?)null
        : reader.GetDateTime(reader.GetOrdinal("completed_at")),

                    Status = Enum.TryParse<EnrollmentStatus>(
        reader["status"]?.ToString(), true, out var status)
        ? status
        : EnrollmentStatus.Active,

                    Progress = reader.GetDecimal(reader.GetOrdinal("progress")),

                    FinalGrade = reader.IsDBNull(reader.GetOrdinal("final_grade"))
        ? (decimal?)null
        : reader.GetDecimal(reader.GetOrdinal("final_grade"))
                };

            }

            return null;
        }

        public List<Enrollment> GetByCourseId(int courseId)
        {
            var enrollments = new List<Enrollment>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = @"
                SELECT e.enrollment_id, e.user_id, e.course_id, e.enrolled_at, 
                       e.completed_at, e.status, e.progress, e.final_grade,
                       u.full_name as student_name
                FROM enrollments e
                JOIN users u ON e.user_id = u.user_id
                WHERE e.course_id = :courseId
                ORDER BY e.enrolled_at DESC";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":courseId", courseId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                enrollments.Add(new Enrollment
                {
                    EnrollmentId = reader.GetInt32(reader.GetOrdinal("enrollment_id")),
                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                    CourseId = reader.GetInt32(reader.GetOrdinal("course_id")),
                    EnrolledAt = reader.GetDateTime(reader.GetOrdinal("enrolled_at")),

                    CompletedAt = reader.IsDBNull(reader.GetOrdinal("completed_at"))
        ? (DateTime?)null
        : reader.GetDateTime(reader.GetOrdinal("completed_at")),

                    Status = Enum.TryParse<EnrollmentStatus>(
        reader["status"]?.ToString(), true, out var status)
        ? status
        : EnrollmentStatus.Active,

                    Progress = reader.GetDecimal(reader.GetOrdinal("progress")),

                    FinalGrade = reader.IsDBNull(reader.GetOrdinal("final_grade"))
        ? (decimal?)null
        : reader.GetDecimal(reader.GetOrdinal("final_grade")),

                    StudentName = reader["student_name"]?.ToString() ?? ""
                });

            }

            return enrollments;
        }

        public bool Delete(int enrollmentId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();
            DatabaseConnection.setUp(connection);

            var query = "DELETE FROM enrollments WHERE enrollment_id = :enrollmentId";

            using var command = new OracleCommand(query, connection);
            command.Parameters.Add(":enrollmentId", enrollmentId);

            return command.ExecuteNonQuery() > 0;
        }
    }
}
