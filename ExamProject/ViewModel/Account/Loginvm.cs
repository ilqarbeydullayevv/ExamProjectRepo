using System.ComponentModel.DataAnnotations;

namespace ExamProject.ViewModel.Account
{
    public class Loginvm
    {
        [Required, MinLength(4, ErrorMessage = "minumum uzunluq 4 olmalidi")]
        public string Username { get; set; }
      
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
