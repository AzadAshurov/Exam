
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Areas.Admin.ViewModels.Profession
{
    public class GetProfessionVM
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public int Id { get; set; }
        
    }
}
