using Oracle.ManagedDataAccess.Client;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using ORCLE_CK.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        public List<Lesson> GetLessonsByCourse(int courseId)
        {
            var lessons = new List<Lesson>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT l.lesson_id, l.course_id, l.title, l.content, l.video_url, 
                                  l.order_number, l.created_at, l.updated_at, l.is_active, l.duration,
                                  c.title as course_name
                                  FROM Lessons l 
                                  LEFT JOIN Courses c ON l.course_id = c.course_id
                                  WHERE l.course_id = :courseId 
                                  ORDER BY l.order_number";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = courseId;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lessons.Add(MapReaderToLesson(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting lessons by course {courseId}: {ex.Message}", ex);
                throw;
            }

            return lessons;
        }

        public Lesson GetLessonById(int lessonId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT l.lesson_id, l.course_id, l.title, l.content, l.video_url, 
                                  l.order_number, l.created_at, l.updated_at, l.is_active, l.duration,
                                  c.title as course_name
                                  FROM Lessons l 
                                  LEFT JOIN Courses c ON l.course_id = c.course_id
                                  WHERE l.lesson_id = :lessonId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":lessonId", OracleDbType.Int32).Value = lessonId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToLesson(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting lesson by ID {lessonId}: {ex.Message}", ex);
                throw;
            }

            return null;
        }

        public bool CreateLesson(Lesson lesson)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO Lessons (course_id, title, content, video_url, order_number, duration, is_active) 
                                  VALUES (:courseId, :title, :content, :videoUrl, :orderNumber, :duration, 1)";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = lesson.CourseId;
                        command.Parameters.Add(":title", OracleDbType.NVarchar2).Value = lesson.Title;
                        command.Parameters.Add(":content", OracleDbType.NClob).Value = lesson.Content ?? (object)DBNull.Value;
                        command.Parameters.Add(":videoUrl", OracleDbType.Varchar2).Value = lesson.VideoUrl ?? (object)DBNull.Value;
                        command.Parameters.Add(":orderNumber", OracleDbType.Int32).Value = lesson.OrderNumber;
                        command.Parameters.Add(":duration", OracleDbType.Int32).Value = lesson.Duration;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Lesson {lesson.Title} created successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error creating lesson {lesson.Title}: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateLesson(Lesson lesson)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE Lessons SET title = :title, content = :content, video_url = :videoUrl, 
                                  order_number = :orderNumber, duration = :duration, updated_at = SYSDATE, is_active = :isActive 
                                  WHERE lesson_id = :lessonId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":title", OracleDbType.NVarchar2).Value = lesson.Title;
                        command.Parameters.Add(":content", OracleDbType.NClob).Value = lesson.Content ?? (object)DBNull.Value;
                        command.Parameters.Add(":videoUrl", OracleDbType.Varchar2).Value = lesson.VideoUrl ?? (object)DBNull.Value;
                        command.Parameters.Add(":orderNumber", OracleDbType.Int32).Value = lesson.OrderNumber;
                        command.Parameters.Add(":duration", OracleDbType.Int32).Value = lesson.Duration;
                        command.Parameters.Add(":isActive", OracleDbType.Int32).Value = lesson.IsActive ? 1 : 0;
                        command.Parameters.Add(":lessonId", OracleDbType.Int32).Value = lesson.LessonId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Lesson {lesson.Title} updated successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating lesson {lesson.LessonId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool DeleteLesson(int lessonId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    // Soft delete - set is_active to 0
                    string sql = "UPDATE Lessons SET is_active = 0 WHERE lesson_id = :lessonId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":lessonId", OracleDbType.Int32).Value = lessonId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Lesson {lessonId} deleted successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error deleting lesson {lessonId}: {ex.Message}", ex);
                throw;
            }
        }

        public int GetNextOrderNumber(int courseId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT NVL(MAX(order_number), 0) + 1 FROM Lessons WHERE course_id = :courseId";

                    using (var command = DatabaseConnection.CreateCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = courseId;

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting next order number for course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool MoveLessonUp(int lessonId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Get current lesson
                            var currentLesson = GetLessonById(lessonId);
                            if (currentLesson == null || currentLesson.OrderNumber <= 1)
                                return false;

                            // Find lesson with order_number = current - 1
                            string findSql = @"SELECT lesson_id FROM Lessons 
                                             WHERE course_id = :courseId AND order_number = :orderNumber AND is_active = 1";

                            using (var findCommand = new OracleCommand(findSql, connection))
                            {
                                findCommand.Transaction = transaction;
                                findCommand.Parameters.Add(":courseId", OracleDbType.Int32).Value = currentLesson.CourseId;
                                findCommand.Parameters.Add(":orderNumber", OracleDbType.Int32).Value = currentLesson.OrderNumber - 1;

                                var previousLessonId = findCommand.ExecuteScalar();
                                if (previousLessonId == null)
                                    return false;

                                // Swap order numbers
                                string updateSql = "UPDATE Lessons SET order_number = :newOrder WHERE lesson_id = :lessonId";

                                // Update current lesson
                                using (var updateCommand1 = new OracleCommand(updateSql, connection))
                                {
                                    updateCommand1.Transaction = transaction;
                                    updateCommand1.Parameters.Add(":newOrder", OracleDbType.Int32).Value = currentLesson.OrderNumber - 1;
                                    updateCommand1.Parameters.Add(":lessonId", OracleDbType.Int32).Value = lessonId;
                                    updateCommand1.ExecuteNonQuery();
                                }

                                // Update previous lesson
                                using (var updateCommand2 = new OracleCommand(updateSql, connection))
                                {
                                    updateCommand2.Transaction = transaction;
                                    updateCommand2.Parameters.Add(":newOrder", OracleDbType.Int32).Value = currentLesson.OrderNumber;
                                    updateCommand2.Parameters.Add(":lessonId", OracleDbType.Int32).Value = Convert.ToInt32(previousLessonId);
                                    updateCommand2.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error moving lesson up {lessonId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool MoveLessonDown(int lessonId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Get current lesson
                            var currentLesson = GetLessonById(lessonId);
                            if (currentLesson == null)
                                return false;

                            // Find lesson with order_number = current + 1
                            string findSql = @"SELECT lesson_id FROM Lessons 
                                             WHERE course_id = :courseId AND order_number = :orderNumber AND is_active = 1";

                            using (var findCommand = new OracleCommand(findSql, connection))
                            {
                                findCommand.Transaction = transaction;
                                findCommand.Parameters.Add(":courseId", OracleDbType.Int32).Value = currentLesson.CourseId;
                                findCommand.Parameters.Add(":orderNumber", OracleDbType.Int32).Value = currentLesson.OrderNumber + 1;

                                var nextLessonId = findCommand.ExecuteScalar();
                                if (nextLessonId == null)
                                    return false;

                                // Swap order numbers
                                string updateSql = "UPDATE Lessons SET order_number = :newOrder WHERE lesson_id = :lessonId";

                                // Update current lesson
                                using (var updateCommand1 = new OracleCommand(updateSql, connection))
                                {
                                    updateCommand1.Transaction = transaction;
                                    updateCommand1.Parameters.Add(":newOrder", OracleDbType.Int32).Value = currentLesson.OrderNumber + 1;
                                    updateCommand1.Parameters.Add(":lessonId", OracleDbType.Int32).Value = lessonId;
                                    updateCommand1.ExecuteNonQuery();
                                }

                                // Update next lesson
                                using (var updateCommand2 = new OracleCommand(updateSql, connection))
                                {
                                    updateCommand2.Transaction = transaction;
                                    updateCommand2.Parameters.Add(":newOrder", OracleDbType.Int32).Value = currentLesson.OrderNumber;
                                    updateCommand2.Parameters.Add(":lessonId", OracleDbType.Int32).Value = Convert.ToInt32(nextLessonId);
                                    updateCommand2.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error moving lesson down {lessonId}: {ex.Message}", ex);
                throw;
            }
        }

        private static Lesson MapReaderToLesson(IDataReader reader)
        {
            return new Lesson
            {
                LessonId = Convert.ToInt32(reader["lesson_id"]),
                CourseId = Convert.ToInt32(reader["course_id"]),
                Title = reader["title"]?.ToString() ?? string.Empty,
                Content = reader["content"]?.ToString(),
                VideoUrl = reader["video_url"]?.ToString(),
                OrderNumber = Convert.ToInt32(reader["order_number"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                UpdatedAt = reader["updated_at"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["updated_at"]),
                IsActive = Convert.ToInt32(reader["is_active"]) == 1,
                Duration = Convert.ToInt32(reader["duration"]),
                CourseName = reader["course_name"]?.ToString() ?? string.Empty
            };
        }
    }
}
