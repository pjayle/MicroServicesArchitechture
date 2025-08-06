using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using boilerplate.web.Data;
using boilerplate.web.Models;
using boilerplate.web.Services;
using boilerplate.web.Models.Dto;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace boilerplate.web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            List<MUser>? list = new List<MUser>();

            APIResponseDto? response = await _userService.GetAllAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MUser>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        // GET: User/Details/5
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

            APIResponseDto? response = await _userService.GetByIdAsync(pkid);
            MUser? mUser = new MUser();
            if (response != null && response.IsSuccess)
            {
                mUser = JsonConvert.DeserializeObject<MUser>(Convert.ToString(response.Result));
                return View(mUser);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            ViewData["RolesId"] = BindRole(mUser.RolesId);
            return NotFound();
        }

        private async Task<SelectList> BindRole(int? selectedRole = null)
        {
            APIResponseDto? response = await _roleService.GetAllAsync();

            List<MRoles> lstmRoles = new List<MRoles>();

            if (response != null && response.IsSuccess)
            {
                lstmRoles = JsonConvert.DeserializeObject<List<MRoles>>(Convert.ToString(response.Result));
            }
            return new SelectList(lstmRoles, "Id", "Title", selectedRole);
        }

        // GET: User/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RolesId"] = await BindRole();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Password,RolesId")] MUser mUser)
        {
            if (ModelState.IsValid)
            {
                APIResponseDto? response = await _userService.CreateAsync(mUser);

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
            ViewData["RolesId"] = BindRole(mUser.RolesId);
            return View(mUser);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int pkid = 0;
            if (id != null && id > 0)
            {
                pkid = (int)id;
            }

            APIResponseDto? response = await _userService.GetByIdAsync(pkid);
            MUser? mUser = new MUser();
            if (response != null && response.IsSuccess)
            {
                mUser = JsonConvert.DeserializeObject<MUser>(Convert.ToString(response.Result));
                ViewData["RolesId"] = await BindRole(mUser.RolesId);

                return View(mUser);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }

            //ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Title", mUser.RolesId);
            //return NotFound();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Password,RolesId")] MUser mUser)
        {
            if (id != mUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    APIResponseDto? response = await _userService.UpdateAsync(mUser);

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
                    if (!MUserExists(mUser.Id))
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
            ViewData["RolesId"] = BindRole(mUser.RolesId);
            return View(mUser);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            APIResponseDto? response = await _userService.GetByIdAsync((int)id);
            var mUser = new MUser();
            if (response != null && response.IsSuccess)
            {
                mUser = JsonConvert.DeserializeObject<MUser>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return NotFound();
            }
            return View(mUser);

        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            APIResponseDto? response = await _userService.GetByIdAsync((int)id);
            var mUser = new MUser();
            if (response != null && response.IsSuccess)
            {
                mUser = JsonConvert.DeserializeObject<MUser>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return NotFound();
            }


            if (mUser != null)
            {
                APIResponseDto? delresponse = await _userService.DeleteAsync(id);
                if (delresponse != null && delresponse.IsSuccess)
                {
                    mUser = JsonConvert.DeserializeObject<MUser>(Convert.ToString(response.Result));
                    TempData["success"] = "delete successfully";
                }
                else
                {
                    TempData["error"] = response?.Message;
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MUserExists(int id)
        {
            return false;
            //return _context.Users.Any(e => e.Id == id);
        }
    }
}
