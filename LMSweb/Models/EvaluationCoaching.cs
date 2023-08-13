using Microsoft.EntityFrameworkCore;

namespace LMSweb.Models
{
    public class EvaluationCoaching
    {
        [Comment("給評價者編號")]
        public string AUID { get; set; } = null!;

        [Comment("接受評價者編號")]
        public string BUID { get; set; } = null!;

        public string MissionId { get; set; } = null!;

        public virtual User? AUser { get; set; }
        public virtual User? BUser { get; set; }
        public virtual Mission? Mission { get; set; }
    }
}
