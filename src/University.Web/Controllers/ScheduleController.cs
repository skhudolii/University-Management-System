using Microsoft.AspNetCore.Mvc;
using University.Core.Services.Interfaces;
using University.Web.ViewModels;
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

            int pageSize = 6; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new ScheduleForFacultyViewModel
            {
                PagedLectures = scheduleForFaculty.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                DateSortParm = ViewData["DateSortParm"] as string,
                SubjectSortParm = ViewData["SubjectSortParm"] as string,
                LectureRoomSortParm = ViewData["LectureRoomSortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ScheduleForTeacher(int id, int daysForward, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["DaysForward"] = daysForward;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["SubjectSortParm"] = sortOrder == "Subject" ? "subject_desc" : "Subject";
            ViewData["LectureRoomSortParm"] = sortOrder == "LectureRoom" ? "lectureRoom_desc" : "LectureRoom";

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

            int pageSize = 6; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new ScheduleForTeacherViewModel
            {
                DaysForward = daysForward,
                PagedLectures = scheduleForTeacher.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                DateSortParm = ViewData["DateSortParm"] as string,
                SubjectSortParm = ViewData["SubjectSortParm"] as string,
                LectureRoomSortParm = ViewData["LectureRoomSortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ScheduleForStudent(int id, int daysForward, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["DaysForward"] = daysForward;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["SubjectSortParm"] = sortOrder == "Subject" ? "subject_desc" : "Subject";
            ViewData["LectureRoomSortParm"] = sortOrder == "LectureRoom" ? "lectureRoom_desc" : "LectureRoom";

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

            int pageSize = 6; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new ScheduleForStudentViewModel
            {
                DaysForward = daysForward,
                PagedLectures = scheduleForStudent.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                DateSortParm = ViewData["DateSortParm"] as string,
                SubjectSortParm = ViewData["SubjectSortParm"] as string,
                LectureRoomSortParm = ViewData["LectureRoomSortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
        }
    }
}
