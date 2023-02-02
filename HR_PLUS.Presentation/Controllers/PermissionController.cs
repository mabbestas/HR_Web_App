
using HR_Plus.Domain.Entities;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Services.PermissionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_PLUS.Presentation.Controllers
{
    
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly UserManager<AppUser> _userManager;
        public PermissionController(IPermissionService permissionService, UserManager<AppUser> userManager)
        {
            _permissionService = permissionService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> PermissionIndex()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            return View(await _permissionService.GetPermissions(appUser.Id));
        }



        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> PermissionCreate() => View(await _permissionService.CreatePermission());
        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> PermissionCreate(CreatePermissionDTO model)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;
            if (ModelState.IsValid && model.PermissionTypeId>=1)
            {
                await _permissionService.Create(model);
                System.Threading.Thread.Sleep(1000);
               return RedirectToAction("PermissionIndex");
            }
            else
            {
               var permission = await _permissionService.CreatePermission();

                TempData["Error"] = "Permission has not been added!";
                model.PermissionTypes = permission.PermissionTypes;
                return View(model);
                

            }
        }
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            

            await _permissionService.Delete(id);
            System.Threading.Thread.Sleep(1000);
            return RedirectToAction("PermissionIndex");
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> PermissionUpdate(int id) => View(await _permissionService.GetById(id));
        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> PermissionUpdate(UpdatePermissionDTO model)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;
            

            if (ModelState.IsValid)
            {
                await _permissionService.Update(model);
                TempData["Success"] = "Permissin has been updated!";
                System.Threading.Thread.Sleep(1000);
                return RedirectToAction("PermissionIndex");
            }
            else
            {
                
                TempData["Error"] = "Permissin has not been updated!";
                return View(model);
            }




        }
    }
}
