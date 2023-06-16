using Microsoft.AspNetCore.Mvc;
using University.Core.Repositories;

namespace University.Web.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultiesRepository _repository;

        public FacultiesController(IFacultiesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var allFaculties = await _repository.GetAllAsync();
            return View(allFaculties);
        }

        // GET: Faculties/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var facultyDetail = await _repository.GetFacultyByIdAsync(id);
            return View(facultyDetail);
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}
