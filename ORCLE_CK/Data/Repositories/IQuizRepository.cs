using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public interface IQuizRepository
    {
        List<Quiz> GetQuizzesByCourse(int courseId);
        Quiz GetQuizById(int quizId);
        bool CreateQuiz(Quiz quiz);
        bool UpdateQuiz(Quiz quiz);
        bool DeleteQuiz(int quizId);

        List<QuizQuestion> GetQuizQuestions(int quizId);
        QuizQuestion GetQuestionById(int questionId);
        bool AddQuizQuestion(QuizQuestion question);
        bool UpdateQuizQuestion(QuizQuestion question);
        bool DeleteQuizQuestion(int questionId);

        List<QuizResult> GetQuizResults(int quizId);
        QuizResult GetQuizResultById(int resultId);
        QuizResult GetQuizResultDetail(int resultId);
        List<QuizResult> GetUserQuizResults(int userId);
        bool SaveQuizResult(QuizResult result);
    }
}
