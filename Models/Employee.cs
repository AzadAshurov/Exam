namespace ExamProject.Models
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string ImageUrl { get; set; }
        //Relation
       public   Profession Profession { get; set; }
        public int ProfessionId {  get; set; }
    }
}
