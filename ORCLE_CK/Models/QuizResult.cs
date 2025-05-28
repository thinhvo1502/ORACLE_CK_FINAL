using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class QuizResult
    {
        public int ResultId { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public decimal Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public DateTime TakenAt { get; set; }
        public int TimeTaken { get; set; } // in minutes
        public int TimeLimit { get; set; } // in minutes
        public int TotalScore { get; set; }

        // Navigation properties
        public string StudentName { get; set; } = string.Empty;
        public string QuizTitle { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;

        // Calculated properties
        public decimal Percentage => TotalScore > 0 ? (Score / TotalScore) * 100 : 0;
    }
}
