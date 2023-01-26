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
            var data = await _repository.GetAllAsync();
            return View(data);
        }

        // Get: AcademicEmployees/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Email,AcademicPosition,FacultyId")] AcademicEmployee academicEmployee)
        {
            if (!ModelState.IsValid)
            {
                return View(academicEmployee);
            }
            await _repository.AddAsync(academicEmployee);
            return RedirectToAction(nameof(Index));
        }

        // Get: AcademicEmployees/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var academicEmployeeDetails = await _repository.GetByIdAsync(id);

            if (academicEmployeeDetails == null)
            {
                return View("NotFound");
            }
            return View(academicEmployeeDetails);
        }

        // Get: AcademicEmployees/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var academicEmployeeDetails = await _repository.GetByIdAsync(id);

            if (academicEmployeeDetails == null)
            {
                return View("NotFound");
            }
            return View(academicEmployeeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Email,AcademicPosition,FacultyId")] AcademicEmployee academicEmployee)
        {
            if (!ModelState.IsValid)
            {
                return View(academicEmployee);
            }
            await _repository.UpdateAsync(id, academicEmployee);
            return RedirectToAction(nameof(Index));
        }

        // Get: AcademicEmployees/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var academicEmployeeDetails = await _repository.GetByIdAsync(id);

            if (academicEmployeeDetails == null)
            {
                return View("NotFound");
            }
            return View(academicEmployeeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicEmployeeDetails = await _repository.GetByIdAsync(id);

            if (academicEmployeeDetails == null)
            {
                return View("NotFound");
            }
            await _repository.DeleteAsync(id);            
            return RedirectToAction(nameof(Index));
        }
    }
}
