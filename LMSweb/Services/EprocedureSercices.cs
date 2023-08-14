using LMSweb.Data;
using LMSweb.Models;
using LMSweb.ViewModels.Questionnaire;

namespace LMSweb.Services
{
    /*
     * 這個類別是用來處理 Eprocedure 相關的資料庫存取 
     */

    public class EprocedureSercices
    {
        private readonly LMSContext _context;

        public EprocedureSercices(LMSContext context)
        {
            _context = context;
        }

        // 這個方法是用來取得某一個主題的題目內容
        public Topic? GetEprocedureContent(string eprocedureId)
        {
            var output = new Topic();

            var Eprocedure = _context.ExperimentalProcedures.Find(eprocedureId);

            if (Eprocedure != null)
            {
                var Questions = _context.Questions.Where(q => q.EprocedureId == eprocedureId).ToList();

                output.Name = Eprocedure.Name ?? "";
                output.Questions = new List<ViewModels.Questionnaire.Question>();

                foreach (var question in Questions)
                {
                    var questionOutput = new ViewModels.Questionnaire.Question();

                    questionOutput.QuestionId = question.QuestionId ?? "";
                    questionOutput.Content = question.Qcontent ?? "";
                    questionOutput.Type = question.Qtype ?? "";
                    questionOutput.Options = new List<ViewModels.Questionnaire.Option>();
                    var options = _context.Options.Where(x => x.QuestionId == question.QuestionId).ToList();

                    // 如果是單選或是多選題，就把選項加進去
                    if (questionOutput.Type == "0" || questionOutput.Type == "1")
                    {
                        foreach (var option in options)
                        {
                            var optionOutput = new ViewModels.Questionnaire.Option();

                            optionOutput.OptionId = option.OptionID.ToString() ?? "";
                            optionOutput.Content = option.Ocontent ?? "";

                            questionOutput.Options.Add(optionOutput);
                        }
                    }
                    else
                    {
                        var optionOutput = new ViewModels.Questionnaire.Option();

                        optionOutput.Content = "";

                        questionOutput.Options.Add(optionOutput);
                    }

                    output.Questions.Add(questionOutput);
                }

                return output;
            }
            else
            {
                return null;
            }
        }

        public string GetEprocedureId(int TaskType, int TaskSteps)
        {
            string[,] EprocedureIdTable = new string[6, 7];

            // 控制組
            EprocedureIdTable[0, 0] = "D";
            EprocedureIdTable[0, 1] = "C";
            // 實驗組一：自我調節
            EprocedureIdTable[1, 0] = "0";
            EprocedureIdTable[1, 1] = "D";
            EprocedureIdTable[1, 2] = "1";
            EprocedureIdTable[1, 3] = "C";
            EprocedureIdTable[1, 4] = "2";
            // 實驗組二：同儕互評
            EprocedureIdTable[2, 0] = "D";
            EprocedureIdTable[2, 1] = "C";
            EprocedureIdTable[2, 2] = "6";
            // 實驗組三：社會共享調節
            EprocedureIdTable[3, 0] = "3";
            EprocedureIdTable[3, 1] = "D";
            EprocedureIdTable[3, 2] = "4";
            EprocedureIdTable[3, 3] = "C";
            EprocedureIdTable[3, 4] = "5";
            // 實驗組四：互動式同儕互評
            EprocedureIdTable[4, 0] = "D";
            EprocedureIdTable[4, 1] = "C";
            EprocedureIdTable[4, 2] = "6";
            EprocedureIdTable[4, 3] = "7";
            // 實驗組五：社會共享調節 & 互動式同儕互評
            EprocedureIdTable[5, 0] = "3";
            EprocedureIdTable[5, 1] = "D";
            EprocedureIdTable[5, 2] = "4";
            EprocedureIdTable[5, 3] = "C";
            EprocedureIdTable[5, 4] = "6";
            EprocedureIdTable[5, 5] = "7";
            EprocedureIdTable[5, 6] = "5";

            return EprocedureIdTable[TaskType, TaskSteps];
        }

        public void SaveAnswer(PostViewModel postViewModel)
        {
            var uid = _context.Users.Find(postViewModel.UID);

            List<string> aidList = new List<string>();

            if (uid != null)
            {
                foreach (var answer in postViewModel.Answers)
                {
                    var question = _context.Questions.Find(answer.QuestionId);

                    if (question != null)
                    {
                        foreach (var option in answer.Content)
                        {
                            var _answer = new Models.Answer();

                            var aid_str = @$"{answer.QuestionId}{option.OptionId}{DateTime.Now:yyyyMMddHHmmss}";

                            _answer.Aid = aid_str;
                            _answer.Acontent = $@"{option.OptionId??""},{option.OcontentContent??""}";
                            _answer.Atime = DateTime.Now;
                            _answer.QuestionId = answer.QuestionId;

                            _context.Answers.Add(_answer);
                            _context.SaveChanges();

                            aidList.Add(aid_str);
                        }
                    }
                }

                var provided = new Provided();
                
                foreach (var aid in aidList)
                {
                    provided.AnswerId = aid;
                    provided.MissionId = postViewModel.MissionId;
                    provided.UserId = postViewModel.UID;

                    var p = _context.Provideds.Where(x => x.UserId == postViewModel.UID && x.MissionId == postViewModel.MissionId && x.AnswerId == aid).FirstOrDefault();
                    
                    if (p == null)
                    {
                        _context.Provideds.Add(provided);
                        _context.SaveChanges();
                    }
                }
                
            }
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

                    foreach(var answer in postViewModel.Answers)
                    {
                        foreach(var option in answer.Content)
                        {
                            var question = _context.Questions.Find(answer.QuestionId);

                            if(question.Qtype == "0" || question.Qtype == "1")
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

        // 判斷是否已經填寫過問卷
        public bool IsAnswered(string uid, string missionId, string eprocedureId)
        {
            var Questions = _context.Questions.Where(q => q.EprocedureId == eprocedureId).ToList();
            var provided = _context.Provideds.Where(x => x.UserId == uid && x.MissionId == missionId).ToList();

            if (provided.Count > 0)
            {
                foreach(var pro in provided)
                {
                    if(Questions.Any(x => pro.AnswerId.Contains(x.QuestionId) ))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        // 取得某人某一主題答覆
        public List<ViewModels.Questionnaire.Answer> GetAnswer(string uid, string missionId, string eprocedureId)
        {
            var Questions = _context.Questions.Where(q => q.EprocedureId == eprocedureId).ToList();
            var provided = _context.Provideds.Where(x => x.UserId == uid && x.MissionId == missionId).ToList();
            var output = new List<ViewModels.Questionnaire.Answer>();

            foreach (var que in Questions)
            {
                var answer = new ViewModels.Questionnaire.Answer();

                answer.QuestionId = que.QuestionId;
                answer.Qcontent = que.Qcontent;
                answer.Content = new List<AnswerContent>();

                foreach (var pro in provided)
                {
                    if ( pro.AnswerId.Contains(que.QuestionId) )
                    {
                        var ac = new AnswerContent();

                        var a = _context.Answers.Find(pro.AnswerId);
                        ac.OcontentContent = a.Acontent.Split(',')[1];

                        answer.Content.Add(ac);
                    }
                }

                output.Add(answer);
            }

            return output;
        }
    }
}
