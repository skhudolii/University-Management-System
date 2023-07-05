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
            if (allLectureRooms.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)allLectureRooms.StatusCode}, {allLectureRooms.Description}");
            }

            return View(allLectureRooms.Data);
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
            if (lectureRoomDropdownsValues.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDropdownsValues.StatusCode}, {lectureRoomDropdownsValues.Description}");
            }

            ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");
            return View();
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

        // GET: LectureRooms/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var lectureRoomDetails = await _lectureRoomsService.GetLectureRoomById(id);
            if (lectureRoomDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            var lectureRoomDropdownsValues = await _lectureRoomsService.GetNewLectureRoomDropdownsValues();
            if (lectureRoomDropdownsValues.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            ViewBag.Faculties = new SelectList(lectureRoomDropdownsValues.Data.Faculties, "Id", "Name");
            return View(lectureRoomDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewLectureRoomVM lectureRoomVM)
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
            if (lectureRoomDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            var response = await _lectureRoomsService.DeleteLectureRoom(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {(int)lectureRoomDetails.StatusCode}, {lectureRoomDetails.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
