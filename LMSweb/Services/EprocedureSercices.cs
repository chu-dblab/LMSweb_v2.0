using LMSweb.Data;
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
                output.Questions = new List<Question>();

                foreach (var question in Questions)
                {
                    var questionOutput = new Question();

                    questionOutput.QuestionId = question.QuestionId ?? "";
                    questionOutput.Content = question.Qcontent ?? "";
                    questionOutput.Type = question.Qtype ?? "";
                    questionOutput.Options = new List<Option>();
                    var options = _context.Options.Where(x => x.QuestionId == question.QuestionId).ToList();

                    // 如果是單選或是多選題，就把選項加進去
                    if (questionOutput.Type == "0" || questionOutput.Type == "1")
                    {
                        foreach (var option in options)
                        {
                            var optionOutput = new Option();

                            optionOutput.OptionId = option.OptionID.ToString() ?? "";
                            optionOutput.Content = option.Ocontent ?? "";

                            questionOutput.Options.Add(optionOutput);
                        }
                    }
                    else
                    {
                        var optionOutput = new Option();

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
            EprocedureIdTable[5, 2] = "6";
            EprocedureIdTable[5, 2] = "7";
            EprocedureIdTable[5, 2] = "5";

            return EprocedureIdTable[TaskType, TaskSteps];
        }

        public void SaveAnswer(PostViewModel postViewModel)
        {
            var uid = _context.Users.Find(postViewModel.UID);

            if (uid != null)
            {
                foreach (var answer in postViewModel.Answers)
                {
                    var question = _context.Questions.Find(answer.QuestionId);

                    if (question != null)
                    {
                        var _answer = new Models.Answer();

                        var aid_str = @$"{answer.QuestionId}{DateTime.Now:yyyymmddhhmmss}";

                        _answer.Aid = aid_str;
                        _answer.Acontent = $@"{answer.Content.OptionId},{answer.Content.OcontentContent}";
                        _answer.Atime = DateTime.Now;
                        _answer.QuestionId = answer.QuestionId;

                        _context.Answers.Add(_answer);
                        _context.SaveChanges();

                        var _provided = new Models.Provided()
                        {
                            AnswerId = aid_str,
                            MissionId = postViewModel.MissionId,
                            UserId = postViewModel.UID
                        };

                        _context.Provideds.Add(_provided);
                        _context.SaveChanges();
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}
