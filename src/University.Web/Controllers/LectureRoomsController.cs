using Microsoft.AspNetCore.Mvc;
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
            var response = await _lectureRoomsService.GetLectureRoomsList();
            return View(response.Data);
        }
    }
}
