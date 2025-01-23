using System.ComponentModel.DataAnnotations;

namespace ExamProject.ViewModel.Account
{
    public class Registervm
    {
        [Required, MaxLength(18, ErrorMessage = "maksimum uzunluq 18 ola biler"),
           MinLength(3, ErrorMessage = "min uzunluq 5 olmalidir")]
        public string Fullname { get; set; }
        [Required,MinLength(4,ErrorMessage ="minumum uzunluq 4 olmalidi")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
