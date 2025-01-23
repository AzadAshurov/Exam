using ExamProject.Models;
using ExamProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Try Again");
                return View(registerVM);
            }
            AppUser user = new AppUser()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
                Surname = registerVM.Surname
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, registerVM.Password);
            //if (identityResult == null)
            //{
            //    foreach (var error in identityResult.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description.ToString());
            //        return View();
            //    }
            //}
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Try Again");
                return View(loginVM);
            }
            AppUser appUser = loginVM.UserNameOrEmail.Contains("@") ? await _userManager.Users.FirstAsync(x => x.Email == loginVM.UserNameOrEmail) : await _userManager.Users.FirstAsync(x => x.UserName == loginVM.UserNameOrEmail);
            if (appUser == null)
            {
                ModelState.AddModelError("", "UserName , Email or password is wrong");
                return View(loginVM);
            }
            var result = _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.IsPersistent, true).Result;
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Try again later");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName , Email or password is wrong");
                return View();
            }


            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRoles()
        {
           await _roleManager.CreateAsync(new IdentityRole("Member"));
            return RedirectToAction("Index", "Home");
        }
    }
}