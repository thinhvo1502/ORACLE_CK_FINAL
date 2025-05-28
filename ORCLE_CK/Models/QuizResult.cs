using System;
using System.Collections.Generic;
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
        public DateTime TakenAt { get; set; } = DateTime.Now;
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int TimeTaken { get; set; } // in minutes

        // Navigation properties
        public string UserFullName { get; set; } = string.Empty;
        public string QuizTitle { get; set; } = string.Empty;
    }
}
