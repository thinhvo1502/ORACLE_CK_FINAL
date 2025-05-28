using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public interface ILessonRepository
    {
        List<Lesson> GetLessonsByCourse(int courseId);
        Lesson GetLessonById(int lessonId);
        bool CreateLesson(Lesson lesson);
        bool UpdateLesson(Lesson lesson);
        bool DeleteLesson(int lessonId);
        int GetNextOrderNumber(int courseId);
        bool MoveLessonUp(int lessonId);
        bool MoveLessonDown(int lessonId);
    }
}
