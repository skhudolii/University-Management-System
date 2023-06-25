using Microsoft.AspNetCore.Mvc;
using University.Core.Services.Interfaces;

namespace University.Web.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _subjectsService.GetSubjectsList();
            return View(response.Data);
        }
    }
}
