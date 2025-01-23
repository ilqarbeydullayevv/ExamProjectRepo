using System.ComponentModel.DataAnnotations;

namespace ExamProject.ViewModel.Member
{
    public class UpdateMembervm
    {
        public int Id { get; set; }
        [Required, MaxLength(18, ErrorMessage = "maksimum uzunluq 18 ola biler"),
           MinLength(3, ErrorMessage = "min uzunluq 5 olmalidir")]
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public IFormFile Myfile { get; set; }
    }
}
