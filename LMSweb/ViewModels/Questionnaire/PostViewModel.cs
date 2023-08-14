using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.Questionnaire
{
    public class PostViewModel
    {
        public string UID { get; set; }
        public string MissionId { get; set; }
        public List<Answer> Answers { get; set; }
        
        // 如果是互動式同儕互評，需要提供被評人的 UID 與步驟 ID
        public string? PostUid { get; set; }
        public string? EprocedureId { get; set; }
    }

    public class Answer
    {
        [Display(Name = "題目編號")]
        public string? QuestionId { get; set; }
        [Display(Name = "題目內容")]
        public string? Qcontent { get; set; }
        public List<AnswerContent> Content { get; set; }
    }

    public class AnswerContent
    {
        [Display(Name = "選項編號")]
        public string? OptionId { get; set; }
        [Display(Name = "選項內容")]
        public string? OcontentContent { get; set; }
    }
}
