using ORCLE_CK.Data.Repositories;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Services
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository enrollmentRepository;

        public EnrollmentService()
        {
            enrollmentRepository = new EnrollmentRepository();
        }

        public List<Enrollment> GetEnrollmentsByStudent(int studentId)
        {
            try
            {
                return enrollmentRepository.GetByStudentId(studentId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting enrollments for student {studentId}: {ex.Message}", ex);
                throw;
            }
        }

        public List<Course> GetEnrolledCourses(int studentId)
        {
            try
            {
                return enrollmentRepository.GetEnrolledCourses(studentId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting enrolled courses for student {studentId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool IsEnrolled(int studentId, int courseId)
        {
            try
            {
                return enrollmentRepository.IsEnrolled(studentId, courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error checking enrollment status for student {studentId} in course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool EnrollStudent(int studentId, int courseId)
        {
            try
            {
                // Check if already enrolled
                if (IsEnrolled(studentId, courseId))
                {
                    return false;
                }
                MessageBox.Show(studentId.ToString() + courseId.ToString());
                var enrollment = new Enrollment
                {
                    UserId = studentId,
                    CourseId = courseId,
                    EnrolledAt = DateTime.Now,
                    Status = EnrollmentStatus.Active,
                    Progress = 0
                };

                return enrollmentRepository.Create(enrollment);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error enrolling student {studentId} to course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateProgress(int studentId, int courseId, decimal progress)
        {
            try
            {
                return enrollmentRepository.UpdateProgress(studentId, courseId, progress);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating progress for student {studentId} in course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public Enrollment GetEnrollment(int studentId, int courseId)
        {
            try
            {
                return enrollmentRepository.GetByStudentAndCourse(studentId, courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting enrollment for student {studentId} and course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public List<Enrollment> GetEnrollmentsByCourse(int courseId)
        {
            try
            {
                return enrollmentRepository.GetByCourseId(courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting enrollments for course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool RemoveStudentFromCourse(int enrollmentId)
        {
            try
            {
                return enrollmentRepository.Delete(enrollmentId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error removing student from course: {ex.Message}", ex);
                throw;
            }
        }
    }
}