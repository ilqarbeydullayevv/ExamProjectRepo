using ExamProject.Models.Base;

namespace ExamProject.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
