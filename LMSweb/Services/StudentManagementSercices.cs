using LMSweb.Data;

namespace LMSweb.Services
{
    public class StudentManagementSercices
    {
        private readonly LMSContext _context;

        public StudentManagementSercices(LMSContext context)
        {
            _context = context;
        }

        // 判斷一個課程有沒有學生
        public bool HasStudentInCourse(string cid)
        {
            return _context.Students.Any(x => x.CourseId == cid);
        }

        // 判斷一個課程有沒有組別
        public bool HasGroupInCourse(string cid)
        {
            return _context.Groups.Any(x => x.CourseId == cid);
        }
    }
}
