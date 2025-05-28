using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CourseId { get; set; }

        public string CertificateNumber { get; set; } = string.Empty;

        public DateTime IssuedDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string Status { get; set; } = "Active"; // Active, Revoked, Expired

        public string? FilePath { get; set; }

        // Navigation properties
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public DateTime CompletionDate { get; set; }
        public double FinalScore { get; set; }
    }
}
