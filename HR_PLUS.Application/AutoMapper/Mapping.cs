using AutoMapper;
using HR_Plus.Domain.Entities;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Models.VMs.DashboardVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Permission, CreatePermissionDTO>().ReverseMap();
            CreateMap<Permission, PermissionVM>().ReverseMap();
            CreateMap<Permission, UpdatePermissionDTO>().ReverseMap();
            CreateMap<Permission, DashboardVM>().ReverseMap();

            CreateMap<AdvancePaymentVM, PermissionVM>().ReverseMap();
            CreateMap<UpdatePermissionDTO, PermissionVM>().ReverseMap();
            CreateMap<PermissionType, PermissionTypeVM>().ReverseMap();

            CreateMap<AdvancePayment, AdvancePaymentVM>().ReverseMap();
            CreateMap<AdvancePayment, CreateAdvancePaymentDTO>().ReverseMap();
            CreateMap<AdvancePayment, UpdateAdvancePaymentDTO>().ReverseMap();

            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, AppUserDetailsVM>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdatePasswordDTO>().ReverseMap();
            CreateMap<AppUser, ResetPasswordDTO>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserPasswordDTO>().ReverseMap();
            CreateMap<AppUser, EmployeesVM>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserEmployeeDTO>().ReverseMap();
            CreateMap<AppUser, CreateAppUserEmployeeDTO>().ReverseMap();
            CreateMap<AppUser, CreateAppUserManagerDTO>().ReverseMap();
            CreateMap<AppUser, ManagerVM>().ReverseMap();

            CreateMap<Expense, CreateExpenseDTO>().ReverseMap();
            CreateMap<Expense, UpdateExpenseDTO>().ReverseMap();
            CreateMap<Expense, ExpenseVM>().ReverseMap();

            CreateMap<Company, CompaniesVM>().ReverseMap();
            CreateMap<Company, CreateCompanyDTO>().ReverseMap();
            CreateMap<Company, UpdateCompanyDTO>().ReverseMap();
        }
    }
}
