namespace LMSweb.Assets
{
    public static class GlobalClass
    {
        public static string DefaultCurrentStatus(int type)
        {
            var table = new Dictionary<int, string>();
            table[0] = "000";
            table[1] = "000000";
            table[2] = "0000";
            table[3] = "000000";
            table[4] = "00000";
            table[5] = "00000000";
            return table[type];
        }
        public enum TaskSteps
        {
            /*
            目標設置    GoalSetting
            自我反思    SelfReflection
            團隊反思    TeamReflection
            學習監督    LearningSupervision
            同儕互評    PeerReview
            評價回饋    EvaluationFeedback
            */
            GoalSetting = 1,
            SelfReflection = 2,
            TeamReflection = 3,
            LearningSupervision = 4,
            PeerReview = 5,
            EvaluationFeedback = 6
        }

        public enum QuestionType
        {
            /*
            單選題    SingleChoice
            多選題    MultipleChoice
            問答題    EssayQuestion
            */
            SingleChoice = 0,
            MultipleChoice = 1,
            EssayQuestion = 2
        }

        public static string GetTypeString(int input)
        {
            string[] str =
            {
                "控制組",
                "實驗組一：自我調節",
                "實驗組二：同儕互評",
                "實驗組三：社會共享調節",
                "實驗組四：互動式同儕互評",
                "實驗組五：社會共享調節 & 互動式同儕互評",
                "組別Type錯誤"
            };

            return str[input];
        }

        public static List<string> GetTaskStepsString(int TaskType)
        {

            List<List<string>> TaskStepsTable = new List<List<string>>()
            {
                new List<string>() {"D", "C"},
                new List<string>() {"0", "D", "1", "C", "2"},
                new List<string>() {"D", "C", "6"},
                new List<string>() {"3", "D", "4", "C", "5"},
                new List<string>() {"D", "C", "6", "7"},
                new List<string>() {"3", "D", "4", "C", "6", "7", "5"},
            };

            return TaskStepsTable[TaskType];
        }
    }
}