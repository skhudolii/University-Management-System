using Microsoft.AspNetCore.Mvc;
using University.Core.Services;
using University.Core.Services.Interfaces;

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
    }
}
