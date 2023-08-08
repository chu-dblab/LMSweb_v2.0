using LMSweb.ViewModels.StudentManagement;

namespace LMSweb.Services
{
    public class FileUploadService
    {
        public async Task<IEnumerable<Student>?> Upload(IFormFile uploaded_file)
        {
            if (uploaded_file == null || uploaded_file.Length == 0)
            {
                return null;
            }
            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploadfiles", uploaded_file.FileName);
            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
               await uploaded_file.CopyToAsync(stream);
            }

            var import_student = File.ReadAllLines(pathFile);
            var students = import_student.Skip(1).Select(x => new Student 
            { 
                StudentId = x.Split(',')[0], 
                StudentName = x.Split(',')[1],
                StudentSex = x.Split(',')[2],
            });
            if (students.Any(x => string.IsNullOrEmpty(x.StudentId) || string.IsNullOrEmpty(x.StudentName) || string.IsNullOrEmpty(x.StudentSex)))
            {
                return null;
            }
            return students;
        }
    }
}
