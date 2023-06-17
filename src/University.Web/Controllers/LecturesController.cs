using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Repositories;

namespace University.Web.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILecturesRepository _repository;

        public LecturesController(ILecturesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var allLectures = await _repository.GetAllAsync(n => n.Subject, n => n.LectureRoom);
            return View(allLectures);
        }

        // GET: Lectures/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var lectureDetail = await _repository.GetLectureByIdAsync(id);

            return View(lectureDetail);
        }

        // GET: Lectures/Create
        public async Task<IActionResult> Create()
        {
            var lectureDropdownsData = await _repository.GetNewLectureDropdownsValues();

            ViewBag.Faculties = new SelectList(lectureDropdownsData.Faculties, "Id", "Name");
            ViewBag.Subjects = new SelectList(lectureDropdownsData.Subjects, "Id", "Name");
            ViewBag.LectureRooms = new SelectList(lectureDropdownsData.LectureRooms, "Id", "Name");
            ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Teachers, "Id", "FullName");
            ViewBag.Groups = new SelectList(lectureDropdownsData.Groups, "Id", "Name");

            return View();
        }
    }
}
