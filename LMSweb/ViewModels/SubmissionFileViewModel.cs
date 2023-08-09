namespace LMSweb.ViewModels
{
    public class SubmissionFileViewModel
    {
        public string? CourseId { get; set; }
        public string CourseName { get; set; }
        public string MisstionId { get; set; }
        public string MisstionName { get; set; }
        public DateTime EndDate { get; set; }

        public string? type { get; set; }
        public string? Path { get; set; }

        public IFormFile? formFile { get; set; }
    }
}
