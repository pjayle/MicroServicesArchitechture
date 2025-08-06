
using boilerplate.web.Models;
using boilerplate.web.Models.Dto;
using boilerplate.web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace boilerplate.web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            List<MRoles>? list = new List<MRoles>();

            APIResponseDto? response = await _roleService.GetAllAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MRoles>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int pkid = 0;
            if (id != null && id > 0)
            {
                pkid = (int)id;
            }

            APIResponseDto? response = await _roleService.GetByIdAsync(pkid);
            MRoles? mRoles = new MRoles();
            if (response != null && response.IsSuccess)
            {
                mRoles = JsonConvert.DeserializeObject<MRoles>(Convert.ToString(response.Result));
                return View(mRoles);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(mRoles);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] MRoles mRoles)
        {
            if (ModelState.IsValid)
            {
                APIResponseDto? response = await _roleService.CreateAsync(mRoles);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(mRoles);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int pkid = 0;
            if (id != null && id > 0)
            {
                pkid = (int)id;
            }

            APIResponseDto? response = await _roleService.GetByIdAsync(pkid);
            MRoles? mUser = new MRoles();
            if (response != null && response.IsSuccess)
            {
                mUser = JsonConvert.DeserializeObject<MRoles>(Convert.ToString(response.Result));
                return View(mUser);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] MRoles mRoles)
        {
            if (id != mRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    APIResponseDto? response = await _roleService.UpdateAsync(mRoles);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "update successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["error"] = response?.Message;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MRolesExists(mRoles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mRoles);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            APIResponseDto? response = await _roleService.GetByIdAsync((int)id);
            var mRole = new MRoles();
            if (response != null && response.IsSuccess)
            {
                mRole = JsonConvert.DeserializeObject<MRoles>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return NotFound();
            }
            if (mRole == null)
            {
                return NotFound();
            }

            return View(mRole);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            APIResponseDto? response = await _roleService.GetByIdAsync((int)id);
            var mUser = new MRoles();
            if (response != null && response.IsSuccess)
            {
                mUser = JsonConvert.DeserializeObject<MRoles>(Convert.ToString(response.Result));
                TempData["success"] = "delete successfully";
            }
            else
            {
                TempData["error"] = response?.Message;
                return NotFound();
            }


            if (mUser != null)
            {
                APIResponseDto? delresponse = await _roleService.DeleteAsync(id);
                if (delresponse != null && delresponse.IsSuccess)
                {
                    mUser = JsonConvert.DeserializeObject<MRoles>(Convert.ToString(response.Result));
                   
                    TempData["success"] = "delete successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MRolesExists(int id)
        {
            return false;
            //return _context.Roles.Any(e => e.Id == id);
        }
    }
}
