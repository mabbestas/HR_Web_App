using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Services.AppUserService;
using HR_PLUS.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace HR_PLUS.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAppUserService appUserService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByEmailAsync(model.Email);
                if (appUser.Status != Status.Passive && appUser.WorkingSituation != false)
                {
                    var result = await _appUserService.Login(model);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Mesaj = "Email or password is incorrect";
                    }
                }
                else
                {
                    ViewBag.Mesaj = "Your membership has been cancelled. Contact Your Manager";
                }
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccesDenied()
        {
            return View();
        }

        public IActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordDTO model)

        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string password = Guid.NewGuid().ToString();
                PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
                user.PasswordHash = ph.HashPassword(user, password);
                user.FirstLogin = true;
                await _userManager.UpdateAsync(user);

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                NetworkCredential credential = new NetworkCredential("hrplus.no-reply@hotmail.com", "hr.123456");
                client.EnableSsl = true;
                client.Credentials = credential;

                MailMessage msg = new MailMessage("hrplus.no-reply@hotmail.com", user.Email);
                msg.Subject = "Şifre Güncelleme Talebi";
                msg.Body = $"<a>Yeni Şifreniz : {password}</a>"; ;
                msg.IsBodyHtml = true;
                client.Send(msg);
                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View();
        }
    }
}
