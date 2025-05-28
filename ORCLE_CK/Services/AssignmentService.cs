using ORCLE_CK.Exceptions;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using ORCLE_CK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = ORCLE_CK.Exceptions.ValidationException;
namespace ORCLE_CK.Services
{
    public class AssignmentService
    {
        private readonly IAssignmentRepository assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            this.assignmentRepository = assignmentRepository;
        }

        public AssignmentService() : this(new AssignmentRepository())
        {
        }

        public List<Assignment> GetAssignmentsByCourse(int courseId)
        {
            try
            {
                return assignmentRepository.GetAssignmentsByCourse(courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetAssignmentsByCourse: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách bài tập", ex);
            }
        }

        public Assignment GetAssignmentById(int assignmentId)
        {
            if (assignmentId <= 0)
                throw new ArgumentException("Assignment ID phải lớn hơn 0");

            try
            {
                return assignmentRepository.GetAssignmentById(assignmentId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetAssignmentById: {ex.Message}", ex);
                throw new ServiceException("Không thể tải thông tin bài tập", ex);
            }
        }

        public bool CreateAssignment(Assignment assignment)
        {
            ValidateAssignment(assignment);

            try
            {
                return assignmentRepository.CreateAssignment(assignment);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in CreateAssignment: {ex.Message}", ex);
                throw new ServiceException("Không thể tạo bài tập", ex);
            }
        }

        public bool UpdateAssignment(Assignment assignment)
        {
            ValidateAssignment(assignment);

            try
            {
                return assignmentRepository.UpdateAssignment(assignment);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in UpdateAssignment: {ex.Message}", ex);
                throw new ServiceException("Không thể cập nhật bài tập", ex);
            }
        }

        public bool DeleteAssignment(int assignmentId)
        {
            if (assignmentId <= 0)
                throw new ArgumentException("Assignment ID phải lớn hơn 0");

            try
            {
                return assignmentRepository.DeleteAssignment(assignmentId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in DeleteAssignment: {ex.Message}", ex);
                throw new ServiceException("Không thể xóa bài tập", ex);
            }
        }

        public List<Submission> GetSubmissionsByAssignment(int assignmentId)
        {
            try
            {
                return assignmentRepository.GetSubmissionsByAssignment(assignmentId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetSubmissionsByAssignment: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách bài nộp", ex);
            }
        }

        public bool GradeSubmission(int submissionId, int score, string feedback)
        {
            try
            {
                return assignmentRepository.GradeSubmission(submissionId, score, feedback);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GradeSubmission: {ex.Message}", ex);
                throw new ServiceException("Không thể chấm điểm bài nộp", ex);
            }
        }

        private static void ValidateAssignment(Assignment assignment)
        {
            if (assignment == null)
                throw new ArgumentNullException(nameof(assignment));

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(assignment);

            if (!Validator.TryValidateObject(assignment, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Dữ liệu không hợp lệ: {errors}");
            }
        }
    }
}
