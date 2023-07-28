using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.StudentVM;
using University.Web.ViewModels;
using X.PagedList;

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

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            ViewData["GroupSortParm"] = sortOrder == "Group" ? "group_desc" : "Group";
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

            var students = await _studentsService.GetSortedStudentsList(sortOrder, searchString);
            if (students.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {students.StatusCode}, {students.Description}");
            }

            int pageSize = 5; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new StudentsListViewModel
            {
                PagedStudents = students.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                LastNameSortParm = ViewData["LastNameSortParm"] as string,
                FirstNameSortParm = ViewData["FirstNameSortParm"] as string,
                GroupSortParm = ViewData["GroupSortParm"] as string,
                FacultySortParm = ViewData["FacultySortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
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
            var groups = (await _studentCascadingDropdownsService.GetDependentGroups()).Data.Groups
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
                return View("Error", $"Error {studentDetails.StatusCode}, {studentDetails.Description}");
            }

            var faculties = (await _studentCascadingDropdownsService.GetFaculties()).Data.Faculties;
            ViewBag.Faculties = new SelectList(faculties, "Id", "Name");

            var studentDropdownsData = await _studentCascadingDropdownsService.GetDependentGroups();
            ViewBag.Groups = new SelectList(studentDropdownsData.Data.Groups.
                Where(f => f.FacultyId == studentDetails.Data.FacultyId), "Id", "Name");

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

                var studentDropdownsData = await _studentCascadingDropdownsService.GetDependentGroups();
                ViewBag.Groups = new SelectList(studentDropdownsData.Data.Groups, "Id", "Name");

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
            var response = await _studentsService.DeleteStudent(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
