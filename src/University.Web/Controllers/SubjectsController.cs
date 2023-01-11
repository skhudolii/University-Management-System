using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly UniversityDbContext _context;

        public SubjectsController(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allSubjects = await _context.Subjects.ToListAsync();
            return View();
        }
    }
}
