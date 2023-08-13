using LMSweb.Data;
using LMSweb.Models;
using LMSweb.Views.StudentManagement;

namespace LMSweb.Services
{
    public class GroupServices
    {
        private readonly LMSContext _context;

        public GroupServices(LMSContext context)
        {
            _context = context;
        }

        public bool GetGroupRandomCreateVM(int n, string cid)
        {
            var GroupRandomCreateVM = new GroupRandomCreateViewModel();

            /* 獲取原本資料 */
            var course = _context.Courses.Find(cid);
            var student_list = _context.Students.Where(x => x.CourseId == cid).ToList();
            var student_has_group = student_list.Where(s => s.GroupId != null);
            var group_list = _context.Groups.Select(g => new ViewModels.StudentManagement.Group
            {
                GroupId = g.Gid,
                GroupName = g.Gname,
                Students = student_has_group.Where(s => s.GroupId == g.Gid)
                .Select(x => new ViewModels.StudentManagement.Student
                {
                    StudentId = x.StudentId,
                    StudentName = x.StudentName,
                    IsLeader = x.IsLeader
                }).OrderBy(x => x.StudentId).ToList()
            });

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
            var group = new Group();
            for (int i = 1; i <= n; i++)
            {
                var gid = Convert.ToInt32($"{random_number.ToString()}{i:D3}");
                GroupList.Add(gid);

                group = new Group
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

                    student_list[stu_index].GroupId = GroupList[GroupList_index++];

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
                    student_list.Where(s => s.GroupId == g).First().IsLeader = true;
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
    }
}
