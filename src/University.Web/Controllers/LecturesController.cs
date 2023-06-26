using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Repositories;
using University.Core.ViewModels.Lecture;

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

        public async Task<IActionResult> Filter(string searchString)
        {
            var allLectures = await _repository.GetAllAsync(s => s.Subject, t => t.Teacher);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allLectures.Where(n => n.Subject.Name.ToLower().Contains(searchString.ToLower()) ||
                                                            n.Teacher.FullName.ToLower().Contains(searchString.ToLower()) || 
                                                            n.LectureDate.ToString().Contains(searchString));

                return View("Index", filteredResult);
            }

            return View("Index", allLectures);
        }

        // GET: Lectures/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var lectureDetails = await _repository.GetLectureByIdAsync(id);

            return View(lectureDetails);
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

        [HttpPost]
        public async Task<IActionResult> Create(NewLectureVM lecture)
        {
            if (!ModelState.IsValid)
            {
                var lectureDropdownsData = await _repository.GetNewLectureDropdownsValues();

                ViewBag.Faculties = new SelectList(lectureDropdownsData.Faculties, "Id", "Name");
                ViewBag.Subjects = new SelectList(lectureDropdownsData.Subjects, "Id", "Name");
                ViewBag.LectureRooms = new SelectList(lectureDropdownsData.LectureRooms, "Id", "Name");
                ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Teachers, "Id", "FullName");
                ViewBag.Groups = new SelectList(lectureDropdownsData.Groups, "Id", "Name");

                return View(lecture);
            }

            await _repository.AddNewLectureAsync(lecture);
            return RedirectToAction(nameof(Index));
        }

        // GET: Lectures/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var lectureDetails = await _repository.GetLectureByIdAsync(id);
            if (lectureDetails == null)
            {
                return View("NotFound");
            }

            var responce = new NewLectureVM()
            {
                Id = lectureDetails.Id,
                LectureDate = lectureDetails.LectureDate,
                StartTime = lectureDetails.StartTime,
                EndTime = lectureDetails.EndTime,
                FacultyId = (int)lectureDetails.FacultyId,
                SubjectId = lectureDetails.SubjectId,
                LectureRoomId = lectureDetails.LectureRoomId,
                AcademicEmployeeId = lectureDetails.AcademicEmployeeId,
                GroupIds = lectureDetails.LecturesGroups.Select(n => n.GroupId).ToList()
            };

            var lectureDropdownsData = await _repository.GetNewLectureDropdownsValues();

            ViewBag.Faculties = new SelectList(lectureDropdownsData.Faculties, "Id", "Name");
            ViewBag.Subjects = new SelectList(lectureDropdownsData.Subjects, "Id", "Name");
            ViewBag.LectureRooms = new SelectList(lectureDropdownsData.LectureRooms, "Id", "Name");
            ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Teachers, "Id", "FullName");
            ViewBag.Groups = new SelectList(lectureDropdownsData.Groups, "Id", "Name");

            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewLectureVM lecture)
        {
            if (id != lecture.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var lectureDropdownsData = await _repository.GetNewLectureDropdownsValues();

                ViewBag.Faculties = new SelectList(lectureDropdownsData.Faculties, "Id", "Name");
                ViewBag.Subjects = new SelectList(lectureDropdownsData.Subjects, "Id", "Name");
                ViewBag.LectureRooms = new SelectList(lectureDropdownsData.LectureRooms, "Id", "Name");
                ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Teachers, "Id", "FullName");
                ViewBag.Groups = new SelectList(lectureDropdownsData.Groups, "Id", "Name");

                return View(lecture);
            }

            await _repository.UpdateLectureAsync(lecture);
            return RedirectToAction(nameof(Index));
        }

        // GET: Lectures/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var lectureDetails = await _repository.GetLectureByIdAsync(id);
            if (lectureDetails == null)
            {
                return View("NotFound");
            }

            return View(lectureDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var lectureDetails = await _repository.GetLectureByIdAsync(id);
            if (lectureDetails == null)
            {
                return View("NotFound");
            }

            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
