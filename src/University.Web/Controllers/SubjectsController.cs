using Microsoft.AspNetCore.Mvc;
using University.Core.Services.Interfaces;

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
            return View(allSubjects.Data);
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
    }
}
