using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.SubjectVM;

namespace University.Web.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["FacultyNameSortParm"] = sortOrder == "FacultyName" ? "facultyname_desc" : "FacultyName";

            var subjects = await _subjectsService.GetSortedSubjectsList(sortOrder, searchString);

            return View(subjects.Data);
        }

        // GET: Subjects/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var subjectDetails = await _subjectsService.GetSubjectWithIncludePropertiesById(id);
            if (subjectDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)subjectDetails.StatusCode}, {subjectDetails.Description}");                
            }

            return View(subjectDetails.Data);

        }

        // GET: Subjects/Create
        public async Task<IActionResult> Create()
        {
            var subjectDropdownsValues = await _subjectsService.GetNewSubjectDropdownsValues();
            ViewBag.Faculties = new SelectList(subjectDropdownsValues.Data.Faculties, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewSubjectVM subject)
        {
            if (!ModelState.IsValid)
            {
                var subjectDropdownsValues = await _subjectsService.GetNewSubjectDropdownsValues();
                ViewBag.Faculties = new SelectList(subjectDropdownsValues.Data.Faculties, "Id", "Name");

                return View(subject);
            }

            await _subjectsService.AddNewSubject(subject);
            return RedirectToAction(nameof(Index));
        }

        // GET: Subjects/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var subjectDetails = await _subjectsService.GetSubjectById(id);
            if (subjectDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)subjectDetails.StatusCode}, {subjectDetails.Description}");
            }

            var subjectDropdownsValues = await _subjectsService.GetNewSubjectDropdownsValues();
            ViewBag.Faculties = new SelectList(subjectDropdownsValues.Data.Faculties, "Id", "Name");

            return View(subjectDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewSubjectVM subjectVM)
        {
            if (id != subjectVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                var subjectDropdownsValues = await _subjectsService.GetNewSubjectDropdownsValues();
                ViewBag.Faculties = new SelectList(subjectDropdownsValues.Data.Faculties, "Id", "Name");

                return View(subjectVM);
            }

            await _subjectsService.UpdateSubject(subjectVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: Subjects/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var subjectDetails = await _subjectsService.GetSubjectWithIncludePropertiesById(id);
            if (subjectDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)subjectDetails.StatusCode}, {subjectDetails.Description}");
            }

            return View(subjectDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var subjectDetails = await _subjectsService.GetSubjectWithIncludePropertiesById(id);
            var response = await _subjectsService.DeleteSubject(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
