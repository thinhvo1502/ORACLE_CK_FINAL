using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Submission
    {
        public int SubmissionId { get; set; }

        [Required]
        public int AssignmentId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public string? Content { get; set; }

        public string? FilePath { get; set; }

        public DateTime SubmittedAt { get; set; }

        public DateTime? GradedAt { get; set; }

        public int? Score { get; set; }

        public string? Feedback { get; set; }

        public string Status { get; set; } = "Submitted"; // Submitted, Graded, Late

        // Navigation properties
        public string AssignmentTitle { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int MaxScore { get; set; }
    }
}
