using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Provided
    {
        [Column(Order = 0)]
        public string AnswerId { get; set; } = null!;
        [Column(Order = 1)]
        public string UserId { get; set; } = null!;
        public string MissionId { get; set; } = null!;
        
        public virtual Answer Answer { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Mission Mission { get; set; } = null!;
        
    }
}
