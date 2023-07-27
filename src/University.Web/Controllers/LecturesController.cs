using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureVM;
using X.PagedList;

namespace University.Web.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILecturesService _lecturesService;
        private readonly ILectureCascadingDropdownsService _lectureCascadingDropdownsService;
        private readonly IScheduleService _scheduleService;

        public LecturesController(ILecturesService lecturesService,
                                  ILectureCascadingDropdownsService lectureCascadingDropdownsService,
                                  IScheduleService scheduleService)
        {
            _lecturesService = lecturesService;
            _lectureCascadingDropdownsService = lectureCascadingDropdownsService;
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["SubjectSortParm"] = sortOrder == "Subject" ? "subject_desc" : "Subject";
            ViewData["LectureRoomSortParm"] = sortOrder == "LectureRoom" ? "lectureRoom_desc" : "LectureRoom";
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

            var lectures = await _lecturesService.GetSortedLecturesList(sortOrder, searchString);
            if (lectures.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {lectures.StatusCode}, {lectures.Description}");
            }

            int pageSize = 6; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            return View(lectures.Data.ToPagedList(pageNumber, pageSize));
        }

        // GET: Lectures/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var lectureDetails = await _lecturesService.GetLectureWithIncludePropertiesById(id);
            if (lectureDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {lectureDetails.StatusCode}, {lectureDetails.Description}");
            }

            return View(lectureDetails.Data);
        }

        // GET: Lectures/Create
        public async Task<IActionResult> Create()
        {
            var faculties = (await _lectureCascadingDropdownsService.GetFaculties()).Data.Faculties;
            ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

            return View();
        }

        // this method is called using jQuery
        public async Task<JsonResult> GetValuesByFacultyId(int facultyId)
        {
            var response = await _lectureCascadingDropdownsService.GetDependentDropdownsValues();

            var academicEmployees = response.Data.AcademicEmployees.Where(g => g.FacultyId == facultyId).ToList();
            var groups = response.Data.Groups.Where(g => g.FacultyId == facultyId).ToList();
            var lectureRooms = response.Data.LectureRooms.Where(g => g.FacultyId == facultyId).ToList();
            var subjects = response.Data.Subjects.Where(g => g.FacultyId == facultyId).ToList();

            var dependentDropdownsValuesByFacultyId = new
            {
                academicEmployees,
                groups,
                lectureRooms,
                subjects
            };

            return Json(dependentDropdownsValuesByFacultyId);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewLectureVM newLectureVM)
        {
            if (!ModelState.IsValid)
            {
                var faculties = (await _lectureCascadingDropdownsService.GetFaculties()).Data.Faculties;
                ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

                return View(newLectureVM);
            }

            await _lecturesService.AddNewLecture(newLectureVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: Lectures/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var lectureDetails = await _lecturesService.GetLectureById(id);
            if (lectureDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {lectureDetails.StatusCode}, {lectureDetails.Description}");
            }

            var faculties = (await _lectureCascadingDropdownsService.GetFaculties()).Data.Faculties;
            ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

            var lectureDropdownsData = await _lectureCascadingDropdownsService.GetDependentDropdownsValues();

            ViewBag.Subjects = new SelectList(lectureDropdownsData.Data.Subjects.
                Where(f => f.FacultyId == lectureDetails.Data.FacultyId), "Id", "Name");
            ViewBag.LectureRooms = new SelectList(lectureDropdownsData.Data.LectureRooms
                .Where(f => f.FacultyId == lectureDetails.Data.FacultyId), "Id", "Name");
            ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Data.AcademicEmployees
                .Where(f => f.FacultyId == lectureDetails.Data.FacultyId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.FirstName} {a.LastName}" }), "Value", "Text");
            ViewBag.Groups = new SelectList(lectureDropdownsData.Data.Groups
                .Where(f => f.FacultyId == lectureDetails.Data.FacultyId), "Id", "Name");

            return View(lectureDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewLectureVM lectureVM)
        {
            if (id != lectureVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                var faculties = (await _lectureCascadingDropdownsService.GetFaculties()).Data.Faculties;
                ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

                var lectureDropdownsData = await _lectureCascadingDropdownsService.GetDependentDropdownsValues();

                ViewBag.Subjects = new SelectList(lectureDropdownsData.Data.Subjects, "Id", "Name");
                ViewBag.LectureRooms = new SelectList(lectureDropdownsData.Data.LectureRooms, "Id", "Name");
                ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Data.AcademicEmployees, "Id", "FirstName", "LastName");
                ViewBag.Groups = new SelectList(lectureDropdownsData.Data.Groups, "Id", "Name");

                return View(lectureVM);
            }

            await _lecturesService.UpdateLecture(lectureVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: Lectures/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var lectureDetails = await _lecturesService.GetLectureWithIncludePropertiesById(id);
            if (lectureDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {lectureDetails.StatusCode}, {lectureDetails.Description}");
            }

            return View(lectureDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var lectureDetails = await _lecturesService.GetLectureWithIncludePropertiesById(id);
            var response = await _lecturesService.DeleteLecture(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
