using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureVM;

namespace University.Web.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILecturesService _lecturesService;
        private readonly ILectureCascadingDropdownsService _lectureCascadingDropdownsService;

        public LecturesController(ILecturesService lecturesService,
                                  ILectureCascadingDropdownsService lectureCascadingDropdownsService)
        {
            _lecturesService = lecturesService;
            _lectureCascadingDropdownsService = lectureCascadingDropdownsService;
        }

        public async Task<IActionResult> Index()
        {
            var allLectures = await _lecturesService.GetLecturesList();
            return View(allLectures.Data);
        }

        //public async Task<IActionResult> Filter(string searchString)
        //{
        //    var allLectures = await _repository.GetAllAsync(s => s.Subject, t => t.Teacher);

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        var filteredResult = allLectures.Where(n => n.Subject.Name.ToLower().Contains(searchString.ToLower()) ||
        //                                                    n.Teacher.FullName.ToLower().Contains(searchString.ToLower()) || 
        //                                                    n.LectureDate.ToString().Contains(searchString));

        //        return View("Index", filteredResult);
        //    }

        //    return View("Index", allLectures);
        //}

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
                .Where(f => f.FacultyId == lectureDetails.Data.FacultyId), "Id", "FullName");
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
                ViewBag.AcademicEmployees = new SelectList(lectureDropdownsData.Data.AcademicEmployees, "Id", "FullName");
                ViewBag.Groups = new SelectList(lectureDropdownsData.Data.Groups, "Id", "Name");

                return View(lectureVM);
            }

            await _lecturesService.UpdateLecture(lectureVM);
            return RedirectToAction(nameof(Index));
        }

        //// GET: Lectures/Delete/1
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var lectureDetails = await _repository.GetLectureByIdAsync(id);
        //    if (lectureDetails == null)
        //    {
        //        return View("NotFound");
        //    }

        //    return View(lectureDetails);
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirm(int id)
        //{
        //    var lectureDetails = await _repository.GetLectureByIdAsync(id);
        //    if (lectureDetails == null)
        //    {
        //        return View("NotFound");
        //    }

        //    await _repository.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
