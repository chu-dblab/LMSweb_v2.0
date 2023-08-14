using LMSweb.Data;
using LMSweb.Models;

namespace LMSweb.Services
{
    public class EvaluationCoachingServices
    {
        private readonly LMSContext _context;

        public EvaluationCoachingServices(LMSContext context)
        {
            _context = context;
        }

        public void SetEvaluationCoaching(string mid)
        {
            // 將此課程互評參照表初始化設定
            var LeaderList = (from m in _context.Missions
                              from e in _context.Executions
                              from s in _context.Students
                              where m.Mid == mid && m.Mid == e.MissionId && e.GroupId == s.GroupId && s.IsLeader == true
                              select new
                              {
                                  s.StudentId,
                              }).ToList();

            // 每組隨機分配 3 個互評對象
            foreach (var leader in LeaderList)
            {
                var LeaderId = leader.StudentId;

                var LeaderGroupList = LeaderList.ToList();

                var LeaderGroupCount = LeaderGroupList.Count;

                var LeaderGroupListRandom = new List<string>();

                for (int i = 0; i < 3; i++)
                {
                    var random = new Random();
                    var randomIndex = random.Next(0, LeaderGroupCount);
                    var randomStudentId = LeaderGroupList[randomIndex];

                    while (LeaderId == randomStudentId.StudentId || LeaderGroupListRandom.Exists(x => x == randomStudentId.StudentId))
                    {
                        randomIndex = random.Next(0, LeaderGroupCount);
                        randomStudentId = LeaderGroupList[randomIndex];
                    }

                    LeaderGroupListRandom.Add(randomStudentId.StudentId);
                }

                var evaluationCoaching = new EvaluationCoaching();
                foreach (var studentId in LeaderGroupListRandom)
                {
                    evaluationCoaching.MissionId = mid;
                    evaluationCoaching.AUID = LeaderId;
                    evaluationCoaching.BUID = studentId;
                    
                    _context.EvaluationCoachings.Add(evaluationCoaching);
                    _context.SaveChanges();
                }
            }

            _context.SaveChanges();
        }

        public List<string> GetEvaluationLeaderList(string mid, string uid)
        {
            var LeaderGroupList = (from ec in _context.EvaluationCoachings
                                   where ec.MissionId == mid && ec.AUID == uid
                                   select ec.BUID).ToList();
            return LeaderGroupList;
        }
    }
}
