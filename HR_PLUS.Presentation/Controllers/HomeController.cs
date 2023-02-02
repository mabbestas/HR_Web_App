using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Plus.Domain.Entities;
using HR_PLUS.Application.Models.VMs.DashboardVMs;
using HR_PLUS.Application.Services.DashboardService;
using HR_PLUS.Application.Services.PermissionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLUS.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(IDashboardService dashboardService, UserManager<AppUser> userManager)
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Employee,Manager,Admin")]
        public async Task<IActionResult> Index()
        {

            AppUser appUser = await _userManager.GetUserAsync(User);
            if (appUser.FirstLogin == true)
            {
                return RedirectToAction("ResetPassword", "AppUser");
            }
            else
            {
                DashboardVM model = await _dashboardService.GetDashboard(appUser.Id);
                return View(model);
            }


        }
       
    }
}
