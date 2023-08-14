using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class ExecutionContent
    {
        public int Id { get; set; }

        public string MissionId { get; set; }
        public int GroupId { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }


        public virtual Mission Mission { get; set; }
        public virtual Group Group { get; set; }
    } 
}
