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
    public class LessonService
    {
        private readonly ILessonRepository lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        public LessonService() : this(new LessonRepository())
        {
        }

        public List<Lesson> GetLessonsByCourse(int courseId)
        {
            try
            {
                return lessonRepository.GetLessonsByCourse(courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetLessonsByCourse: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách bài học", ex);
            }
        }

        public Lesson GetLessonById(int lessonId)
        {
            if (lessonId <= 0)
                throw new ArgumentException("Lesson ID phải lớn hơn 0");

            try
            {
                return lessonRepository.GetLessonById(lessonId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetLessonById: {ex.Message}", ex);
                throw new ServiceException("Không thể tải thông tin bài học", ex);
            }
        }

        public bool CreateLesson(Lesson lesson)
        {
            ValidateLesson(lesson);

            try
            {
                // Set order number
                lesson.OrderNumber = lessonRepository.GetNextOrderNumber(lesson.CourseId);

                return lessonRepository.CreateLesson(lesson);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in CreateLesson: {ex.Message}", ex);
                throw new ServiceException("Không thể tạo bài học", ex);
            }
        }

        public bool UpdateLesson(Lesson lesson)
        {
            ValidateLesson(lesson);

            try
            {
                return lessonRepository.UpdateLesson(lesson);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in UpdateLesson: {ex.Message}", ex);
                throw new ServiceException("Không thể cập nhật bài học", ex);
            }
        }

        public bool DeleteLesson(int lessonId)
        {
            if (lessonId <= 0)
                throw new ArgumentException("Lesson ID phải lớn hơn 0");

            try
            {
                return lessonRepository.DeleteLesson(lessonId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in DeleteLesson: {ex.Message}", ex);
                throw new ServiceException("Không thể xóa bài học", ex);
            }
        }

        public bool MoveLessonUp(int lessonId)
        {
            try
            {
                return lessonRepository.MoveLessonUp(lessonId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in MoveLessonUp: {ex.Message}", ex);
                throw new ServiceException("Không thể di chuyển bài học", ex);
            }
        }

        public bool MoveLessonDown(int lessonId)
        {
            try
            {
                return lessonRepository.MoveLessonDown(lessonId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in MoveLessonDown: {ex.Message}", ex);
                throw new ServiceException("Không thể di chuyển bài học", ex);
            }
        }

        private static void ValidateLesson(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(lesson);

            if (!Validator.TryValidateObject(lesson, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Dữ liệu không hợp lệ: {errors}");
            }
        }
    }
}
