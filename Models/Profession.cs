namespace ExamProject.Models
{
    public class Profession : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //relation
        List<Employee> Employees { get; set; }
    }
}
