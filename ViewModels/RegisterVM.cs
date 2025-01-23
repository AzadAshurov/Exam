using System.ComponentModel.DataAnnotations;

namespace ExamProject.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(63)]
        public string Name { get; set; }
        [Required]
        [MaxLength(63)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(63)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(127)]
        public string Password { get; set; }
    }
        
}
