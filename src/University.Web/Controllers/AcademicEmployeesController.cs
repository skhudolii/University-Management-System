using Microsoft.AspNetCore.Mvc;
using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class AcademicEmployeesController : Controller
    {
        private readonly IAcademicEmployeesRepository _repository;

        public AcademicEmployeesController(IAcademicEmployeesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAll();
            return View(data);
        }

        // Get: AcademicEmployees/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Email,AcademicPosition,FacultyId")] AcademicEmployee academicEmployee)
        {
            if (!ModelState.IsValid)
            {
                return View(academicEmployee);
            }
            _repository.Add(academicEmployee);
            return RedirectToAction(nameof(Index));
        }
    }
}
