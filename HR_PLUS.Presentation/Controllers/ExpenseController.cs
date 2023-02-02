using HR_Plus.Domain.Entities;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Services.ExpenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace HR_PLUS.Presentation.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService, UserManager<AppUser> userManager)
        {
            _expenseService = expenseService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Employee,Manager")]
        public async Task <IActionResult> ExpenseIndex(int p=1)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            return View((await _expenseService.GetExpenses(appUser.Id)).ToPagedList(p,3));
        }
        [Authorize(Roles = "Employee,Manager")]
        public IActionResult ExpenseCreate()
        {
            return View();
        }
        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> ExpenseCreate(CreateExpenseDTO model)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;



            if (ModelState.IsValid)
            {
                await _expenseService.Create(model);

                return RedirectToAction("ExpenseIndex");
            }
            else
            {
                return View(model);
            }

        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _expenseService.Delete(id);
            return RedirectToAction("ExpenseIndex");
        }
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> ExpenseUpdate(int id) => View(await _expenseService.GetById(id));
        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> ExpenseUpdate(UpdateExpenseDTO model)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.AppUserId = appUser.Id;
            if (ModelState.IsValid)
            {
                await _expenseService.Update(model);
                TempData["Success"] = "Expense has been updated!";
                return RedirectToAction("ExpenseIndex");
            }
            else
            {
                TempData["Error"] = "Expense has not been updated!";
                return View(model);
            }
        }
    }
}
