using ORCLE_CK.Data.Repositories;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using ORCLE_CK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Services
{
    public class QuizService
    {
        private readonly IQuizRepository quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public QuizService() : this(new QuizRepository())
        {
        }

        public List<Quiz> GetQuizzesByCourse(int courseId)
        {
            try
            {
                return quizRepository.GetQuizzesByCourse(courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting quizzes by course: {ex.Message}", ex);
                throw;
            }
        }

        public Quiz GetQuizById(int quizId)
        {
            try
            {
                return quizRepository.GetQuizById(quizId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting quiz by id: {ex.Message}", ex);
                throw;
            }
        }

        public bool CreateQuiz(Quiz quiz)
        {
            try
            {
                quiz.CreatedAt = DateTime.Now;
                return quizRepository.CreateQuiz(quiz);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error creating quiz: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateQuiz(Quiz quiz)
        {
            try
            {
                quiz.UpdatedAt = DateTime.Now;
                return quizRepository.UpdateQuiz(quiz);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating quiz: {ex.Message}", ex);
                throw;
            }
        }

        public bool DeleteQuiz(int quizId)
        {
            try
            {
                return quizRepository.DeleteQuiz(quizId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error deleting quiz: {ex.Message}", ex);
                throw;
            }
        }

        public List<QuizQuestion> GetQuizQuestions(int quizId)
        {
            try
            {
                return quizRepository.GetQuizQuestions(quizId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting quiz questions: {ex.Message}", ex);
                throw;
            }
        }

        public QuizQuestion GetQuestionById(int questionId)
        {
            try
            {
                return quizRepository.GetQuestionById(questionId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting question by id: {ex.Message}", ex);
                throw;
            }
        }

        public bool AddQuizQuestion(QuizQuestion question)
        {
            try
            {
                // Validate correct answer is A, B, C, or D
                if (!new[] { "A", "B", "C", "D" }.Contains(question.CorrectAnswer))
                {
                    throw new ArgumentException("Đáp án đúng phải là A, B, C hoặc D");
                }

                return quizRepository.AddQuizQuestion(question);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error adding quiz question: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateQuizQuestion(QuizQuestion question)
        {
            try
            {
                // Validate correct answer is A, B, C, or D
                if (!new[] { "A", "B", "C", "D" }.Contains(question.CorrectAnswer))
                {
                    throw new ArgumentException("Đáp án đúng phải là A, B, C hoặc D");
                }

                return quizRepository.UpdateQuizQuestion(question);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating quiz question: {ex.Message}", ex);
                throw;
            }
        }

        public bool DeleteQuizQuestion(int questionId)
        {
            try
            {
                return quizRepository.DeleteQuizQuestion(questionId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error deleting quiz question: {ex.Message}", ex);
                throw;
            }
        }

        public List<QuizResult> GetQuizResults(int quizId)
        {
            try
            {
                return quizRepository.GetQuizResults(quizId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetQuizResults: {ex.Message}", ex);
                throw new ServiceException("Không thể tải kết quả quiz", ex);
            }
        }

        public QuizResult GetQuizResultDetail(int resultId)
        {
            try
            {
                var result = quizRepository.GetQuizResultDetail(resultId);
                if (result == null)
                {
                    throw new ServiceException("Không tìm thấy kết quả quiz");
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetQuizResultDetail: {ex.Message}", ex);
                throw new ServiceException("Không thể tải chi tiết kết quả quiz", ex);
            }
        }

        public List<QuizResult> GetUserQuizResults(int userId)
        {
            try
            {
                return quizRepository.GetUserQuizResults(userId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting user quiz results: {ex.Message}", ex);
                throw new ServiceException("Không thể tải kết quả quiz của học viên", ex);
            }
        }

        public bool SaveQuizResult(QuizResult result)
        {
            try
            {
                if (result == null)
                {
                    throw new ArgumentNullException(nameof(result));
                }

                // Validate required fields
                if (result.QuizId <= 0 || result.UserId <= 0)
                {
                    throw new ArgumentException("Quiz ID và User ID phải lớn hơn 0");
                }

                if (result.Score < 0)
                {
                    throw new ArgumentException("Điểm không được âm");
                }

                if (result.TimeTaken < 0)
                {
                    throw new ArgumentException("Thời gian làm bài không được âm");
                }

                result.TakenAt = DateTime.Now;
                return quizRepository.SaveQuizResult(result);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error saving quiz result: {ex.Message}", ex);
                throw new ServiceException("Không thể lưu kết quả quiz", ex);
            }
        }

        // Tính tổng điểm của quiz dựa trên các câu hỏi
        public int CalculateTotalScore(int quizId)
        {
            try
            {
                var questions = quizRepository.GetQuizQuestions(quizId);
                int totalScore = 0;

                foreach (var question in questions)
                {
                    totalScore += question.Points;
                }

                return totalScore;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error calculating total score: {ex.Message}", ex);
                throw;
            }
        }

        // Cập nhật tổng điểm của quiz
        public bool UpdateQuizTotalScore(int quizId)
        {
            try
            {
                var quiz = GetQuizById(quizId);
                if (quiz == null) return false;

                quiz.TotalScore = CalculateTotalScore(quizId);
                return UpdateQuiz(quiz);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating quiz total score: {ex.Message}", ex);
                throw;
            }
        }
    }
}
