using ORCLE_CK.Data.Repositories;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
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

        public QuizService()
        {
            quizRepository = new QuizRepository();
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
                Logger.LogError($"Error getting quiz results: {ex.Message}", ex);
                throw;
            }
        }

        public QuizResult GetQuizResultById(int resultId)
        {
            try
            {
                return quizRepository.GetQuizResultById(resultId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting quiz result by id: {ex.Message}", ex);
                throw;
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
                throw;
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
