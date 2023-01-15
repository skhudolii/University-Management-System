using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class LectureRoomsController : Controller
    {
        private readonly UniversityDbContext _context;

        public LectureRoomsController(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allLectureRooms = await _context.LectureRooms.ToListAsync();
            return View(allLectureRooms);
        }
    }
}
