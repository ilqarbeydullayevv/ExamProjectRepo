using ExamProject.Models.Base;

namespace ExamProject.Models
{
    public class Member:BaseEntity
    {
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public string Imgurl { get; set; }
    }
}
