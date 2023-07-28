using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.GroupVM;
using University.Web.ViewModels;
using X.PagedList;

namespace University.Web.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var groups = await _groupsService.GetSortedGroupsList(sortOrder, searchString);
            if (groups.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {groups.StatusCode}, {groups.Description}");
            }

            int pageSize = 8; // Set the desired page size here
            int pageNumber = page ?? 1; // If page is null, default to page 1

            var viewModel = new GroupsListViewModel
            {
                PagedGroups = groups.Data.ToPagedList(pageNumber, pageSize),
                CurrentSort = sortOrder,
                NameSortParm = ViewData["NameSortParm"] as string,
                FacultySortParm = ViewData["FacultySortParm"] as string,
                CurrentFilter = ViewData["CurrentFilter"] as string
            };

            return View(viewModel);
        }

        // GET: Groups/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var groupDetails = await _groupsService.GetGroupWithIncludePropertiesById(id);
            if (groupDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {groupDetails.StatusCode}, {groupDetails.Description}");
            }

            return View(groupDetails.Data);
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            var groupDropdownsValues = await _groupsService.GetNewGroupDropdownsValues();
            ViewBag.Faculties = new SelectList(groupDropdownsValues.Data.Faculties, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewGroupModel groupVM)
        {
            if (!ModelState.IsValid)
            {
                var groupDropdownsValues = await _groupsService.GetNewGroupDropdownsValues();
                ViewBag.Faculties = new SelectList(groupDropdownsValues.Data.Faculties, "Id", "Name");

                return View(groupVM);
            }

            var response = await _groupsService.AddNewGroup(groupVM);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var groupDetails = await _groupsService.GetGroupById(id);
            if (groupDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {groupDetails.StatusCode}, {groupDetails.Description}");
            }

            var groupDropdownsValues = await _groupsService.GetNewGroupDropdownsValues();
            ViewBag.Faculties = new SelectList(groupDropdownsValues.Data.Faculties, "Id", "Name");

            return View(groupDetails.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewGroupModel groupVM)
        {
            if (id != groupVM.Id)
            {
                return View("Error", "Not found");
            }

            if (!ModelState.IsValid)
            {
                var groupDropdownsValues = await _groupsService.GetNewGroupDropdownsValues();
                ViewBag.Faculties = new SelectList(groupDropdownsValues.Data.Faculties, "Id", "Name");

                return View(groupVM);
            }

            var response = await _groupsService.UpdateGroup(groupVM);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var groupDetails = await _groupsService.GetGroupWithIncludePropertiesById(id);
            if (groupDetails.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {groupDetails.StatusCode}, {groupDetails.Description}");
            }

            return View(groupDetails.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var groupDetails = await _groupsService.GetGroupWithIncludePropertiesById(id);
            var response = await _groupsService.DeleteGroup(id);
            if (response.StatusCode != Core.Enums.StatusCode.OK)
            {
                return View("Error", $"Error {response.StatusCode}, {response.Description}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
