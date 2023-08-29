using LMSweb.Assets;
using LMSweb.Data;
using LMSweb.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSweb.Services
{
    public class GuideForStudent
    {

        private string mid;
        private string cid;
        private string sid;

        private LMSContext db;

        private int TestType;
        private Mission MissionData;
        private List<Student> Leaders;

        private DateTime StartDate;
        private DateTime EndDate;

        private readonly EprocedureSercices _EprocedureSercices;
        private readonly EvaluationCoachingServices _EvaluationCoachingServices;

        public GuideForStudent(string mid, string cid, LMSContext db, EprocedureSercices eprocedureSercices, EvaluationCoachingServices evaluationCoachingServices)
        {
            this.mid = mid;
            this.cid = cid;

            this.db = db;

            TestType = db.Courses.Find(cid).TestType;
            MissionData = db.Missions.Find(mid);

            StartDate = MissionData.StartDate;
            EndDate = MissionData.EndDate;

            Leaders = (from e in db.Executions
                       join g in db.Groups on e.GroupId equals g.Gid
                       join s in db.Students on e.GroupId equals s.GroupId
                       where e.MissionId == mid && s.IsLeader == true
                       select s).ToList();

            _EprocedureSercices = eprocedureSercices;
            _EvaluationCoachingServices = evaluationCoachingServices;
        }
        public GuideForStudent(string mid, string cid, LMSContext db, EprocedureSercices eprocedureSercices, EvaluationCoachingServices evaluationCoachingServices, string sid) : this(mid, cid, db, eprocedureSercices, evaluationCoachingServices)
        {
            this.sid = sid;
        }
        // 幫我創建一個判斷目前步驟進度方法
        public void UpdateCurrentStatus()
        {
            switch (TestType)
            {
                case 0:
                    UpdateCurrentStatusForTestType0();
                    break;
                case 1:
                    UpdateCurrentStatusForTestType1();
                    break;
                case 2:
                    UpdateCurrentStatusForTestType2();
                    break;
                case 3:
                    UpdateCurrentStatusForTestType3();
                    break;
                case 4:
                    UpdateCurrentStatusForTestType4();
                    break;
                case 5:
                    UpdateCurrentStatusForTestType5();
                    break;
                default:
                    break;
            }
        }

        private void UpdateCurrentStatusForTestType0()
        {
            if (sid != null)
            {
                Student student;
                Execution execution;
                SetExecutionByCurrentStatus(out student, out execution);

                // 開啟第一步驟畫流程圖
                if (execution.CurrentStatus == "000")
                {
                    execution.CurrentStatus = "100";
                }

                // 判斷流程圖是否上傳
                else if (execution.CurrentStatus == "100")
                {
                    if (HasDraw((int)student.GroupId))
                    {
                        execution.CurrentStatus = "210";
                    }
                }

                // 判斷程式碼是否上傳
                else if (execution.CurrentStatus == "210")
                {
                    if (HasCode((int)student.GroupId))
                    {
                        execution.CurrentStatus = "221";
                    }
                }
                db.SaveChanges();
            }
        }

        private void UpdateCurrentStatusForTestType1()
        {
            if (sid != null)
            {
                Student student;
                Execution execution;
                SetExecutionByCurrentStatus(out student, out execution);

                // 開啟第一步驟畫流程圖
                if (execution.CurrentStatus == "000000")
                {
                    execution.CurrentStatus = "100000";
                }

                // 判斷目標設置是否填寫
                else if (execution.CurrentStatus == "100000")
                {
                    if (HasQuestion(student.StudentId, mid, "0"))
                    {
                        execution.CurrentStatus = "210000";
                    }
                }

                // 判斷流程圖是否上傳
                else if (execution.CurrentStatus == "210000")
                {
                    if (HasDraw((int)student.GroupId))
                    {
                        execution.CurrentStatus = "221000";
                    }
                }

                // 判斷任務監控是否填寫
                else if (execution.CurrentStatus == "221000")
                {
                    if (HasQuestion(student.StudentId, mid, "1"))
                    {
                        execution.CurrentStatus = "222100";
                    }
                }

                // 判斷程式碼是否上傳
                else if (execution.CurrentStatus == "222100")
                {
                    if (HasCode((int)student.GroupId))
                    {
                        execution.CurrentStatus = "222210";
                    }
                }

                // 判斷自我反思是否填寫
                else if (execution.CurrentStatus == "222210")
                {
                    if (HasQuestion(student.StudentId, mid, "2"))
                    {
                        execution.CurrentStatus = "222221";
                    }
                }
                db.SaveChanges();
            }
        }

        private void UpdateCurrentStatusForTestType2()
        {
            if (sid != null)
            {
                Student student;
                Execution execution;
                SetExecutionByCurrentStatus(out student, out execution);
                
                // 開啟第一步驟
                if (execution.CurrentStatus == "0000")
                {
                    execution.CurrentStatus = "1000";
                }

                else if (execution.CurrentStatus == "1000")
                {
                    if (HasDraw((int)student.GroupId))
                    {
                        execution.CurrentStatus = "2100";
                    }
                }

                else if (execution.CurrentStatus == "2100")
                {
                    if (HasCode((int)student.GroupId))
                    {
                        execution.CurrentStatus = "2210";
                    }
                }

                else if (execution.CurrentStatus == "2210")
                {
                    if (HasEvaluation(sid, mid))
                    {
                        execution.CurrentStatus = "2221";
                    }
                }
                db.SaveChanges();
            }
        }

        private void UpdateCurrentStatusForTestType3()
        {
            if (sid != null)
            {
                Student student;
                Execution execution;
                SetExecutionByCurrentStatus(out student, out execution);

                // 開啟第一步驟畫流程圖
                if (execution.CurrentStatus == "000000")
                {
                    execution.CurrentStatus = "100000";
                }

                // 判斷目標設置是否填寫
                else if (execution.CurrentStatus == "100000")
                {
                    if (HasQuestion(student.StudentId, mid, "3"))
                    {
                        execution.CurrentStatus = "210000";
                    }
                }

                // 判斷流程圖是否上傳
                else if (execution.CurrentStatus == "210000")
                {
                    if (HasDraw((int)student.GroupId))
                    {
                        execution.CurrentStatus = "221000";
                    }
                }

                // 判斷任務監控是否填寫
                else if (execution.CurrentStatus == "221000")
                {
                    if (HasQuestion(student.StudentId, mid, "4"))
                    {
                        execution.CurrentStatus = "222100";
                    }
                }

                // 判斷程式碼是否上傳
                else if (execution.CurrentStatus == "222100")
                {
                    if (HasCode((int)student.GroupId))
                    {
                        execution.CurrentStatus = "222210";
                    }
                }

                // 判斷自我反思是否填寫
                else if (execution.CurrentStatus == "222210")
                {
                    if (HasQuestion(student.StudentId, mid, "5"))
                    {
                        execution.CurrentStatus = "222221";
                    }
                }
                db.SaveChanges();
            }
        }

        private void UpdateCurrentStatusForTestType4()
        {
            if (sid != null)
            {
                Student student;
                Execution execution;
                SetExecutionByCurrentStatus(out student, out execution);

                // 開啟第一步驟
                if (execution.CurrentStatus == "00000")
                {
                    execution.CurrentStatus = "10000";
                }

                else if (execution.CurrentStatus == "10000")
                {
                    if (HasDraw((int)student.GroupId))
                    {
                        execution.CurrentStatus = "21000";
                    }
                }

                else if (execution.CurrentStatus == "21000")
                {
                    if (HasCode((int)student.GroupId))
                    {
                        execution.CurrentStatus = "22100";
                    }
                }

                else if (execution.CurrentStatus == "22100")
                {
                    if (HasEvaluation(sid, mid))
                    {
                        execution.CurrentStatus = "22210";
                    }
                }

                else if (execution.CurrentStatus == "22210")
                {
                    if (HasCoaching(sid, mid))
                    {
                        execution.CurrentStatus = "22221";
                    }
                }
                db.SaveChanges();
            }
        }

        private void UpdateCurrentStatusForTestType5()
        {
            if (sid != null)
            {
                Student student;
                Execution execution;
                SetExecutionByCurrentStatus(out student, out execution);

                // 開啟第一步驟畫流程圖
                if (execution.CurrentStatus == "00000000")
                {
                    execution.CurrentStatus = "10000000";
                }

                // 判斷目標設置是否填寫
                else if (execution.CurrentStatus == "10000000")
                {
                    if (HasQuestion(student.StudentId, mid, "3"))
                    {
                        execution.CurrentStatus = "21000000";
                    }
                }

                // 判斷流程圖是否上傳
                else if (execution.CurrentStatus == "21000000")
                {
                    if (HasDraw((int)student.GroupId))
                    {
                        execution.CurrentStatus = "22100000";
                    }
                }

                // 判斷任務監控是否填寫
                else if (execution.CurrentStatus == "22100000")
                {
                    if (HasQuestion(student.StudentId, mid, "4"))
                    {
                        execution.CurrentStatus = "22210000";
                    }
                }

                // 判斷程式碼是否上傳
                else if (execution.CurrentStatus == "22210000")
                {
                    if (HasCode((int)student.GroupId))
                    {
                        execution.CurrentStatus = "22221000";
                    }
                }

                else if (execution.CurrentStatus == "22221000")
                {
                    if (HasEvaluation(sid, mid))
                    {
                        execution.CurrentStatus = "22222100";
                    }
                }

                else if (execution.CurrentStatus == "22222100")
                {
                    if (HasCoaching(sid, mid))
                    {
                        execution.CurrentStatus = "22222210";
                    }
                }

                // 判斷反思是否填寫
                else if (execution.CurrentStatus == "22222210")
                {
                    if (HasQuestion(student.StudentId, mid, "5"))
                    {
                        execution.CurrentStatus = "22222221";
                    }
                }
                db.SaveChanges();
            }
        }

        // 這邊要做的事情是，把所有學生的CurrentStatus都設為00，代表任務尚未開始，做初始化。
        private void SetExecutionByCurrentStatus(out Student student, out Execution execution)
        {
            student = db.Students.Find(sid);
            execution = db.Executions.Find(student.GroupId, mid);
            if (execution == null)
            {
                var _execution = new Execution()
                {
                    GroupId = (int)student.GroupId,
                    MissionId = mid,
                    CurrentStatus = GlobalClass.DefaultCurrentStatus(TestType),
                };
                db.Executions.Add(_execution);
                db.SaveChanges();
                execution = db.Executions.Find(student.GroupId, mid);
            }
        }

        private bool HasDraw(int gid)
        {
            var Draw = db.ExecutionContents.Where(x => x.GroupId == gid && x.MissionId == mid && x.Type == "D").FirstOrDefault();
            if (Draw != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HasCode(int gid)
        {
            var Code = db.ExecutionContents.Where(x => x.GroupId == gid && x.MissionId == mid && x.Type == "C").FirstOrDefault();
            if (Code != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool HasQuestion(string uid, string missionId, string eprocedureId)
        {
            // 如果實驗組一要判對每一組員是否填寫問卷
            if (eprocedureId == "0" || eprocedureId == "1" || eprocedureId == "2")
            {
                bool output = true;

                var sid_list = db.Students.Where(x => x.GroupId == db.Students.Find(uid).GroupId).Select(x => x.StudentId).ToList();

                foreach (var sid in sid_list)
                {
                    if (!_EprocedureSercices.IsAnswered(sid, missionId, eprocedureId))
                    {
                        output = false;
                        break;
                    }
                }
                return output;

            } else
            {
                return _EprocedureSercices.IsAnswered(uid, missionId, eprocedureId);
            }
        }

        private bool HasEvaluation(string uid, string missionId)
        {
            var buid_list = _EvaluationCoachingServices.GetEvaluationLeaderList(missionId, uid);

            foreach (var buid in buid_list)
            {
                var evaluation = db.EvaluationCoachings.Where(x => x.MissionId == missionId && x.AUID == uid && x.BUID == buid).FirstOrDefault();
                if (evaluation == null || evaluation.Evaluation == null)
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasCoaching(string uid, string missionId)
        {
            var auid_list = _EvaluationCoachingServices.GetCoachingLeaderList(mid, uid);

            foreach (var auid in auid_list)
            {
                var evaluation = db.EvaluationCoachings.Where(x => x.MissionId == missionId && x.AUID == auid && x.BUID == uid).FirstOrDefault();
                if (evaluation == null || evaluation.Coaching == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}