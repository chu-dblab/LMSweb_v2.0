using LMSweb.Data;
using LMSweb.Models;
using LMSweb.ViewModels.Questionnaire;

namespace LMSweb.Services
{
    public class EvaluationCoachingServices
    {
        private readonly LMSContext _context;
        private readonly CoachingScore NoSumitOutput;

        public EvaluationCoachingServices(LMSContext context)
        {
            _context = context;

            NoSumitOutput = new CoachingScore()
            {
                PE01 = 0,
                PE02 = 0,
                PE03 = 0,
            };
        }

        public void SetEvaluationCoaching(string mid)
        {
            // 將此課程互評參照表初始化設定
            var LeaderList = (from m in _context.Missions
                              from e in _context.Executions
                              from s in _context.Students
                              where m.Mid == mid && m.Mid == e.MissionId && e.GroupId == s.GroupId && s.IsLeader == true
                              select s.StudentId).ToList();

            // 每組隨機分配 2 個互評對象
            var random = new Random();
            int A_dash = random.Next(1, LeaderList.Count);
            int B_dash = random.Next(1, LeaderList.Count);
            while(A_dash == B_dash)
            {
                B_dash = random.Next(1, LeaderList.Count);
            }

            foreach (var leader in LeaderList)
            {
                var LeaderGroupCount = LeaderList.Count;

                var LeaderGroupListRandom = new List<string>()
                {
                    LeaderList[A_dash++],
                    LeaderList[B_dash++],
                };

                if (A_dash >= LeaderGroupCount)
                    A_dash = 0;

                if (B_dash >= LeaderGroupCount)
                    B_dash = 0;

                var evaluationCoaching = new EvaluationCoaching();
                foreach (var studentId in LeaderGroupListRandom)
                {
                    evaluationCoaching.MissionId = mid;
                    evaluationCoaching.AUID = leader;
                    evaluationCoaching.BUID = studentId;

                    _context.EvaluationCoachings.Add(evaluationCoaching);
                    _context.SaveChanges();
                }
            }

            // 初始化老師批改(評價)欄位
            var tid = (from m in _context.Missions
                       from c in _context.Courses
                       where m.Mid == mid && m.CourseId == c.Cid
                       select c.TeacherId).FirstOrDefault();

            foreach (var leader in LeaderList)
            {
                var evaluationCoaching = new EvaluationCoaching();
                evaluationCoaching.MissionId = mid;
                evaluationCoaching.AUID = tid;
                evaluationCoaching.BUID = leader;

                _context.EvaluationCoachings.Add(evaluationCoaching);
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

        public void SaveAnswerByEvaluation(PostViewModel postViewModel)
        {
            var uid = _context.Users.Find(postViewModel.UID);
            if (uid != null && postViewModel.UID != null && postViewModel.PostUid != null)
            {
                var _EvaluationCoaching = _context.EvaluationCoachings.Where(x => x.AUID == postViewModel.UID && x.BUID == postViewModel.PostUid && x.MissionId == postViewModel.MissionId).FirstOrDefault();
                if (_EvaluationCoaching != null)
                {
                    var saveString = "";

                    foreach (var answer in postViewModel.Answers)
                    {
                        foreach (var option in answer.Content)
                        {
                            var question = _context.Questions.Find(answer.QuestionId);

                            if (question.Qtype is "0" or "1")
                            {
                                saveString += $@"{answer.QuestionId}:{option.OptionId}";
                            }
                            else
                            {
                                saveString += $@"{answer.QuestionId}:{option.OcontentContent}";
                            }
                            saveString += ",";
                        }
                    }

                    _EvaluationCoaching.Evaluation = saveString;
                }

                _context.SaveChanges();
            }
        }

        public List<string> GetCoachingLeaderList(string mid, string uid)
        {
            var LeaderGroupList = (from ec in _context.EvaluationCoachings
                                   join user in _context.Students on ec.AUID equals user.StudentId
                                   where ec.MissionId == mid && ec.BUID == uid
                                   select ec.AUID).ToList();
            return LeaderGroupList;
        }

        public CoachingScore GetClassAgv(string mid)
        {
            var ClassScore = (from ec in _context.EvaluationCoachings
                              join user in _context.Students on ec.AUID equals user.StudentId
                              where ec.MissionId == mid
                              select ec.Evaluation).ToList();



            Dictionary<string, int> scoreDict = new Dictionary<string, int>();

            scoreDict.Add("PE01", 0);
            scoreDict.Add("PE02", 0);
            scoreDict.Add("PE03", 0);

            foreach (var item in ClassScore)
            {
                if (item == null)
                    return NoSumitOutput;

                var score = item.Split(',').ToList();
                var scoreList = new List<int>();

                foreach (var s in score)
                {
                    var scoreSplit = s.Split(':').ToList();
                    if (scoreDict.ContainsKey(scoreSplit[0]))
                    {
                        scoreDict[key: scoreSplit[0]] += GetScore(int.Parse(scoreSplit[1]));
                    }
                }
            }

            var ClassAgv = new CoachingScore();
            ClassAgv.PE01 = scoreDict["PE01"] / ClassScore.Count;
            ClassAgv.PE02 = scoreDict["PE02"] / ClassScore.Count;
            ClassAgv.PE03 = scoreDict["PE03"] / ClassScore.Count;

            return ClassAgv;
        }

        public CoachingGroup GetCoachingGroup(string mid, string buid, string auid)
        {
            var group = new CoachingGroup();
            group.Evaluation = new List<string>();

            var group_score = (from ec in _context.EvaluationCoachings
                               where ec.MissionId == mid && ec.BUID == buid && ec.AUID == auid
                               select new
                               {
                                   ec.Evaluation,
                                   ec.Coaching
                               }).FirstOrDefault();

            if (group_score.Evaluation == null)
            {
                group.CoachingScore = NoSumitOutput;
                group.IsSubmit = false;

                return group;
            }


            if (group_score.Coaching != null)
                group.IsSubmit = true;

            var group_score_list = group_score.Evaluation.Split(',').ToList();

            Dictionary<string, int> scoreDict = new Dictionary<string, int>();

            scoreDict.Add("PE01", 0);
            scoreDict.Add("PE02", 0);
            scoreDict.Add("PE03", 0);

            foreach (var item in group_score_list)
            {
                var score = item.Split(':').ToList();
                if (scoreDict.ContainsKey(score[0]))
                {
                    scoreDict[key: score[0]] = GetScore(int.Parse(score[1]));
                }
                else
                {
                    if (score.Count() > 1)
                        group.Evaluation.Add(score[1]);
                }
            }

            group.CoachingScore = new CoachingScore();
            group.CoachingScore.PE01 = scoreDict["PE01"];
            group.CoachingScore.PE02 = scoreDict["PE02"];
            group.CoachingScore.PE03 = scoreDict["PE03"];

            group.GroupLeaderId = auid;

            return group;
        }

        public CoachingScore GetCoachingGroupAgv(string mid, string buid)
        {
            var GroupScore = (from ec in _context.EvaluationCoachings
                              join user in _context.Students on ec.AUID equals user.StudentId
                              where ec.MissionId == mid && ec.BUID == buid
                              select ec.Evaluation).ToList();

            Dictionary<string, int> scoreDict = new Dictionary<string, int>();

            scoreDict.Add("PE01", 0);
            scoreDict.Add("PE02", 0);
            scoreDict.Add("PE03", 0);

            foreach (var item in GroupScore)
            {
                if (item == null)
                    return NoSumitOutput;

                var score = item.Split(',').ToList();
                var scoreList = new List<int>();

                foreach (var s in score)
                {
                    var scoreSplit = s.Split(':').ToList();
                    if (scoreDict.ContainsKey(scoreSplit[0]))
                    {
                        scoreDict[key: scoreSplit[0]] += GetScore(int.Parse(scoreSplit[1]));
                    }
                }
            }

            var GroupAgv = new CoachingScore()
            {
                PE01 = scoreDict["PE01"] / GroupScore.Count,
                PE02 = scoreDict["PE02"] / GroupScore.Count,
                PE03 = scoreDict["PE03"] / GroupScore.Count
            };

            return GroupAgv;
        }

        public Coaching GetCoaching(string mid, string uid)
        {
            var output = new Coaching();
            output.Groups = new List<CoachingGroup>();

            var CoachingLeaderList = GetCoachingLeaderList(mid, uid);

            output.ClassAgv = GetClassAgv(mid);
            output.GroupAgv = GetCoachingGroupAgv(mid, uid);

            foreach (var leader in CoachingLeaderList)
            {
                var group = GetCoachingGroup(mid, uid, leader);
                output.Groups.Add(group);
            }

            return output;
        }

        public int GetScore(int optionId)
        {
            var score = 0;

            var option = _context.Options.Where(x => x.OptionID == optionId).FirstOrDefault();

            if (option != null)
            {
                score = int.Parse(option.Ocontent);
            }

            return score;
        }

        public void SaveAnswerByCoaching(PostViewModel postViewModel)
        {
            var uid = _context.Users.Find(postViewModel.UID);
            if (uid != null && postViewModel.UID != null && postViewModel.PostUid != null)
            {
                var _EvaluationCoaching = _context.EvaluationCoachings.Where(x => x.BUID == postViewModel.UID && x.AUID == postViewModel.PostUid && x.MissionId == postViewModel.MissionId).FirstOrDefault();
                if (_EvaluationCoaching != null)
                {
                    var saveString = "";

                    foreach (var answer in postViewModel.Answers)
                    {
                        foreach (var option in answer.Content)
                        {
                            var question = _context.Questions.Find(answer.QuestionId);

                            if (question.Qtype is "0" or "1")
                            {
                                saveString += $@"{answer.QuestionId}:{option.OptionId}";
                            }
                            else
                            {
                                saveString += $@"{answer.QuestionId}:{option.OcontentContent}";
                            }
                            saveString += ",";
                        }
                    }

                    _EvaluationCoaching.Coaching = saveString;
                }

                _context.SaveChanges();
            }
        }

        public bool HasEvaluationAtBUID(string uid, string buid,  string missionId)
        {
            var _EvaluationCoachingServices = new EvaluationCoachingServices(_context);
            
            var evaluation = _context.EvaluationCoachings.Where(x => x.MissionId == missionId && x.AUID == uid && x.BUID == buid).FirstOrDefault();
            if (evaluation == null || evaluation.Evaluation == null)
            {
                return false;
            }

            return true;
        }
    }
}
