using Microsoft.AspNetCore.Mvc;
using University.Core.Repositories;
using University.Infrastructure.Data;

namespace University.Web.Controllers
{
    public class AcademicEmployeesController : Controller
    {
        private readonly IAcademicEmployeesRepository _repository;

        public AcademicEmployeesController(IAcademicEmployeesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAll();
            return View(data);
        }
    }
}
