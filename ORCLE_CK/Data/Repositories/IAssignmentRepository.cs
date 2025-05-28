using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public interface IAssignmentRepository
    {
        List<Assignment> GetAssignmentsByCourse(int courseId);
        Assignment GetAssignmentById(int assignmentId);
        bool CreateAssignment(Assignment assignment);
        bool UpdateAssignment(Assignment assignment);
        bool DeleteAssignment(int assignmentId);
        List<Submission> GetSubmissionsByAssignment(int assignmentId);
        bool GradeSubmission(int submissionId, int score, string feedback);
    }
}
