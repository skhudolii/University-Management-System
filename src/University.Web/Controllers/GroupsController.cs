using Microsoft.AspNetCore.Mvc;
using University.Core.Entities;
using University.Core.Repositories;

namespace University.Web.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupsRepository _repository;

        public GroupsController(IGroupsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var allGroups = await _repository.GetAllAsync();
            return View(allGroups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,FacultyId")]Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }
            await _repository.AddAsync(group);
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var groupDetails = await _repository.GetByIdAsync(id);

            if (groupDetails == null)
            {
                return View("NotFound");
            }
            return View(groupDetails);
        }

        // GET: Groups/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var groupDetails = await _repository.GetByIdAsync(id);

            if (groupDetails == null)
            {
                return View("NotFound");
            }
            return View(groupDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FacultyId")] Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }
            await _repository.UpdateAsync(id, group);
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var groupDetails = await _repository.GetByIdAsync(id);

            if (groupDetails == null)
            {
                return View("NotFound");
            }
            return View(groupDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var groupDetails = await _repository.GetByIdAsync(id);

            if (groupDetails == null)
            {
                return View("NotFound");
            }
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
