using HR_Plus.Domain.Entities;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Services.AdvancePaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_PLUS.Presentation.Controllers
{
    public class AdvancePaymentController : Controller
    {
        private readonly IAdvancePaymentService _advancepaymentService;
        private readonly UserManager<AppUser> _userManager;

        public AdvancePaymentController(IAdvancePaymentService advancepaymentService, UserManager<AppUser> userManager)
        {
            _advancepaymentService = advancepaymentService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task <IActionResult> AdvancePaymentIndex()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            return View(await _advancepaymentService.GetAdvancePayments(appUser.Id));
        }

        [Authorize(Roles = "Employee,Manager")]
        public IActionResult AdvancePaymentCreate()
        {
            return View();
        }

        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> AdvancePaymentCreate(CreateAdvancePaymentDTO model)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;

            if (ModelState.IsValid )
            {
                await _advancepaymentService.Create(model);
                
                return RedirectToAction("AdvancePaymentIndex");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _advancepaymentService.Delete(id);
            return RedirectToAction("AdvancePaymentIndex");
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> AdvancePaymentUpdate(int id) => View(await _advancepaymentService.GetById(id));

        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> AdvancePaymentUpdate(UpdateAdvancePaymentDTO model)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;
            if (ModelState.IsValid)
            {
                await _advancepaymentService.Update(model);
                TempData["Success"] = "Permissin has been updated!";
                return RedirectToAction("AdvancePaymentIndex");
            }
            else
            {
                TempData["Error"] = "Permissin has not been updated!";
                return View(model);
            }
        }
    }
}
