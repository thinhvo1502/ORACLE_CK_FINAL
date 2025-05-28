using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Tiêu đề quiz không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? TimeLimit { get; set; } // in minutes, nullable for unlimited time

        public int TotalScore { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties (không lưu trong DB, chỉ để hiển thị)
        public string CourseName { get; set; } = string.Empty;
        public int QuestionCount { get; set; }
        public int AttemptCount { get; set; }
    }
}
