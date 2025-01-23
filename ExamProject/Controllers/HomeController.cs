
using ExamProject.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamProject.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var members = _context.Members.Take(4).ToList();
            return View(members);
        }

       

    }
}
