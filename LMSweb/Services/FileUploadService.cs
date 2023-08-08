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
            string pathFile = await SaveFile(uploaded_file, "uploadfiles");
            var students = ReadDataFromFile(pathFile);
            return students.Skip(1);
        }

        private static IEnumerable<Student> ReadDataFromFile(string filepath)
        {
            var reader = new StreamReader(filepath);
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) break;
                var values = line.Split(',');
                if (string.IsNullOrEmpty(values[0]) && string.IsNullOrEmpty(values[1]) && string.IsNullOrEmpty(values[2])) break;
                else
                {
                    yield return new Student
                    {
                        StudentId = values[0],
                        StudentName = values[1],
                        StudentSex = values[2]
                    };
                }
            }
        }

        public async Task<string> SaveFile(IFormFile uploaded_file, string directory)
        {
            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", directory, uploaded_file.FileName);
            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                await uploaded_file.CopyToAsync(stream);
            }

            return pathFile;
        }
    }
}
