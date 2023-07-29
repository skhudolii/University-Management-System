using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureRoomVM;
using University.Web.ViewModels;
using X.PagedList;

namespace University.Web.Controllers
{
    public class LectureRoomsController : Controller
    {
        private readonly ILectureRoomsService _lectureRoomsService;

        public LectureRoomsController(ILectureRoomsService lectureRoomsService)
        {
            _lectureRoomsService = lectureRoomsService;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CapacitySortParm"] = sortOrder == "Capacity" ? "capacity_desc" : "Capacity";
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

            var lectureRooms = await _lectureRoomsService.GetSortedLectureRoomsList(sortOrder, searchString);
            if (lectureRooms.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {lectureRooms.StatusCode}, {lectureRooms.Description}");
            }

            int pageSize = 8; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new LectureRoomsListViewModel
            {
                PagedLectureRooms = lectureRooms.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                NameSortParm = ViewData["NameSortParm"] as string,
                CapacitySortParm = ViewData["CapacitySortParm"] as string,
                FacultySortParm = ViewData["FacultySortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
        }

        // GET: Subjects/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var lectureRoomDetails = await _lectureRoomsService.GetLectureRoomWithIncludePropertiesById(id);
            if (lectureRoomDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            return View(lectureRoomDetails.Data);
        }

        // GET: LectureRooms/Create
        public async Task<IActionResult> Create()
        {
            var lectureRoomDropdownsValues = await _lectureRoomsService.GetNewLectureRoomDropdownsValues();
            ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewLectureRoomModel lectureRoom)
        {
            if (!ModelState.IsValid)
            {
                var lectureRoomDropdownsValues = await _lectureRoomsService.GetNewLectureRoomDropdownsValues();
                ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");

                return View(lectureRoom);
            }

            await _lectureRoomsService.AddNewLectureRoom(lectureRoom);
            return RedirectToAction(nameof(Index));
        }

        // GET: LectureRooms/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var lectureRoomDetails = await _lectureRoomsService.GetLectureRoomById(id);
            if (lectureRoomDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            var lectureRoomDropdownsValues = await _lectureRoomsService.GetNewLectureRoomDropdownsValues();
            ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");

            return View(lectureRoomDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewLectureRoomModel lectureRoomVM)
        {
            if (id != lectureRoomVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                var lectureRoomDropdownsValues = await _lectureRoomsService.GetNewLectureRoomDropdownsValues();
                ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");

                return View(lectureRoomVM);
            }

            await _lectureRoomsService.UpdateLectureRoom(lectureRoomVM);
            return RedirectToAction(nameof(Index));
        }

        // GET: LectureRooms/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var lectureRoomDetails = await _lectureRoomsService.GetLectureRoomWithIncludePropertiesById(id);
            if (lectureRoomDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            return View(lectureRoomDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var lectureRoomDetails = await _lectureRoomsService.GetLectureRoomWithIncludePropertiesById(id);
            var response = await _lectureRoomsService.DeleteLectureRoom(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
