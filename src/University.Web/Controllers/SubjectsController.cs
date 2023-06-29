using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allSubjects = await _subjectsService.GetSubjectsList();
            if (allSubjects.StatusCode == Core.Enums.StatusCode.OK)
            {
                return View(allSubjects.Data);
            }
            return View("Error", $"Error {(int)allSubjects.StatusCode}, {allSubjects.Description}");
        }

        // GET: Subjects/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var subjectDetails = await _subjectsService.GetSubjectById(id);
            if (subjectDetails.StatusCode == Core.Enums.StatusCode.OK)
            {
                return View(subjectDetails.Data);
            }

            return View("Error", $"Error {(int)subjectDetails.StatusCode}, {subjectDetails.Description}");
        }

        // GET: Subjects/Create
        public async Task<IActionResult> Create()
        {
            var subjectDropdownsValues = await _subjectsService.GetNewSubjectDropdownsValues();
            if (subjectDropdownsValues.StatusCode == Core.Enums.StatusCode.OK)
            {
                ViewBag.Faculties = new SelectList(subjectDropdownsValues.Data.Faculties, "Id", "Name");
                return View();
            }

            return View("Error", $"Error {(int)subjectDropdownsValues.StatusCode}, {subjectDropdownsValues.Description}");
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
    }
}
