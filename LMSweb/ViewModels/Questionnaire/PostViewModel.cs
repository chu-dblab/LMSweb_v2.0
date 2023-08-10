using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModels.Questionnaire
{
    public class PostViewModel
    {
        public string UID { get; set; }
        public string MissionId { get; set; }
        public List<Answer> Answers { get; set; }
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
