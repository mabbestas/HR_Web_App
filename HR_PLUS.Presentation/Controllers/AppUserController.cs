using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using ClosedXML.Excel;
using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_PLUS.Application.Models;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Services.AppUserService;
using HR_PLUS.Application.Services.EmployeeService;
using HR_PLUS.Application.Services.ManagerService;
using HR_PLUS.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace HR_PLUS.Presentation.Controllers
{

    public class AppUserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmployeeService _employeeService;
        private readonly IManagerService _managerService;


        public AppUserController(IAppUserService appUserService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmployeeService employeeService, IManagerService managerService)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _signInManager = signInManager;
            _employeeService = employeeService;
            _managerService = managerService;
        }
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> AppUserIndex()
        {

            AppUser appUser = await _userManager.GetUserAsync(User);
            AppUserDetailsVM model = await _appUserService.GetAppUserDetails(appUser.Id);


            return View(model);

        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetEmployees(int p = 1)
        {
            AppUser appUser = await _userManager.GetUserAsync(User);

            return View(await _employeeService.GetEmployeesCompany(appUser.CompanyId));

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetManager()
        {
            return View(await _managerService.GetManagers());
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> AppUserUpdate(int id) => View(await _appUserService.GetById(id));
        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> AppUserUpdate(UpdateAppUserDTO model)
        {

            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());

                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.Email = model.Email;



                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);

            }
            return RedirectToAction("AppUserIndex", "AppUser");

        }
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            user.Status = Status.Passive;
            IdentityResult result = await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("GetEmployees");

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            user.Status = Status.Passive;
            IdentityResult result = await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("GetManager");

        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Activate(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            user.Status = Status.Modified;
            user.WorkingSituation = true;
            IdentityResult result = await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("GetEmployees");

        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(CreateAppUserEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                CreateAppUserEmployeeDTO model2 = await _appUserService.GetEmployeeByEmail(model.Email);
                if (model2 != null)
                {
                    ViewBag.Mesaj = "Mail adresi kullanılıyor.";
                }
                else
                {
                    PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
                    AppUser manager = await _userManager.GetUserAsync(User);
                    AppUser user = new AppUser();
                    string password = Guid.NewGuid().ToString();
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Gender = model.Gender;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.CreateDate = DateTime.Now;
                    user.Status = Status.Active;
                    user.CompanyId = manager.CompanyId;
                    user.FirstLogin = true;
                    user.Address = model.Address;
                    user.EmailConfirmed = true;
                    user.UserName = model.Email;
                    user.HireDate = model.HireDate;
                    user.BirthDate = model.BirthDate;
                    user.WorkingSituation = true;
                    user.IsManager = false;



                    IdentityResult result = await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, "Employee");
                    if (!result.Succeeded)
                    {

                        return View(model);
                    }
                    else
                    {
                        SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;

                        NetworkCredential credential = new NetworkCredential("hrplus.no-reply@hotmail.com", "hr.123456");
                        client.EnableSsl = true;
                        client.Credentials = credential;

                        MailMessage msg = new MailMessage("hrplus.no-reply@hotmail.com", user.Email);
                        msg.Subject = "Well Come HR PLUS";
                        msg.Body = $"<a>Kullanıcı Adınız : {model.Email}</a> <a>Şifreniz : {password}</a>";
                        msg.IsBodyHtml = true;
                        client.Send(msg);
                        return RedirectToAction("GetEmployees", "AppUser");
                    }
                }

            }



            return View(model);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateManager(CreateAppUserManagerDTO model)
        {
            if (ModelState.IsValid)
            {
                CreateAppUserEmployeeDTO model2 = await _appUserService.GetEmployeeByEmail(model.Email);
                if (model2 != null)
                {
                    ViewBag.Mesaj = "Mail adresi kullanılıyor.";
                }
                else
                {
                    PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
                    AppUser manager = await _userManager.GetUserAsync(User);
                    AppUser user = new AppUser();
                    string password = Guid.NewGuid().ToString();
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Gender = model.Gender;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.CreateDate = DateTime.Now;
                    user.Status = Status.Active;
                    user.CompanyId = model.CompanyId;
                    user.FirstLogin = true;
                    user.Address = model.Address;
                    user.EmailConfirmed = true;
                    user.UserName = model.Email;
                    user.HireDate = model.HireDate;
                    user.BirthDate = model.BirthDate;
                    user.WorkingSituation = true;
                    user.IsManager = true;



                    IdentityResult result = await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, "Manager");
                    if (!result.Succeeded)
                    {

                        return View(model);
                    }
                    else
                    {
                        SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;

                        NetworkCredential credential = new NetworkCredential("hrplus.no-reply@hotmail.com", "hr.123456");
                        client.EnableSsl = true;
                        client.Credentials = credential;

                        MailMessage msg = new MailMessage("hrplus.no-reply@hotmail.com", user.Email);
                        msg.Subject = "Well Come HR PLUS";
                        msg.Body = $"<a>Kullanıcı Adınız : {model.Email}</a><br><a>Şifreniz : {password}</a>";
                        msg.IsBodyHtml = true;
                        client.Send(msg);
                        return RedirectToAction("Index", "Home");
                    }
                }

            }



            return View(model);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateManager()
        {
            return View();
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> ChangePassword(int id)
        {
            return View(await _appUserService.GetByIdForPassword(id));
        }
        [Authorize(Roles = "Employee,Manager")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UpdateAppUserPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());

                var response = Request.Form["g-recaptcha-response"];
                const string secret = "6LcTWwciAAAAAL6E_6cJUx6A6Lw1q_EhwlE8oeag";


                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                if (!captchaResponse.Success)
                    TempData["Message"] = "Lütfen güvenliği doğrulayınız.";
                else
                {

                    PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
                    user.PasswordHash = ph.HashPassword(user, model.Password);
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                        return View(model);
                    }
                    await _userManager.UpdateSecurityStampAsync(user);
                    return RedirectToAction("AppUserIndex", "AppUser");

                }
            }

            return View(model);
        }
        public async Task<IActionResult> ResetPassword()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            return View(await _appUserService.GetByIdForPassword(appUser.Id));
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(UpdateAppUserPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());

                PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
                user.PasswordHash = ph.HashPassword(user, model.Password);
                user.FirstLogin = false;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }

                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }

        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AppUserEmployeeUpdate(int id) => View(await _appUserService.GetEmployeeById(id));
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> AppUserEmployeeUpdate(UpdateAppUserEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());



                user.Name = model.Name;
                user.Surname = model.Surname;
                user.BirthDate = model.BirthDate;
                user.HireDate = model.HireDate;
                user.Gender = model.Gender;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.Email = model.Email;
                user.CreateDate = model.CreateDate;
                user.UpdateDate = DateTime.Now;
                user.Status = Status.Modified;


                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);



            }
            return RedirectToAction("GetEmployees", "AppUser");



        }
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Inactive(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            user.WorkingSituation = false;
            IdentityResult result = await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction("GetEmployees");

        }
        public async Task<IActionResult> ExportExcelEmployeeList()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employee List");
                worksheet.Cell(1, 1).Value = "Employee ID";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Surname";
                worksheet.Cell(1, 4).Value = "Address";
                worksheet.Cell(1, 5).Value = "Phone Number";
                worksheet.Cell(1, 6).Value = "Hire Date";
                worksheet.Cell(1, 7).Value = "Birth Date";

                int rowCount = 2;
                foreach (var item in EmployeeList())
                {
                    if (item.CompanyId == appUser.CompanyId && item.WorkingSituation == true)
                    {
                        worksheet.Cell(rowCount, 1).Value = item.Id;
                        worksheet.Cell(rowCount, 2).Value = item.Name;
                        worksheet.Cell(rowCount, 3).Value = item.Surname;
                        worksheet.Cell(rowCount, 4).Value = item.Address;
                        worksheet.Cell(rowCount, 5).Value = item.PhoneNumber;
                        worksheet.Cell(rowCount, 6).Value = item.HireDate;
                        worksheet.Cell(rowCount, 7).Value = item.BirthDate;
                        rowCount++;
                    }

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application / vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeList.xlsx");
                }

            }


        }
        public List<ExportEmployee> EmployeeList()
        {
            List<ExportEmployee> ee = new List<ExportEmployee>();
            using (var c = new AppDbContext())
            {
                ee = c.AppUsers.Select(x => new ExportEmployee
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    BirthDate = x.BirthDate,
                    HireDate = x.HireDate,
                    CompanyId = x.CompanyId,
                    WorkingSituation = x.WorkingSituation
                }).ToList();
            }
            return ee;
        }

    }
}
