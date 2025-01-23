using ExamProject.Areas.Admin.ViewModels.Profession;
using ExamProject.DAL;
using ExamProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfessionController : Controller
    {
        private readonly AppDbContext _context;

        public ProfessionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetProfessionVM> list = await _context.Professions.Select(x => new GetProfessionVM
            {
                Name = x.Name,
                Id = x.Id
            }).ToListAsync();
            return View(list);
        }


        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(int? Id, CreateProfessionVM professionVM)
        {
            if (!ModelState.IsValid)
            {

                return View(professionVM);
            }

            if (await _context.Professions.AnyAsync(x => x.Name == professionVM.Name))
            {
                ModelState.AddModelError("Name", "Already exists");
            }
            Profession profession = new Profession()
            {
                Name = professionVM.Name,
                Description = professionVM.Description,
            };
            await _context.Professions.AddAsync(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id <= 0 || Id is null)
            {
                return BadRequest();
            }
            Profession profession = await _context.Professions.FirstOrDefaultAsync(x => x.Id == Id);
            if (profession == null)
            {
                return NotFound();
            }
            UpdateProfessionVM professionVM = new UpdateProfessionVM()
            {
                Name = profession.Name,
                Description = profession.Description,
                Id = profession.Id
            };

            return View(professionVM);

        }
        public async Task<IActionResult> Update(int? Id, UpdateProfessionVM professionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(professionVM);
            }
            Profession profession = await _context.Professions.FirstOrDefaultAsync(x => x.Id == professionVM.Id);
            if (profession == null)
            {
                return NotFound();
            }
            profession.Name = professionVM.Name;
            profession.Description = professionVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id <= 0 || Id is null)
            {
                return BadRequest();
            }
            Profession profession = await _context.Professions.FirstOrDefaultAsync(x => x.Id == Id);
            if (profession == null)
            {
                return NotFound();
            }
            _context.Professions.Remove(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? Id)
        {

            if (Id <= 0 || Id is null)
            {
                return BadRequest();
            }
            Profession profession = await _context.Professions.FirstOrDefaultAsync(x => x.Id == Id);
            if (profession is null)
            {
                return NotFound();
            }
            GetProfessionDetailsVM getProfessionDetailsVM = new GetProfessionDetailsVM
            {
                Name = profession.Name,
                Description = profession.Description
            };
            return View();
        }
    }
}