using ExamProject.DAL.Context;
using ExamProject.Models;
using ExamProject.ViewModel.Position;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            var positions = await _context.Positions.Include(x=>x.Members).ToListAsync();
          
            return View(positions);
        }
        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create (CreatePositionvm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var IsPositionExsist = await _context.Positions.FirstOrDefaultAsync(x=>x.Name==vm.Name);
            if(IsPositionExsist != null)
            {
                ModelState.AddModelError("Name", "Bu adda Position Artiq movcuddur");
                return View();    
            }
            Position position = new Position()
            {
                Name = vm.Name,
            };
           await _context.Positions.AddAsync(position);
           await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete (int? id)
        {
            if(id == null)
            {
                return View();
            }
            var position = await _context.Positions.FirstOrDefaultAsync(x=>x.Id==id); 
            if(position == null)
            {
                return View();
            }
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (position == null)
            {
                return View();
            }
            UpdatePositionvm vm = new UpdatePositionvm()
            {
                Id = position.Id,
                Name = position.Name,
            };
            return View(vm);    
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionvm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (vm.Id == null)
            {
                return View();
            }
            var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (position == null)
            {
                return View();
            }
            var IsPositionExsist = await _context.Positions.FirstOrDefaultAsync(x => x.Name == vm.Name);
            if (IsPositionExsist != null)
            {
                ModelState.AddModelError("Name", "Bu adda Position Artiq movcuddur");
                return View();
            }
            position.Id = vm.Id;
            position.Name = vm.Name;
       
            _context.Positions.Update(position);
          await  _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
