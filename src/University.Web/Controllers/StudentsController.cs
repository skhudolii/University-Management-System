using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityDbContext _context;

        public StudentsController(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allStudents = await _context.Students.ToListAsync();
            return View();
        }
    }
}
