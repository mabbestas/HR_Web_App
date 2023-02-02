using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.AppUserService
{
    public interface IAppUserService
    {

        Task<SignInResult> Login(LoginDTO model);

        //Task UpdateEmployee(UpdateEmployeeDTO model);
        Task<AppUserDetailsVM> GetAppUserDetails(int id);
        Task<List<EmployeesVM>> GetEmployees(int id);

        Task<List<EmployeesVM>> GetEmployeesInactive(int id);
        Task<UpdateAppUserDTO> GetById(int id);
        Task<UpdateAppUserPasswordDTO> GetByIdForPassword(int id);
        Task<UpdateAppUserDTO> GetByUserName(string userName);
        Task UpdateAppUser(UpdateAppUserDTO model);

        Task<UpdateAppUserEmployeeDTO> GetEmployeeById(int id);
        Task<CreateAppUserEmployeeDTO> GetEmployeeByEmail(string email);

    }
}
