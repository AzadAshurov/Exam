using ExamProject.Areas.Admin.ViewModels.Employee;
using ExamProject.DAL;
using ExamProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ExamProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetEmployeeVM> list = await _context.Employees.Select(x => new GetEmployeeVM
            {
                FullName = x.FullName,
              
                Id = x.Id
            }).ToListAsync();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(int? Id, CreateEmployeeVM EmployeeVM)
        {
            if (!ModelState.IsValid)
            {

                return View(EmployeeVM);
            }

            if (await _context.Employees.AnyAsync(x => x.FullName == EmployeeVM.FullName))
            {
                ModelState.AddModelError("Name", "Already exists");
            }
            using (var stream = System.IO.File.Create("~/assets/img"))
            {
                await EmployeeVM.Image.CopyToAsync(stream);
            }
            Employee Employee = new Employee()
            {
                FullName= EmployeeVM.FullName,
                FacebookLink = EmployeeVM.FacebookLink,
                TwitterLink = EmployeeVM.TwitterLink,
                InstagramLink = EmployeeVM.InstagramLink,
                ImageUrl = "~/assets/img" + $"{EmployeeVM.Image.FileName}"

            };
            await _context.Employees.AddAsync(Employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id <= 0 || Id is null)
            {
                return BadRequest();
            }
            Employee Employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (Employee == null)
            {
                return NotFound();
            }
            UpdateEmployeeVM EmployeeVM = new UpdateEmployeeVM()
            {
                FullName = Employee.FullName,
                FacebookLink = Employee.FacebookLink,
                TwitterLink = Employee.TwitterLink,
                InstagramLink = Employee.InstagramLink,
                ImageUrl = Employee.ImageUrl
            };

            return View(EmployeeVM);

        }
        public async Task<IActionResult> Update(int? Id, UpdateEmployeeVM EmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(EmployeeVM);
            }
            Employee Employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == EmployeeVM.Id);
            if (Employee == null)
            {
                return NotFound();
            }
            using (var stream = System.IO.File.Create("~/assets/img"))
            {
                await EmployeeVM.Image.CopyToAsync(stream);
            }

            Employee.FullName = EmployeeVM.FullName;
            Employee.FacebookLink = EmployeeVM.FacebookLink;
            Employee.TwitterLink = EmployeeVM.TwitterLink;
            Employee.InstagramLink = EmployeeVM.InstagramLink;
            Employee.ImageUrl = "~/assets/img" + $"{EmployeeVM.Image.FileName}";

           
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id <= 0 || Id is null)
            {
                return BadRequest();
            }
            Employee Employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (Employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(Employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? Id)
        {

            if (Id <= 0 || Id is null)
            {
                return BadRequest();
            }
            Employee Employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (Employee is null)
            {
                return NotFound();
            }
            GetEmployeeDetailsVM getEmployeeDetailsVM = new GetEmployeeDetailsVM
            {
                FullName = Employee.FullName,
                FacebookLink = Employee.FacebookLink,
                TwitterLink = Employee.TwitterLink,
                InstagramLink = Employee.InstagramLink,       
            };
            return View();
        }
    }
}
