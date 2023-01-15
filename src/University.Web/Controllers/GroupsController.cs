using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class GroupsController : Controller
    {
        private readonly UniversityDbContext _context;

        public GroupsController(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allGroups = await _context.Groups.ToListAsync();
            return View(allGroups);
        }
    }
}
