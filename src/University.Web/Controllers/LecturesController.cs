using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class LecturesController : Controller
    {
        private readonly UniversityDbContext _context;

        public LecturesController(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allLectures = await _context.Lectures.Include(s => s.Subject)
                                                     .Include(l => l.LectureRoom)
                                                     .OrderBy(l => l.LectureDate)
                                                     .ToListAsync();
            return View(allLectures);
        }
    }
}
