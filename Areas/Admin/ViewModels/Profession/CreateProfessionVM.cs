using System.ComponentModel.DataAnnotations;

namespace ExamProject.Areas.Admin.ViewModels.Profession
{
    public class CreateProfessionVM
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
    }
}
