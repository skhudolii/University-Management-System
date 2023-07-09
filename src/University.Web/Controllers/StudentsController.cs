using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Entities;
using University.Core.Services;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.StudentVM;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IStudentCascadingDropdownsService _studentCascadingDropdownsService;

        public StudentsController(IStudentsService studentsService,
                                  IStudentCascadingDropdownsService studentCascadingDropdownsService)
        {
            _studentsService = studentsService;
            _studentCascadingDropdownsService = studentCascadingDropdownsService;
        }

        public async Task<IActionResult> Index()
        {
            var allStudents = await _studentsService.GetStudentsList();
            return View(allStudents.Data);
        }

        // GET: Students/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var studentDetails = await _studentsService.GetStudentWithIncludePropertiesById(id);
            if (studentDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {studentDetails.StatusCode}, {studentDetails.Description}");
            }
            return View(studentDetails.Data);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            var faculties = (await _studentCascadingDropdownsService.GetFaculties()).Data.Faculties;
            ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

            return View();
        }

        // this method is called using jQuery
        public async Task<JsonResult> GetGroupsByFacultyId(int facultyId)
        {
            var groups = (await _studentCascadingDropdownsService.GetGroups()).Data.Groups
                .Where(g => g.FacultyId == facultyId).ToList();

            return Json(groups);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewStudentVM newStudentVM)
        {
            if (!ModelState.IsValid)
            {
                var faculties = (await _studentCascadingDropdownsService.GetFaculties()).Data.Faculties;
                ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

                return View(newStudentVM);
            }

            await _studentsService.AddNewStudent(newStudentVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var studentDetails = await _studentsService.GetStudentById(id);
            if (studentDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)studentDetails.StatusCode}, {studentDetails.Description}");
            }

            var faculties = (await _studentCascadingDropdownsService.GetFaculties()).Data.Faculties;
            ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

            return View(studentDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewStudentVM studentVM)
        {
            if (id != studentVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                var faculties = (await _studentCascadingDropdownsService.GetFaculties()).Data.Faculties;
                ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

                return View(studentVM);
            }

            await _studentsService.UpdateStudent(studentVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var studentDetails = await _studentsService.GetStudentWithIncludePropertiesById(id);
            if (studentDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {studentDetails.StatusCode}, {studentDetails.Description}");
            }

            return View(studentDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var studentDetails = await _studentsService.GetStudentWithIncludePropertiesById(id);
            if (studentDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {studentDetails.StatusCode}, {studentDetails.Description}");
            }

            var response = await _studentsService.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
