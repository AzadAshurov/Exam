
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Areas.Admin.ViewModels.Employee
{
    public class GetEmployeeVM
    {
        [Required]
        [MaxLength(64)]
        public string FullName { get; set; }
        public int Id { get; set; }
        
    }
}
