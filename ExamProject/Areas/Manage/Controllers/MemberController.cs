using ExamProject.DAL.Context;
using ExamProject.Models;
using ExamProject.ViewModel.Member;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ExamProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MemberController : Controller
    {
      AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public MemberController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.Include(x=>x.Position).ToListAsync();
            
            return View(members);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.positions=await _context.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMembervm vm)
        {
            ViewBag.positions = await _context.Positions.ToListAsync();
            string filename=vm.Myfile.FileName;
            string path = env.WebRootPath + @"/upload/member/";
            using(FileStream stream = new FileStream(path+filename, FileMode.Create))
            {
                vm.Myfile.CopyTo(stream);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (vm.PositionId != null)
            {
                if(!await _context.Positions.AnyAsync(x=>x.Id == vm.PositionId))
                {
                    ModelState.AddModelError("PositionId",$"{vm.PositionId}-li position yoxdur");
                }
            }
            Member member = new Member()
            {
                FullName = vm.FullName,
                PositionId = vm.PositionId,
                Imgurl=vm.Myfile.FileName,

            };
           await _context.Members.AddAsync(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View();
            }
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == id);
            if (member == null)
            {
                return View();
            }
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update (int? id)
        {
            ViewBag.positions = await _context.Positions.ToListAsync();
            if (id == null)
            {
                return View();
            }
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == id);
            if (member == null)
            {
                return View();
            }
            UpdateMembervm vm = new UpdateMembervm()
            {
                Id = member.Id,
                FullName = member.FullName
            };
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Update (UpdateMembervm vm)
        {
            ViewBag.positions = await _context.Positions.ToListAsync();
            string filename = vm.Myfile.FileName;
            string path = env.WebRootPath + @"/upload/member/";
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                vm.Myfile.CopyTo(stream);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (vm.Id == null)
            {
                return View();
            }
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (member == null)
            {
                return View();
            }

            member.Id = vm.Id;
            member.FullName = vm.FullName;
            member.PositionId = vm.PositionId;
            member.Imgurl = vm.Myfile.FileName;

            _context.Members.Update(member);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
