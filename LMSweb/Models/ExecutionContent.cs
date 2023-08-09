using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class ExecutionContent
    {
        [Column(Order = 0)]
        public string MissionId { get; set; }
        [Column(Order = 1)]
        public int GroupId { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }


        public virtual Mission Mission { get; set; }
        public virtual Group Group { get; set; }
    } 
}
