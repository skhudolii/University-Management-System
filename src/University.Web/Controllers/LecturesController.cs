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
            var allLectures = await _context.Lectures.ToListAsync();
            return View();
        }
    }
}
