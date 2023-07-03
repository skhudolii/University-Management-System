using Microsoft.AspNetCore.Mvc;
using University.Core.Services.Interfaces;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<IActionResult> Index()
        {
            var allStudents = await _studentsService.GetStudentsList();
            if (allStudents.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {allStudents.StatusCode}, {allStudents.Description}");
            }
            return View(allStudents.Data);
        }

        // GET: Students/Details/1
        //public async Task<IActionResult> Details(int id)
        //{
        //    var studentDetails = await _repository.GetByIdAsync(id);
        //
        //    if (studentDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(studentDetails);
        //}
        //
        //// GET: Students/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //
        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Email,GroupId")] Student student)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(student);
        //    }
        //    await _repository.AddAsync(student);
        //    return RedirectToAction(nameof(Index));
        //}
        //
        //// GET: Students/Edit/1
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var studentDetails = await _repository.GetByIdAsync(id);
        //
        //    if (studentDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(studentDetails);
        //}
        //
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Email,GroupId")] Student student)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(student);
        //    }
        //    if (id == student.Id)
        //    {
        //        await _repository.UpdateAsync(id, student);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}
        //
        //// GET: Students/Delete/1
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var studentDetails = await _repository.GetByIdAsync(id);
        //
        //    if (studentDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(studentDetails);
        //}
        //
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var studentDetails = await _repository.GetByIdAsync(id);
        //
        //    if (studentDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    await _repository.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
