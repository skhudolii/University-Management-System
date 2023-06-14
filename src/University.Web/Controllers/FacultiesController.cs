using Microsoft.AspNetCore.Mvc;
using University.Core.Repositories;

namespace University.Web.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultiesRepository _repository;

        public FacultiesController(IFacultiesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var allFaculties = await _repository.GetAllAsync();
            return View(allFaculties);
        }
    }
}
