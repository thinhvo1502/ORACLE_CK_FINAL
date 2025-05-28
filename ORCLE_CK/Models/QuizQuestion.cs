using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class QuizQuestion
    {
        public int QuestionId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required(ErrorMessage = "Câu hỏi không được để trống")]
        public string Question { get; set; } = string.Empty;

        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }

        [RegularExpression("[A-D]", ErrorMessage = "Đáp án đúng phải là A, B, C hoặc D")]
        public string CorrectAnswer { get; set; } = "A";

        public int Points { get; set; } = 1;

        public int OrderNumber { get; set; }
    }
}
