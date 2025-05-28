using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public interface IEnrollmentRepository
    {
        List<Enrollment> GetByStudentId(int studentId);
        List<Course> GetEnrolledCourses(int studentId);
        bool IsEnrolled(int studentId, int courseId);
        bool Create(Enrollment enrollment);
        bool UpdateProgress(int studentId, int courseId, decimal progress);
        Enrollment GetByStudentAndCourse(int studentId, int courseId);
        List<Enrollment> GetByCourseId(int courseId);
    }
}
