using Microsoft.AspNetCore.Mvc;
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

        // GET: Lecture/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var lectureDetail = await _repository.GetLectureByIdAsync(id);

            return View(lectureDetail);
        }
    }
}
