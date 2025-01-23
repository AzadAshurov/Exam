
using ExamProject.DAL;
using ExamProject.Models;
using ExamProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context , IWebHostEnvironment env)
        {
           _context = context;
           _env = env;
        }
        public async Task <IActionResult> Index()
        {
            

            List<HomeVM> homeVM = await _context.Employees.Include(x => x.Profession).Select( x => 
                new HomeVM
                {
                   ProfessionName = x.Profession.Name,
                   TwitterLink = x.TwitterLink,
                   ImageUrl = x.ImageUrl,
                   InstagramLink = x.InstagramLink,
                   FacebookLink = x.FacebookLink,
                   FullName = x.FullName,
                }
                ).ToListAsync();
            return View(homeVM);

        }
 
    }
}
