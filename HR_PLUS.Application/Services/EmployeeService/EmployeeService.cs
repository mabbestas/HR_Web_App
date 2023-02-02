using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Services.AppUserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAppUserService _appUserService;

        public EmployeeService(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        public async Task<GetEmployeesVM> GetEmployeesCompany(int id)
        {
            var  employees = new GetEmployeesVM()
            {
                EmployeesActive = await _appUserService.GetEmployees(id),
                EmployeesInactive = await _appUserService.GetEmployeesInactive(id)
            };
            return employees;
        }    
    }
}
