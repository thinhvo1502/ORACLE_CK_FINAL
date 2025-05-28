using Oracle.ManagedDataAccess.Client;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public List<Assignment> GetAssignmentsByCourse(int courseId)
        {
            var assignments = new List<Assignment>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT a.assignment_id, a.course_id, a.title, a.description, a.due_date,
                                  a.created_at, a.updated_at, a.is_active, a.max_score,
                                  c.title as course_name,
                                  (SELECT COUNT(*) FROM Submissions s WHERE s.assignment_id = a.assignment_id) as submission_count
                                  FROM Assignments a 
                                  LEFT JOIN Courses c ON a.course_id = c.course_id
                                  WHERE a.course_id = :courseId 
                                  ORDER BY a.created_at DESC";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = courseId;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                assignments.Add(MapReaderToAssignment(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting assignments by course {courseId}: {ex.Message}", ex);
                throw;
            }

            return assignments;
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT a.assignment_id, a.course_id, a.title, a.description, a.due_date,
                                  a.created_at, a.updated_at, a.is_active, a.max_score,
                                  c.title as course_name,
                                  (SELECT COUNT(*) FROM Submissions s WHERE s.assignment_id = a.assignment_id) as submission_count
                                  FROM Assignments a 
                                  LEFT JOIN Courses c ON a.course_id = c.course_id
                                  WHERE a.assignment_id = :assignmentId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":assignmentId", OracleDbType.Int32).Value = assignmentId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToAssignment(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting assignment by ID {assignmentId}: {ex.Message}", ex);
                throw;
            }

            return null;
        }

        public bool CreateAssignment(Assignment assignment)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO Assignments (course_id, title, description, due_date, max_score, is_active) 
                                  VALUES (:courseId, :title, :description, :dueDate, :maxScore, 1)";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = assignment.CourseId;
                        command.Parameters.Add(":title", OracleDbType.NVarchar2).Value = assignment.Title;
                        command.Parameters.Add(":description", OracleDbType.NClob).Value = assignment.Description ?? (object)DBNull.Value;
                        command.Parameters.Add(":dueDate", OracleDbType.Date).Value = assignment.DueDate ?? (object)DBNull.Value;
                        command.Parameters.Add(":maxScore", OracleDbType.Int32).Value = assignment.MaxScore;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Assignment {assignment.Title} created successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error creating assignment {assignment.Title}: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateAssignment(Assignment assignment)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE Assignments SET title = :title, description = :description, due_date = :dueDate,
                                  max_score = :maxScore, updated_at = SYSDATE, is_active = :isActive 
                                  WHERE assignment_id = :assignmentId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":title", OracleDbType.NVarchar2).Value = assignment.Title;
                        command.Parameters.Add(":description", OracleDbType.NClob).Value = assignment.Description ?? (object)DBNull.Value;
                        command.Parameters.Add(":dueDate", OracleDbType.Date).Value = assignment.DueDate ?? (object)DBNull.Value;
                        command.Parameters.Add(":maxScore", OracleDbType.Int32).Value = assignment.MaxScore;
                        command.Parameters.Add(":isActive", OracleDbType.Int32).Value = assignment.IsActive ? 1 : 0;
                        command.Parameters.Add(":assignmentId", OracleDbType.Int32).Value = assignment.AssignmentId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Assignment {assignment.Title} updated successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating assignment {assignment.AssignmentId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool DeleteAssignment(int assignmentId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "UPDATE Assignments SET is_active = 0 WHERE assignment_id = :assignmentId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":assignmentId", OracleDbType.Int32).Value = assignmentId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Assignment {assignmentId} deleted successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error deleting assignment {assignmentId}: {ex.Message}", ex);
                throw;
            }
        }

        public List<Submission> GetSubmissionsByAssignment(int assignmentId)
        {
            var submissions = new List<Submission>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT s.submission_id, s.assignment_id, s.student_id, s.content, s.file_path,
                                  s.submitted_at, s.graded_at, s.score, s.feedback, s.status,
                                  a.title as assignment_title, a.max_score,
                                  u.full_name as student_name,
                                  c.title as course_name
                                  FROM Submissions s
                                  LEFT JOIN Assignments a ON s.assignment_id = a.assignment_id
                                  LEFT JOIN Users u ON s.student_id = u.user_id
                                  LEFT JOIN Courses c ON a.course_id = c.course_id
                                  WHERE s.assignment_id = :assignmentId
                                  ORDER BY s.submitted_at DESC";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":assignmentId", OracleDbType.Int32).Value = assignmentId;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                submissions.Add(MapReaderToSubmission(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting submissions by assignment {assignmentId}: {ex.Message}", ex);
                throw;
            }

            return submissions;
        }

        public bool GradeSubmission(int submissionId, int score, string feedback)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE Submissions SET score = :score, feedback = :feedback, 
                                  graded_at = SYSDATE, status = 'Graded' 
                                  WHERE submission_id = :submissionId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":score", OracleDbType.Int32).Value = score;
                        command.Parameters.Add(":feedback", OracleDbType.NClob).Value = feedback ?? (object)DBNull.Value;
                        command.Parameters.Add(":submissionId", OracleDbType.Int32).Value = submissionId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Submission {submissionId} graded successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error grading submission {submissionId}: {ex.Message}", ex);
                throw;
            }
        }

        private static Assignment MapReaderToAssignment(IDataReader reader)
        {
            return new Assignment
            {
                AssignmentId = Convert.ToInt32(reader["assignment_id"]),
                CourseId = Convert.ToInt32(reader["course_id"]),
                Title = reader["title"]?.ToString() ?? string.Empty,
                Description = reader["description"]?.ToString(),
                DueDate = reader["due_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["due_date"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                UpdatedAt = reader["updated_at"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["updated_at"]),
                IsActive = Convert.ToInt32(reader["is_active"]) == 1,
                MaxScore = Convert.ToInt32(reader["max_score"]),
                CourseName = reader["course_name"]?.ToString() ?? string.Empty,
                SubmissionCount = Convert.ToInt32(reader["submission_count"])
            };
        }

        private static Submission MapReaderToSubmission(IDataReader reader)
        {
            return new Submission
            {
                SubmissionId = Convert.ToInt32(reader["submission_id"]),
                AssignmentId = Convert.ToInt32(reader["assignment_id"]),
                StudentId = Convert.ToInt32(reader["student_id"]),
                Content = reader["content"]?.ToString(),
                FilePath = reader["file_path"]?.ToString(),
                SubmittedAt = Convert.ToDateTime(reader["submitted_at"]),
                GradedAt = reader["graded_at"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["graded_at"]),
                Score = reader["score"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["score"]),
                Feedback = reader["feedback"]?.ToString(),
                Status = reader["status"]?.ToString() ?? "Submitted",
                AssignmentTitle = reader["assignment_title"]?.ToString() ?? string.Empty,
                StudentName = reader["student_name"]?.ToString() ?? string.Empty,
                CourseName = reader["course_name"]?.ToString() ?? string.Empty,
                MaxScore = Convert.ToInt32(reader["max_score"])
            };
        }
    }
}
