using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.AcademicEmployeeVM;
using University.Web.ViewModels;
using X.PagedList;

namespace University.Web.Controllers
{
    public class AcademicEmployeesController : Controller
    {
        private readonly IAcademicEmployeesService _academicEmployeesService;

        public AcademicEmployeesController(IAcademicEmployeesService academicEmployeesService)
        {
            _academicEmployeesService = academicEmployeesService;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AcademicPositionSortParm"] = sortOrder == "AcademicPosition" ? "academicPosition_desc" : "AcademicPosition";
            ViewData["FacultySortParm"] = sortOrder == "Faculty" ? "faculty_desc" : "Faculty";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var academicEmployees = await _academicEmployeesService.GetSortedAcademicEmployeesList(sortOrder, searchString);
            if (academicEmployees.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {academicEmployees.StatusCode}, {academicEmployees.Description}");
            }

            int pageSize = 5; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new AcademicEmployeesListViewModel
            {
                PagedAcademicEmployees = academicEmployees.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                LastNameSortParm = ViewData["LastNameSortParm"] as string, // Updated property name
                AcademicPositionSortParm = ViewData["AcademicPositionSortParm"] as string,
                FacultySortParm = ViewData["FacultySortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
        }

        // GET: AcademicEmployees/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var academicEmployeeDetails = await _academicEmployeesService.GetAcademicEmployeeWithIncludePropertiesById(id);
            if (academicEmployeeDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)academicEmployeeDetails.StatusCode}, {academicEmployeeDetails.Description}");
            }

            return View(academicEmployeeDetails.Data);
        }

        // GET: AcademicEmployees/Create
        public async Task<IActionResult> Create()
        {
            var academicEmployeeDropdownsValues = await _academicEmployeesService.GetNewAcademicEmployeeDropdownsValues();
            ViewBag.Faculties = new SelectList(academicEmployeeDropdownsValues.Data.Faculties, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewAcademicEmployeeVM academicEmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                var academicEmployeeDropdownsValues = await _academicEmployeesService.GetNewAcademicEmployeeDropdownsValues();
                ViewBag.Faculties = new SelectList(academicEmployeeDropdownsValues.Data.Faculties, "Id", "Name");

                return View(academicEmployeeVM);
            }

            await _academicEmployeesService.AddNewAcademicEmployee(academicEmployeeVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: AcademicEmployees/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var academicEmployeeDetails = await _academicEmployeesService.GetAcademicEmployeeById(id);
            if (academicEmployeeDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)academicEmployeeDetails.StatusCode}, {academicEmployeeDetails.Description}");
            }

            var academicEmployeeDropdownsValues = await _academicEmployeesService.GetNewAcademicEmployeeDropdownsValues();
            ViewBag.Faculties = new SelectList(academicEmployeeDropdownsValues.Data.Faculties, "Id", "Name");

            return View(academicEmployeeDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewAcademicEmployeeVM academicEmployeeVM)
        {
            if (id != academicEmployeeVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                var academicEmployeeDropdownsValues = await _academicEmployeesService.GetNewAcademicEmployeeDropdownsValues();
                ViewBag.Faculties = new SelectList(academicEmployeeDropdownsValues.Data.Faculties, "Id", "Name");

                return View(academicEmployeeVM);
            }

            await _academicEmployeesService.UpdateAcademicEmployee(academicEmployeeVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: AcademicEmployees/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var academicEmployeeDetails = await _academicEmployeesService.GetAcademicEmployeeWithIncludePropertiesById(id);
            if (academicEmployeeDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)academicEmployeeDetails.StatusCode}, {academicEmployeeDetails.Description}");
            }

            return View(academicEmployeeDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var academicEmployeeDetails = await _academicEmployeesService.GetAcademicEmployeeWithIncludePropertiesById(id);
            var response = await _academicEmployeesService.DeleteAcademicEmployee(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
