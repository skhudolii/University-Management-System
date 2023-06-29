using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureRoomVM;

namespace University.Web.Controllers
{
    public class LectureRoomsController : Controller
    {
        private readonly ILectureRoomsService _lectureRoomsService;

        public LectureRoomsController(ILectureRoomsService lectureRoomsService)
        {
            _lectureRoomsService = lectureRoomsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allLectureRooms = await _lectureRoomsService.GetLectureRoomsList();
            if (allLectureRooms.StatusCode == Core.Enums.StatusCode.OK)
            {
                return View(allLectureRooms.Data);
            }
            return View("Error", $"Error {(int)allLectureRooms.StatusCode}, {allLectureRooms.Description}");
        }

        // GET: Subjects/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var lectureRoomDetails = await _lectureRoomsService.GetLectureRoomById(id);
            if (lectureRoomDetails.StatusCode == Core.Enums.StatusCode.OK)
            {
                return View(lectureRoomDetails.Data);
            }

            return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
        }

        // GET: LectureRooms/Create
        public async Task<IActionResult> Create()
        {
            var lectureRoomDropdownsValues = await _lectureRoomsService.GetNewLectureRoomDropdownsValues();
            if (lectureRoomDropdownsValues.StatusCode == Core.Enums.StatusCode.OK)
            {
                ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");
                return View();
            }

            return View("Error", $"Error {(int)lectureRoomDropdownsValues.StatusCode}, {lectureRoomDropdownsValues.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewLectureRoomVM lectureRoom)
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
    }
}
