using System.ComponentModel.DataAnnotations;

namespace ExamProject.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MaxLength(255)]
        public string UserNameOrEmail { get; set; }
        [Required]
        [MaxLength(127)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }


    }
}
