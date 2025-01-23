using System.ComponentModel.DataAnnotations;

namespace ExamProject.Areas.Admin.ViewModels.Employee
{
    public class GetEmployeeDetailsVM
    {
        [Required]
        [MaxLength(64)]
        public string FullName { get; set; }

        [Required]
        public string TwitterLink { get; set; }
        [Required]
        public string InstagramLink { get; set; }
        [Required]
        public string FacebookLink { get; set; }
        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
