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
                var LeaderGroupId = (from s in _context.Students
                                     where s.StudentId == LeaderId
                                     select s.GroupId).FirstOrDefault();

                var LeaderGroupList = (from s in _context.Students
                                       where s.GroupId == LeaderGroupId
                                       select s.StudentId).ToList();

                var LeaderGroupCount = LeaderGroupList.Count;

                var LeaderGroupListRandom = new List<string>();

                for (int i = 0; i < 3; i++)
                {
                    var random = new Random();
                    var randomIndex = random.Next(0, LeaderGroupCount);
                    var randomStudentId = LeaderGroupList[randomIndex];

                    while (LeaderGroupListRandom.Contains(randomStudentId) || randomStudentId == LeaderId)
                    {
                        randomIndex = random.Next(0, LeaderGroupCount);
                        randomStudentId = LeaderGroupList[randomIndex];
                    }

                    LeaderGroupListRandom.Add(randomStudentId);
                }

                foreach (var studentId in LeaderGroupListRandom)
                {
                    var evaluationCoaching = new EvaluationCoaching
                    {
                        MissionId = mid,
                        AUID = LeaderId,
                        BUID = studentId,
                    };

                    _context.EvaluationCoachings.Add(evaluationCoaching);
                }
            }

            _context.SaveChanges();
        }
    }
}
