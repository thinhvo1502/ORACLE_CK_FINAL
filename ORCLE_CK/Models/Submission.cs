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
        public int UserId { get; set; }

        public string? FileUrl { get; set; }

        public DateTime SubmittedAt { get; set; }

        public decimal? Grade { get; set; }

        public string? Feedback { get; set; }

        public string Status { get; set; } = "submitted"; // submitted, graded, returned

        // Navigation properties
        public string AssignmentTitle { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int MaxScore { get; set; }
    }
}
