using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly UniversityDbContext _context;

        public FacultiesController(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allFaculties = await _context.Faculties.ToListAsync();
            return View(allFaculties);
        }
    }
}
