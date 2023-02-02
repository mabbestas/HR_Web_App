using AutoMapper;
using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public AppUserService(SignInManager<AppUser> signInManager, IAppUserRepository appUserRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppUserDetailsVM> GetAppUserDetails(int CurrentUserId)
        {
            var appUser = await _appUserRepository.GetFilteredFirstOrDefault(
               select: x => new AppUserDetailsVM
               {
                   AppUserId = x.Id,
                   Name = x.Name,
                   Surname = x.Surname,
                   FullName = x.Name + " " + x.Surname,
                   Email = x.Email,
                   Address = x.Address,
                   BirthDate = x.BirthDate,
                   HireDate = x.HireDate,
                   PhoneNumber = x.PhoneNumber,
                   CompanyName = x.Company.CompanyName,
                   CompanyAddress = x.Company.Adress,
                   CompanyEmail = x.Company.CompanyEmail,
                   CompanyPhoneNumber = x.Company.PhoneNumber,
                   CompanyTaxIdentificationNumber = x.Company.TaxIdentificationNumber,
                   CompanyId = x.CompanyId

               },
               where: x => x.Id == CurrentUserId);
            return appUser;
        }

        public async Task<UpdateAppUserDTO> GetById(int id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateAppUserDTO>(appUser);
            return model;
        }
        public async Task<UpdateAppUserPasswordDTO> GetByIdForPassword(int id)
        {

            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateAppUserPasswordDTO>(appUser);
            return model;
        }

        public async Task<UpdateAppUserDTO> GetByUserName(string userName)
        {
            var result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateAppUserDTO
                {
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                },
                where: x => x.UserName == userName);
            return result;
        }

        public async Task<CreateAppUserEmployeeDTO> GetEmployeeByEmail(string email)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Email == email && x.Status != Status.Passive);
            var model = _mapper.Map<CreateAppUserEmployeeDTO>(appUser);
            return model;
        }

        public async Task<UpdateAppUserEmployeeDTO> GetEmployeeById(int id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdateAppUserEmployeeDTO>(appUser);
            return model;
        }

        public async Task<List<EmployeesVM>> GetEmployees(int id)
        {
            var appUser = await _appUserRepository.GetFilteredList(
               select: x => new EmployeesVM
               {
                   AppUserId = x.Id,
                   FullName = x.Name + " " + x.Surname,
                   Email = x.Email
               },
               where: x => x.Status != Status.Passive && x.Company.CompanyId == id && x.WorkingSituation == true);
            return appUser;
        }

        public async Task<List<EmployeesVM>> GetEmployeesInactive(int id)
        {
            var appUser = await _appUserRepository.GetFilteredList(
               select: x => new EmployeesVM
               {
                   AppUserId = x.Id,
                   FullName = x.Name + " " + x.Surname,
                   Email = x.Email
               },
               where: x => x.Status != Status.Passive && x.Company.CompanyId == id && x.WorkingSituation == false);
            return appUser;
        }

        public Task<SignInResult> Login(LoginDTO model)
        {
            var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            return result;
        }

        public async Task UpdateAppUser(UpdateAppUserDTO model)
        {
            var user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            await _userManager.UpdateAsync(user);
        }
    }
}
