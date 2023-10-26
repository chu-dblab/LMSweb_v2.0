using LMSweb.Data;
using LMSweb.Models;
using LMSweb.ViewModels.StudentManagement;

namespace LMSweb.Services
{
    public class GroupServices
    {
        private readonly LMSContext _context;

        public GroupServices(LMSContext context)
        {
            _context = context;
        }


        // 正常版隨機分組
        public bool doGroupRandom(int n, string cid)
        {
            var GroupRandomCreateVM = new GroupRandomCreateViewModel();

            /* 獲取原本資料 */
            var course = _context.Courses.Find(cid);
            var student_list = _context.Students.Where(x => x.CourseId == cid).ToList();

            // 產生隨機碼
            var random = new Random();
            var random_number = random.Next(1000, 10000);
            while (_context.Groups.Any(x => x.Gid == Convert.ToInt32($"{random_number.ToString()}{1:D3}")))
            {
                random_number = random.Next(1000, 10000);
            }

            // GroupList 存放 Gid
            var GroupList = new List<int>();

            // Gid 編碼規則：隨機數字 + 三位數字
            var group = new Models.Group();
            for (int i = 1; i <= n; i++)
            {
                var gid = Convert.ToInt32($"{random_number.ToString()}{i:D3}");
                GroupList.Add(gid);

                group = new Models.Group
                {
                    Gid = gid,
                    CourseId = cid,
                    Gname = $"第{i}組",
                };
                _context.Groups.Add(group);
                _context.SaveChanges();
            }

            // 將學生分組
            bool GoodGroup = false;
            while (!GoodGroup)
            {
                GoodGroup = true;
                int GroupList_index = 0;
                bool[] stu_HasGroup = new bool[student_list.Count()];
                for (int stu_index = 0; stu_index < student_list.Count(); stu_index++)
                {
                    var stu_random_index = random.Next(0, student_list.Count());
                    while (stu_HasGroup[stu_random_index])
                    {
                        stu_random_index = random.Next(0, student_list.Count());
                    }

                    student_list[stu_random_index].GroupId = GroupList[GroupList_index++];

                    if (GroupList_index == n)
                    {
                        GroupList_index = 0;
                    }

                    stu_HasGroup[stu_random_index] = true;
                }

                foreach (var g in GroupList)
                {
                    var g_stu_list = student_list.Where(s => s.GroupId == g).ToList();

                    int GroupCount = g_stu_list.Count();
                    int SexSoucec = 0;
                    foreach (var s in g_stu_list)
                    {
                        if (s.Sex == "男") SexSoucec += 1;
                        else SexSoucec += 2;
                    }
                    if (SexSoucec == (GroupCount + 1) || SexSoucec == (GroupCount * 2 - 1)) GoodGroup = false;
                }

                foreach (var g in GroupList)
                {
                    var temp = student_list.Where(s => s.GroupId == g).ToList();
                    temp.ForEach(s => { s.IsLeader = false; });
                    temp.First().IsLeader = true;
                }

                if (GoodGroup)
                {
                    foreach (var s in student_list)
                    {
                        _context.Students.Update(s);
                    }
                    _context.SaveChanges();
                }
            }

            return true;
        }

        // 進階版隨機分組-同性別分組 (這邊輸入的是一組幾個人)
        public bool doGroupRandom(int n, string cid, bool IsSameSex)
        {
            /* 獲取原本資料 */
            var course = _context.Courses.Find(cid);
            var student_list = _context.Students.Where(x => x.CourseId == cid).ToList();

            // 產生隨機碼
            var random = new Random();
            var random_number = random.Next(1000, 10000);
            while (_context.Groups.Any(x => x.Gid == Convert.ToInt32($"{random_number.ToString()}{1:D3}")))
            {
                random_number = random.Next(1000, 10000);
            }

            // GroupList 存放 Gid
            var GroupList = new List<int>();

            // 計算需要多少組別
            int MaleCount = student_list.Count(x => x.Sex == "男");
            int FemaleCount = student_list.Count() - MaleCount;
            int MaleGroupCount = MaleCount / n;
            int FemaleGroupCount = FemaleCount / n;
            int GroupCount = 0;

            if (MaleCount % n != 0) { MaleGroupCount += 1; }
            if (FemaleCount % n != 0) { FemaleGroupCount += 1; }
            //if (MaleCount % n == 1) { MaleGroupCount += 1; }
            //if (FemaleCount % n == 1) { FemaleGroupCount += 1; }

            GroupCount = MaleGroupCount + FemaleGroupCount;

            // Gid 編碼規則：隨機數字 + 三位數字
            var group = new Models.Group();
            for (int i = 1; i <= GroupCount; i++)
            {
                var gid = Convert.ToInt32($"{random_number.ToString()}{i:D3}");
                GroupList.Add(gid);

                group = new Models.Group
                {
                    Gid = gid,
                    CourseId = cid,
                    Gname = $"第{i}組",
                };
                _context.Groups.Add(group);
                _context.SaveChanges();
            }

            // 將學生分組
            bool GoodGroup = false;
            while (!GoodGroup)
            {
                GoodGroup = true;
                int GroupList_index = 0;
                int[] groupMaxPeople = new int[] { n, 2 };
                bool[] stu_HasGroup = new bool[student_list.Count()];
                string[] sex_string = new string[] { "男", "女" };
                

                // 男生先分組在換女生
                for (int i = 0; i < GroupCount; i++)
                {
                    int groupMaxPeople_index = 0;
                    if (MaleCount % n != 0 && i == MaleGroupCount - 1 - 1) groupMaxPeople_index = 1;
                    if (FemaleCount % n != 0 && i == MaleGroupCount + FemaleGroupCount - 1 - 1) groupMaxPeople_index = 1;
                    if (MaleCount % n == 1 && i == MaleGroupCount - 2 - 1) groupMaxPeople_index = 1;
                    if (FemaleCount % n == 1 && i == MaleGroupCount + FemaleGroupCount - 2 - 1) groupMaxPeople_index = 1;

                    for (int k = 0; k < groupMaxPeople[groupMaxPeople_index]; k++)
                    {
                        int sex_index = 0;
                        if (i >= MaleGroupCount) sex_index = 1;

                        var stu_random_index = random.Next(0, student_list.Count);
                        while (stu_HasGroup[stu_random_index] || student_list[stu_random_index].Sex != sex_string[sex_index])
                        {
                            stu_random_index = random.Next(0, student_list.Count);
                        }

                        student_list[stu_random_index].GroupId = GroupList[i];
                        if (k == 0) { student_list[stu_random_index].IsLeader = true; }
                        else { student_list[stu_random_index].IsLeader = false; }
                        stu_HasGroup[stu_random_index] = true;
                    }
                }

                _context.SaveChanges();
            }

            return true;
        }

        // 獲取一個隨機的新 GroupId
        public int GetNewGroupId()
        {
            var random = new Random();
            var random_number = random.Next(10000, 100000);
            while (_context.Groups.Any(x => x.Gid == Convert.ToInt32($"{random_number.ToString()}")))
            {
                random_number = random.Next(10000, 100000);
            }

            return random_number;
        }
    }
}
