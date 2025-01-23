using Microsoft.AspNetCore.Identity;

namespace ExamProject.Models
{
    public class Appuser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
