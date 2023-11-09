using LMSweb.Data;
using LMSweb.Services;
using LMSweb.ViewModels;
using LMSweb.ViewModels.Coaching;
using LMSweb.ViewModels.Evaluation;
using LMSweb.ViewModels.Questionnaire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSweb.Controllers
{
    [Authorize]
    public class LearningProcessController : Controller
    {
        private readonly LMSContext _context;
        private readonly EvaluationCoachingServices _evaluationCoachingServices;

        public LearningProcessController(LMSContext context, EvaluationCoachingServices evaluationCoachingServices)
        {
            _context = context;
            _evaluationCoachingServices = evaluationCoachingServices;
        }

        public IActionResult Index(string mid, string? leader)
        {
            var uid = User.Claims.FirstOrDefault(x => x.Type == "UID").Value;
            if (leader != null)
                uid = leader;

            var viewModel = new LearningProcessViewModel
            {
                CourseId = _context.Missions.Find(mid).CourseId,
                MissionId = mid,
                CourseAgv = new ChartData
                {
                    Data = new double[] { 0, 0, 0 }
                },
                GroupAgv = new ChartData
                {
                    Data = new double[] { 0, 0, 0 }
                },
                TeacherAgv = new ChartData
                {
                    Data = new double[] { 0, 0, 0 }
                },
                Detail = new List<DetailData>()
            };

            var _Coaching = new Coaching();

            if (mid != null && uid != null)
                _Coaching = _evaluationCoachingServices.GetCoaching(mid, uid);

            if (_Coaching != null)
            {
                viewModel.CourseAgv = new ChartData
                {
                    Data = new double[] { _Coaching.ClassAgv.PE01, _Coaching.ClassAgv.PE02, _Coaching.ClassAgv.PE03 }
                };

                viewModel.GroupAgv = new ChartData
                {
                    Data = new double[] { _Coaching.GroupAgv.PE01, _Coaching.GroupAgv.PE02, _Coaching.GroupAgv.PE03 }
                };
            }

            var testType = _context.Courses.Find(viewModel.CourseId).TestType;
            viewModel.TestType = testType;

            // 找到該課程老師
            var teacher = _context.Courses.Where(x => x.Cid == viewModel.CourseId).FirstOrDefault().TeacherId;
            // 找到老師評價
            var TeacherEvaluation = _context.EvaluationCoachings.Where(x => x.MissionId == mid && x.AUID == teacher).ToList();
            // 計算老師評價平均分數
            if (TeacherEvaluation.Count > 0)
            {
                var TeacherAgv = new double[] { 0, 0, 0 };
                foreach (var eva in TeacherEvaluation)
                {
                    if (eva.Evaluation == null)
                    {
                        TeacherAgv = new double[] { 0, 0, 0 };
                        break;
                    }
                    else
                    {
                        var score_list = eva.Evaluation.Split(',').ToList();
                        var EvaAnswer_list = new List<EvaAnswer>();

                        Dictionary<string, int> scoreDict = new Dictionary<string, int>();

                        scoreDict.Add("PE01", 0);
                        scoreDict.Add("PE02", 0);
                        scoreDict.Add("PE03", 0);

                        foreach (var item in score_list)
                        {
                            var score = item.Split(':').ToList();
                            if (scoreDict.ContainsKey(score[0]))
                            {
                                scoreDict[key: score[0]] = _evaluationCoachingServices.GetScore(int.Parse(score[1]));
                            }
                            else
                            {
                                if (score.Count() > 1 && score[1] != "")
                                {
                                    var question = _context.Questions.Find(score[0]).Qcontent;

                                    EvaAnswer_list.Add(new EvaAnswer() { Question = question, Answer = score[1] });
                                }
                            }
                        }

                        TeacherAgv[0] += scoreDict["PE01"];
                        TeacherAgv[1] += scoreDict["PE02"];
                        TeacherAgv[2] += scoreDict["PE03"];
                    }
                }

                TeacherAgv[0] /= TeacherEvaluation.Count;
                TeacherAgv[1] /= TeacherEvaluation.Count;
                TeacherAgv[2] /= TeacherEvaluation.Count;

                viewModel.TeacherAgv = new ChartData
                {
                    Data = TeacherAgv
                };
            }


            // 找到評價&回饋
            var EvaluationList = _context.EvaluationCoachings.Where(x => x.MissionId == mid && x.BUID == uid).ToList();
            var CoachingList = _context.EvaluationCoachings.Where(x => x.MissionId == mid && x.AUID == uid).ToList();

            // 顯示評價

            if (EvaluationList.Count > 0)
            {
                foreach (var eva in EvaluationList)
                {
                    var Teacher = _context.Teachers.Where(x => x.TeacherId == eva.AUID).FirstOrDefault();
                    if (Teacher != null && eva.Evaluation != null)
                    {
                        var vmDetailData = new DetailData();
                        var score_list = eva.Evaluation.Split(',').ToList();
                        var EvaAnswer_list = new List<EvaAnswer>();

                        Dictionary<string, int> scoreDict = new Dictionary<string, int>();

                        scoreDict.Add("PE01", 0);
                        scoreDict.Add("PE02", 0);
                        scoreDict.Add("PE03", 0);

                        foreach (var item in score_list)
                        {
                            var score = item.Split(':').ToList();
                            if (scoreDict.ContainsKey(score[0]))
                            {
                                scoreDict[key: score[0]] = _evaluationCoachingServices.GetScore(int.Parse(score[1]));
                            }
                            else
                            {
                                if (score.Count() > 1 && score[1] != "")
                                {
                                    var question = _context.Questions.Find(score[0]).Qcontent;

                                    EvaAnswer_list.Add(new EvaAnswer() { Question = question, Answer = score[1] });
                                }
                            }
                        }

                        vmDetailData.Score = new int[] { scoreDict["PE01"], scoreDict["PE02"], scoreDict["PE03"] };
                        vmDetailData.TestFeedback = EvaAnswer_list.Select(x => x.Answer).ToArray();

                        viewModel.Detail.Add(vmDetailData);
                    }
                }

                if (viewModel.Detail.Count == 0)
                {
                    var nullData = new DetailData
                    {
                        Score = new int[] { 0, 0, 0 },
                        TestFeedback = new string[] { }
                    };
                    viewModel.Detail.Add(nullData);
                }

                if (testType == 2 || testType == 4 || testType == 5)
                {

                    foreach (var eva in EvaluationList)
                    {
                        var Teacher = _context.Teachers.Find(eva.AUID);
                        if (Teacher == null && eva.Evaluation != null)
                        {
                            var vmDetailData = new DetailData();
                            var score_list = eva.Evaluation.Split(',').ToList();
                            var EvaAnswer_list = new List<EvaAnswer>();

                            Dictionary<string, int> scoreDict = new Dictionary<string, int>();

                            scoreDict.Add("PE01", 0);
                            scoreDict.Add("PE02", 0);
                            scoreDict.Add("PE03", 0);

                            foreach (var item in score_list)
                            {
                                var score = item.Split(':').ToList();
                                if (scoreDict.ContainsKey(score[0]))
                                {
                                    scoreDict[key: score[0]] = _evaluationCoachingServices.GetScore(int.Parse(score[1]));
                                }
                                else
                                {
                                    if (score.Count() > 1 && score[1] != "")
                                    {
                                        var question = _context.Questions.Find(score[0]).Qcontent;

                                        EvaAnswer_list.Add(new EvaAnswer() { Question = question, Answer = score[1] });
                                    }
                                }
                            }

                            vmDetailData.Score = new int[] { scoreDict["PE01"], scoreDict["PE02"], scoreDict["PE03"] };
                            vmDetailData.TestFeedback = EvaAnswer_list.Select(x => x.Answer).ToArray();

                            viewModel.Detail.Add(vmDetailData);
                        }
                    }
                    // 如果評價是 EvaluationList.Count == 0 放兩個空資料
                    for (int i = 0; i < 3 - viewModel.Detail.Count; i++)
                    {
                        var nullData = new DetailData
                        {
                            Score = new int[] { 0, 0, 0 },
                            TestFeedback = new string[] { }
                        };
                        viewModel.Detail.Add(nullData);
                    }
                }
            }

            if (testType == 4 || testType == 5)
            {
                // 顯示回饋
                if (CoachingList.Count > 0)
                {
                    foreach (var coa in CoachingList)
                    {
                        var Teacher = _context.Teachers.Find(coa.AUID);
                        if (Teacher == null && coa.Evaluation != null && coa.Coaching != null)
                        {
                            var vmDetailData = new DetailData();
                            var score_list = coa.Evaluation.Split(',').ToList();
                            var EvaAnswer_list = new List<EvaAnswer>();

                            Dictionary<string, int> scoreDict = new Dictionary<string, int>();

                            scoreDict.Add("PE01", 0);
                            scoreDict.Add("PE02", 0);
                            scoreDict.Add("PE03", 0);

                            foreach (var item in score_list)
                            {
                                var score = item.Split(':').ToList();
                                if (scoreDict.ContainsKey(score[0]))
                                {
                                    scoreDict[key: score[0]] = _evaluationCoachingServices.GetScore(int.Parse(score[1]));
                                }
                                else
                                {
                                    if (score.Count() > 1 && score[1] != "")
                                    {
                                        var question = _context.Questions.Find(score[0]).Qcontent;

                                        EvaAnswer_list.Add(new EvaAnswer() { Question = question, Answer = score[1] });
                                    }
                                }
                            }

                            vmDetailData.Score = new int[] { scoreDict["PE01"], scoreDict["PE02"], scoreDict["PE03"] };
                            vmDetailData.TestFeedback = EvaAnswer_list.Select(x => x.Answer).ToArray();

                            var score_coa_list = coa.Coaching.Split(',').ToList();
                            var CoaAnswer_list = new List<CoaAnswer>();

                            Dictionary<string, string> scoreDict2 = new Dictionary<string, string>();

                            scoreDict2.Add("PC01", "");

                            foreach (var score in score_coa_list)
                            {
                                var score_split = score.Split(':').ToList();

                                if (scoreDict2.ContainsKey(score_split[0]))
                                {
                                    scoreDict2[key: score_split[0]] = _context.Options.Where(x => x.OptionID == int.Parse(score_split[1])).FirstOrDefault().Ocontent;
                                }
                                else
                                {
                                    if (score_split.Count() > 1 && score_split[1] != "")
                                    {
                                        var question = _context.Questions.Find(score_split[0]).Qcontent;

                                        CoaAnswer_list.Add(new CoaAnswer() { Question = question, Answer = score_split[1] });
                                    }
                                }
                            }

                            vmDetailData.ReTestFeedback = new List<string> { scoreDict2["PC01"] };
                            vmDetailData.ReTestFeedback.AddRange(CoaAnswer_list.Select(x => x.Answer).ToArray());

                            viewModel.Detail.Add(vmDetailData);
                        }
                        else if (Teacher == null && coa.Evaluation != null && coa.Coaching == null)
                        {
                            var vmDetailData = new DetailData();
                            var score_list = coa.Evaluation.Split(',').ToList();
                            var EvaAnswer_list = new List<EvaAnswer>();

                            Dictionary<string, int> scoreDict = new Dictionary<string, int>();

                            scoreDict.Add("PE01", 0);
                            scoreDict.Add("PE02", 0);
                            scoreDict.Add("PE03", 0);

                            foreach (var item in score_list)
                            {
                                var score = item.Split(':').ToList();
                                if (scoreDict.ContainsKey(score[0]))
                                {
                                    scoreDict[key: score[0]] = _evaluationCoachingServices.GetScore(int.Parse(score[1]));
                                }
                                else
                                {
                                    if (score.Count() > 1 && score[1] != "")
                                    {
                                        var question = _context.Questions.Find(score[0]).Qcontent;

                                        EvaAnswer_list.Add(new EvaAnswer() { Question = question, Answer = score[1] });
                                    }
                                }
                            }

                            vmDetailData.Score = new int[] { scoreDict["PE01"], scoreDict["PE02"], scoreDict["PE03"] };
                            vmDetailData.TestFeedback = EvaAnswer_list.Select(x => x.Answer).ToArray();

                            viewModel.Detail.Add(vmDetailData);
                        }
                        else if (Teacher == null)
                        {
                            // 都沒有資料放nullData
                            var nullData = new DetailData
                            {
                                Score = new int[] { 0, 0, 0 },
                                TestFeedback = new string[] { },
                                ReTestFeedback = new List<string> { }
                            };
                            viewModel.Detail.Add(nullData);
                        }
                    }
                }

                // 如果回饋是 CoachingList.Count == 0 放兩個空資料
                for (int i = 0; i < 5 - viewModel.Detail.Count; i++)
                {
                    var nullData = new DetailData
                    {
                        Score = new int[] { 0, 0, 0 },
                        TestFeedback = new string[] { },
                        ReTestFeedback = new List<string> { }
                    };
                    viewModel.Detail.Add(nullData);
                }
            }


            return View(viewModel);
        }
    }
}
