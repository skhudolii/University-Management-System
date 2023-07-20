using Microsoft.AspNetCore.Mvc;
using University.Core.Services;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.FacultyVM;

namespace University.Web.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultiesService _facultiesService;

        public FacultiesController(IFacultiesService facultiesService)
        {
            _facultiesService = facultiesService;
        }

        public async Task<IActionResult> Index()
        {
            var allFaculties = await _facultiesService.GetFacultiesList();
            return View(allFaculties.Data);
        }

        // GET: Faculties/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var facultyDetails = await _facultiesService.GetFacultyWithIncludePropertiesById(id);
            if (facultyDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {facultyDetails.StatusCode}, {facultyDetails.Description}");
            }

            return View(facultyDetails.Data);
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewFacultyVM facultyVM)
        {
            if (!ModelState.IsValid)
            {
                return View(facultyVM);
            }

            var response = await _facultiesService.AddNewFaculty(facultyVM);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Faculties/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var facultyDetails = await _facultiesService.GetFacultyById(id);
            if (facultyDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {facultyDetails.StatusCode}, {facultyDetails.Description}");
            }

            return View(facultyDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewFacultyVM facultyVM)
        {
            if (id != facultyVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                return View(facultyVM);
            }

            var response = await _facultiesService.UpdateFaculty(facultyVM);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Faculties/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var facultyDetails = await _facultiesService.GetFacultyWithIncludePropertiesById(id);
            if (facultyDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {facultyDetails.StatusCode}, {facultyDetails.Description}");
            }

            return View(facultyDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var facultyDetails = await _facultiesService.GetFacultyWithIncludePropertiesById(id);
            var response = await _facultiesService.DeleteFaculty(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
