using Oracle.ManagedDataAccess.Client;
using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Data.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        public List<Quiz> GetQuizzesByCourse(int courseId)
        {
            var quizzes = new List<Quiz>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"SELECT q.QUIZ_ID, q.COURSE_ID, q.TITLE, q.DESCRIPTION, q.TIME_LIMIT, 
                               q.TOTAL_SCORE, q.CREATED_AT, q.UPDATED_AT, q.IS_ACTIVE,
                               c.TITLE as COURSE_NAME,
                               (SELECT COUNT(*) FROM QUIZ_QUESTIONS qq WHERE qq.QUIZ_ID = q.QUIZ_ID) as QUESTION_COUNT,
                               (SELECT COUNT(*) FROM QUIZ_RESULTS qr WHERE qr.QUIZ_ID = q.QUIZ_ID) as ATTEMPT_COUNT
                        FROM QUIZZES q
                        LEFT JOIN COURSES c ON q.COURSE_ID = c.COURSE_ID
                        WHERE q.COURSE_ID = :courseId
                        ORDER BY q.CREATED_AT DESC";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":courseId", OracleDbType.Int32).Value = courseId;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                quizzes.Add(new Quiz
                {
                    QuizId = reader.GetInt32(0), // QUIZ_ID
                    CourseId = reader.GetInt32(1), // COURSE_ID
                    Title = reader.GetString(2), // TITLE
                    Description = reader.IsDBNull(3) ? null : reader.GetString(3), // DESCRIPTION
                    TimeLimit = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4), // TIME_LIMIT
                    TotalScore = reader.IsDBNull(5) ? 0 : reader.GetInt32(5), // TOTAL_SCORE
                    CreatedAt = reader.GetDateTime(6), // CREATED_AT
                    UpdatedAt = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7), // UPDATED_AT
                    IsActive = reader.GetInt32(8) == 1, // IS_ACTIVE
                    CourseName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9), // COURSE_NAME
                    QuestionCount = reader.GetInt32(10), // QUESTION_COUNT
                    AttemptCount = reader.GetInt32(11) // ATTEMPT_COUNT
                });

            }

            return quizzes;
        }

        public Quiz GetQuizById(int quizId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"SELECT q.QUIZ_ID, q.COURSE_ID, q.TITLE, q.DESCRIPTION, q.TIME_LIMIT, 
                               q.TOTAL_SCORE, q.CREATED_AT, q.UPDATED_AT, q.IS_ACTIVE,
                               c.TITLE as COURSE_NAME
                        FROM QUIZZES q
                        LEFT JOIN COURSES c ON q.COURSE_ID = c.COURSE_ID
                        WHERE q.QUIZ_ID = :quizId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = quizId;

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Quiz
                {
                    QuizId = reader.GetInt32(0), // QUIZ_ID
                    CourseId = reader.GetInt32(1), // COURSE_ID
                    Title = reader.GetString(2), // TITLE
                    Description = reader.IsDBNull(3) ? null : reader.GetString(3), // DESCRIPTION
                    TimeLimit = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4), // TIME_LIMIT
                    TotalScore = reader.IsDBNull(5) ? 0 : reader.GetInt32(5), // TOTAL_SCORE
                    CreatedAt = reader.GetDateTime(6), // CREATED_AT
                    UpdatedAt = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7), // UPDATED_AT
                    IsActive = reader.GetInt32(8) == 1, // IS_ACTIVE
                    CourseName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9), // COURSE_NAME
                };
            }

            return null;
        }

        public bool CreateQuiz(Quiz quiz)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"INSERT INTO QUIZZES (COURSE_ID, TITLE, DESCRIPTION, TIME_LIMIT, 
                                           TOTAL_SCORE, CREATED_AT, IS_ACTIVE)
                        VALUES (:courseId, :title, :description, :timeLimit, 
                               :totalScore, :createdAt, :isActive)";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":courseId", OracleDbType.Int32).Value = quiz.CourseId;
            command.Parameters.Add(":title", OracleDbType.Varchar2).Value = quiz.Title;
            command.Parameters.Add(":description", OracleDbType.Clob).Value = quiz.Description ?? (object)DBNull.Value;
            command.Parameters.Add(":timeLimit", OracleDbType.Int32).Value = quiz.TimeLimit ?? (object)DBNull.Value;
            command.Parameters.Add(":totalScore", OracleDbType.Int32).Value = quiz.TotalScore;
            command.Parameters.Add(":createdAt", OracleDbType.Date).Value = quiz.CreatedAt;
            command.Parameters.Add(":isActive", OracleDbType.Int32).Value = quiz.IsActive ? 1 : 0;

            return command.ExecuteNonQuery() > 0;
        }

        public bool UpdateQuiz(Quiz quiz)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"UPDATE QUIZZES SET 
                               TITLE = :title,
                               DESCRIPTION = :description,
                               TIME_LIMIT = :timeLimit,
                               TOTAL_SCORE = :totalScore,
                               UPDATED_AT = :updatedAt,
                               IS_ACTIVE = :isActive
                        WHERE QUIZ_ID = :quizId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":title", OracleDbType.Varchar2).Value = quiz.Title;
            command.Parameters.Add(":description", OracleDbType.Clob).Value = quiz.Description ?? (object)DBNull.Value;
            command.Parameters.Add(":timeLimit", OracleDbType.Int32).Value = quiz.TimeLimit ?? (object)DBNull.Value;
            command.Parameters.Add(":totalScore", OracleDbType.Int32).Value = quiz.TotalScore;
            command.Parameters.Add(":updatedAt", OracleDbType.Date).Value = DateTime.Now;
            command.Parameters.Add(":isActive", OracleDbType.Int32).Value = quiz.IsActive ? 1 : 0;
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = quiz.QuizId;

            return command.ExecuteNonQuery() > 0;
        }

        public bool DeleteQuiz(int quizId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            // Soft delete
            var sql = "UPDATE QUIZZES SET IS_ACTIVE = 0, UPDATED_AT = :updatedAt WHERE QUIZ_ID = :quizId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":updatedAt", OracleDbType.Date).Value = DateTime.Now;
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = quizId;

            return command.ExecuteNonQuery() > 0;
        }

        public List<QuizQuestion> GetQuizQuestions(int quizId)
        {
            var questions = new List<QuizQuestion>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"SELECT QUESTION_ID, QUIZ_ID, QUESTION, 
                               OPTION_A, OPTION_B, OPTION_C, OPTION_D, 
                               CORRECT_ANSWER, POINTS, ORDER_NUMBER
                        FROM QUIZ_QUESTIONS 
                        WHERE QUIZ_ID = :quizId
                        ORDER BY ORDER_NUMBER";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = quizId;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                questions.Add(new QuizQuestion
                {
                    QuestionId = reader.GetInt32(0), // QUESTION_ID
                    QuizId = reader.GetInt32(1), // QUIZ_ID
                    Question = reader.GetString(2), // QUESTION
                    OptionA = reader.IsDBNull(3) ? null : reader.GetString(3), // OPTION_A
                    OptionB = reader.IsDBNull(4) ? null : reader.GetString(4), // OPTION_B
                    OptionC = reader.IsDBNull(5) ? null : reader.GetString(5), // OPTION_C
                    OptionD = reader.IsDBNull(6) ? null : reader.GetString(6), // OPTION_D
                    CorrectAnswer = reader.GetString(7), // CORRECT_ANSWER
                    Points = reader.GetInt32(8), // POINTS
                    OrderNumber = reader.GetInt32(9) // ORDER_NUMBER
                });
            }

            return questions;
        }

        public QuizQuestion GetQuestionById(int questionId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"SELECT QUESTION_ID, QUIZ_ID, QUESTION, 
                               OPTION_A, OPTION_B, OPTION_C, OPTION_D, 
                               CORRECT_ANSWER, POINTS, ORDER_NUMBER
                        FROM QUIZ_QUESTIONS 
                        WHERE QUESTION_ID = :questionId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":questionId", OracleDbType.Int32).Value = questionId;

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new QuizQuestion
                {
                    QuestionId = reader.GetInt32(0), // QUESTION_ID
                    QuizId = reader.GetInt32(1), // QUIZ_ID
                    Question = reader.GetString(2), // QUESTION
                    OptionA = reader.IsDBNull(3) ? null : reader.GetString(3), // OPTION_A
                    OptionB = reader.IsDBNull(4) ? null : reader.GetString(4), // OPTION_B
                    OptionC = reader.IsDBNull(5) ? null : reader.GetString(5), // OPTION_C
                    OptionD = reader.IsDBNull(6) ? null : reader.GetString(6), // OPTION_D
                    CorrectAnswer = reader.GetString(7), // CORRECT_ANSWER
                    Points = reader.GetInt32(8), // POINTS
                    OrderNumber = reader.GetInt32(9) // ORDER_NUMBER
                };
            }

            return null;
        }

        public bool AddQuizQuestion(QuizQuestion question)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"INSERT INTO QUIZ_QUESTIONS (QUIZ_ID, QUESTION, 
                                                   OPTION_A, OPTION_B, OPTION_C, OPTION_D, 
                                                   CORRECT_ANSWER, POINTS, ORDER_NUMBER)
                        VALUES (:quizId, :question, :optionA, :optionB, 
                               :optionC, :optionD, :correctAnswer, :points, :orderNumber)";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = question.QuizId;
            command.Parameters.Add(":question", OracleDbType.Clob).Value = question.Question;
            command.Parameters.Add(":optionA", OracleDbType.Clob).Value = question.OptionA ?? (object)DBNull.Value;
            command.Parameters.Add(":optionB", OracleDbType.Clob).Value = question.OptionB ?? (object)DBNull.Value;
            command.Parameters.Add(":optionC", OracleDbType.Clob).Value = question.OptionC ?? (object)DBNull.Value;
            command.Parameters.Add(":optionD", OracleDbType.Clob).Value = question.OptionD ?? (object)DBNull.Value;
            command.Parameters.Add(":correctAnswer", OracleDbType.Char).Value = question.CorrectAnswer;
            command.Parameters.Add(":points", OracleDbType.Int32).Value = question.Points;
            command.Parameters.Add(":orderNumber", OracleDbType.Int32).Value = question.OrderNumber;

            return command.ExecuteNonQuery() > 0;
        }

        public bool UpdateQuizQuestion(QuizQuestion question)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"UPDATE QUIZ_QUESTIONS SET 
                               QUESTION = :question,
                               OPTION_A = :optionA,
                               OPTION_B = :optionB,
                               OPTION_C = :optionC,
                               OPTION_D = :optionD,
                               CORRECT_ANSWER = :correctAnswer,
                               POINTS = :points,
                               ORDER_NUMBER = :orderNumber
                        WHERE QUESTION_ID = :questionId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":question", OracleDbType.Clob).Value = question.Question;
            command.Parameters.Add(":optionA", OracleDbType.Clob).Value = question.OptionA ?? (object)DBNull.Value;
            command.Parameters.Add(":optionB", OracleDbType.Clob).Value = question.OptionB ?? (object)DBNull.Value;
            command.Parameters.Add(":optionC", OracleDbType.Clob).Value = question.OptionC ?? (object)DBNull.Value;
            command.Parameters.Add(":optionD", OracleDbType.Clob).Value = question.OptionD ?? (object)DBNull.Value;
            command.Parameters.Add(":correctAnswer", OracleDbType.Char).Value = question.CorrectAnswer;
            command.Parameters.Add(":points", OracleDbType.Int32).Value = question.Points;
            command.Parameters.Add(":orderNumber", OracleDbType.Int32).Value = question.OrderNumber;
            command.Parameters.Add(":questionId", OracleDbType.Int32).Value = question.QuestionId;

            return command.ExecuteNonQuery() > 0;
        }

        public bool DeleteQuizQuestion(int questionId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            // Hard delete for questions
            var sql = "DELETE FROM QUIZ_QUESTIONS WHERE QUESTION_ID = :questionId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":questionId", OracleDbType.Int32).Value = questionId;

            return command.ExecuteNonQuery() > 0;
        }

        public List<QuizResult> GetQuizResults(int quizId)
        {
            var results = new List<QuizResult>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();


            var sql = @"SELECT r.RESULT_ID, r.QUIZ_ID, r.USER_ID, r.SCORE, r.TAKEN_AT,
                               r.TOTAL_QUESTIONS, r.CORRECT_ANSWERS, r.TIME_TAKEN,
                               u.FULL_NAME as STUDENT_NAME,
                               q.TITLE as QUIZ_TITLE, q.TIME_LIMIT, q.TOTAL_SCORE
                        FROM QUIZ_RESULTS r
                        JOIN USERS u ON r.USER_ID = u.USER_ID
                        JOIN QUIZZES q ON r.QUIZ_ID = q.QUIZ_ID
                        WHERE r.QUIZ_ID = :quizId
                        ORDER BY r.TAKEN_AT DESC";
            //MessageBox.Show("trước h");

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = quizId;

            using var reader = command.ExecuteReader();
            //MessageBox.Show("sau h");
            while (reader.Read())
            {
                results.Add(new QuizResult
                {
                    ResultId = reader.GetInt32(0),
                    QuizId = reader.GetInt32(1),
                    UserId = reader.GetInt32(2),
                    Score = reader.GetDecimal(3),
                    TakenAt = reader.GetDateTime(4),
                    TotalQuestions = reader.GetInt32(5),
                    CorrectAnswers = reader.GetInt32(6),
                    TimeTaken = reader.GetInt32(7),
                    StudentName = reader.GetString(8),
                    QuizTitle = reader.GetString(9),
                    TimeLimit = reader.GetInt32(10),
                    TotalScore = reader.GetInt32(11)
                });
            }

            return results;
        }

        public QuizResult GetQuizResultById(int resultId)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"SELECT r.RESULT_ID, r.QUIZ_ID, r.USER_ID, r.SCORE, r.TAKEN_AT,
                               r.TOTAL_QUESTIONS, r.CORRECT_ANSWERS, r.TIME_TAKEN,
                               u.FIRST_NAME || ' ' || u.LAST_NAME as STUDENT_NAME,
                               q.TITLE as QUIZ_TITLE, q.TIME_LIMIT, q.TOTAL_SCORE
                        FROM QUIZ_RESULTS r
                        JOIN USERS u ON r.USER_ID = u.USER_ID
                        JOIN QUIZZES q ON r.QUIZ_ID = q.QUIZ_ID
                        WHERE r.RESULT_ID = :resultId";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":resultId", OracleDbType.Int32).Value = resultId;

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new QuizResult
                {
                    ResultId = reader.GetInt32(0),
                    QuizId = reader.GetInt32(1),
                    UserId = reader.GetInt32(2),
                    Score = reader.GetDecimal(3),
                    TakenAt = reader.GetDateTime(4),
                    TotalQuestions = reader.GetInt32(5),
                    CorrectAnswers = reader.GetInt32(6),
                    TimeTaken = reader.GetInt32(7),
                    StudentName = reader.GetString(8),
                    QuizTitle = reader.GetString(9),
                    TimeLimit = reader.GetInt32(10),
                    TotalScore = reader.GetInt32(11)
                };
            }

            return null;
        }

        public QuizResult GetQuizResultDetail(int resultId)
        {
            return GetQuizResultById(resultId);
        }

        public List<QuizResult> GetUserQuizResults(int userId)
        {
            var results = new List<QuizResult>();

            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"SELECT r.RESULT_ID, r.QUIZ_ID, r.USER_ID, r.SCORE, r.TAKEN_AT,
                               r.TOTAL_QUESTIONS, r.CORRECT_ANSWERS, r.TIME_TAKEN,
                               u.FIRST_NAME || ' ' || u.LAST_NAME as STUDENT_NAME,
                               q.TITLE as QUIZ_TITLE, q.TIME_LIMIT, q.TOTAL_SCORE
                        FROM QUIZ_RESULTS r
                        JOIN USERS u ON r.USER_ID = u.USER_ID
                        JOIN QUIZZES q ON r.QUIZ_ID = q.QUIZ_ID
                        WHERE r.USER_ID = :userId
                        ORDER BY r.TAKEN_AT DESC";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new QuizResult
                {
                    ResultId = reader.GetInt32(0),
                    QuizId = reader.GetInt32(1),
                    UserId = reader.GetInt32(2),
                    Score = reader.GetDecimal(3),
                    TakenAt = reader.GetDateTime(4),
                    TotalQuestions = reader.GetInt32(5),
                    CorrectAnswers = reader.GetInt32(6),
                    TimeTaken = reader.GetInt32(7),
                    StudentName = reader.GetString(8),
                    QuizTitle = reader.GetString(9),
                    TimeLimit = reader.GetInt32(10),
                    TotalScore = reader.GetInt32(11)
                });
            }

            return results;
        }

        public bool SaveQuizResult(QuizResult result)
        {
            using var connection = DatabaseConnection.GetConnection();
            connection.Open();

            var sql = @"INSERT INTO QUIZ_RESULTS (
                            QUIZ_ID, USER_ID, SCORE, TAKEN_AT,
                            TOTAL_QUESTIONS, CORRECT_ANSWERS, TIME_TAKEN
                        ) VALUES (
                            :quizId, :userId, :score, :takenAt,
                            :totalQuestions, :correctAnswers, :timeTaken
                        )";

            using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(":quizId", OracleDbType.Int32).Value = result.QuizId;
            command.Parameters.Add(":userId", OracleDbType.Int32).Value = result.UserId;
            command.Parameters.Add(":score", OracleDbType.Decimal).Value = result.Score;
            command.Parameters.Add(":takenAt", OracleDbType.Date).Value = result.TakenAt;
            command.Parameters.Add(":totalQuestions", OracleDbType.Int32).Value = result.TotalQuestions;
            command.Parameters.Add(":correctAnswers", OracleDbType.Int32).Value = result.CorrectAnswers;
            command.Parameters.Add(":timeTaken", OracleDbType.Int32).Value = result.TimeTaken;

            return command.ExecuteNonQuery() > 0;
        }
    }
}
