using ExamProject.Helpers.Enums;
using ExamProject.Models;
using ExamProject.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ExamProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;

        public AccountController(UserManager<Appuser> userManager,SignInManager<Appuser>signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Registervm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = await _userManager.FindByNameAsync(vm.Username);
            if (user != null)
            {
                ModelState.AddModelError("", "bu username artb istifade olunub");
                return View(vm);
            }
            Appuser appuser = new Appuser()
            {
                 FullName = vm.Fullname,
                 Email = vm.Email,
                 UserName = vm.Username,
            };
           var result=await _userManager.CreateAsync(appuser,vm.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
               
                }
                return View();

            }
            return RedirectToAction("Login");
           
        }
        [HttpPost]
        public async Task<IActionResult> Login (Loginvm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = await _userManager.FindByNameAsync(vm.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "username or password invalid");
                return View(vm);
            }
            return View(vm);
            var result = await _userManager.CheckPasswordAsync(user, vm.Password);
            if (!result)
            {
                ModelState.AddModelError("", "username or password invalid");
                return View(vm);

            }
            var signinresut = await _signInManager.PasswordSignInAsync(user,vm.Password,false,false);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public IActionResult Createrole()
        {
            foreach(var item in Enum.GetValues(typeof(UserRoles)))
            {
            Name: item.ToString();
            }
            return RedirectToAction("Index", "Home");
        }
    }

}
