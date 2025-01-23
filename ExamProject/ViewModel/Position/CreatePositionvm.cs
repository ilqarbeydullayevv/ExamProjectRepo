using System.ComponentModel.DataAnnotations;

namespace ExamProject.ViewModel.Position
{
    public record CreatePositionvm
    {
        [Required,MaxLength(10 , ErrorMessage ="maksimum uzunluq 10 ola biler") ,MinLength(3,ErrorMessage ="min uzunluq 3 olmalidir")]
        public string Name { get; set; }
    }
}
