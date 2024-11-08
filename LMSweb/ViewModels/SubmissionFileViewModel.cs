namespace LMSweb.ViewModels
{
    public class SubmissionFileViewModel
    {
        public string? CourseId { get; set; }
        public string CourseName { get; set; }
        public string MissionId { get; set; }
        public string MisstionName { get; set; }
        public DateTime EndDate { get; set; }

        public string? type { get; set; }
        public string? Path { get; set; }

        // 是否開放上傳功能
        public bool IsUpload { get; set; }

        public IFormFile? formFile { get; set; }
    }
}
