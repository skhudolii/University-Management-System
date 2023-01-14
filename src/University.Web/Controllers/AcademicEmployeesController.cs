using Microsoft.AspNetCore.Mvc;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class AcademicEmployeesController : Controller
    {
        private readonly UniversityDbContext _context;

        public AcademicEmployeesController(UniversityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.AcademicEmployees.ToList();
            return View(data);
        }
    }
}
