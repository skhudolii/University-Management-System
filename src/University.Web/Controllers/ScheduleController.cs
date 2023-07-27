using Microsoft.AspNetCore.Mvc;
using University.Core.Services.Interfaces;
using X.PagedList;

namespace University.Web.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> ScheduleForFaculty(int id, string sortOrder, string currentFilter, string searchString, int? page)
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

            var scheduleForFaculty = await _scheduleService.GetScheduleForFaculty(id, sortOrder, searchString);
            if (scheduleForFaculty.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {scheduleForFaculty.StatusCode}, {scheduleForFaculty.Description}");
            }

            int pageSize = 5; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            return View(scheduleForFaculty.Data.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> ScheduleForTeacher(int id, int daysForward, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["DaysForward"] = daysForward;
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

            var scheduleForTeacher = await _scheduleService.GetScheduleForTeacher(id, DateTime.Now.Date.AddDays(daysForward), sortOrder, searchString);
            if (scheduleForTeacher.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {scheduleForTeacher.StatusCode}, {scheduleForTeacher.Description}");
            }

            int pageSize = 5; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            return View(scheduleForTeacher.Data.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> ScheduleForStudent(int id, int daysForward, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["DaysForward"] = daysForward;
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

            var scheduleForStudent = await _scheduleService.GetScheduleForStudent(id, DateTime.Now.Date.AddDays(daysForward), sortOrder, searchString);
            if (scheduleForStudent.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {scheduleForStudent.StatusCode}, {scheduleForStudent.Description}");
            }

            int pageSize = 3; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            return View(scheduleForStudent.Data.ToPagedList(pageNumber, pageSize));
        }
    }
}
