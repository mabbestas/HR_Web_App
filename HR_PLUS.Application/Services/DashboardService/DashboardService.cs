using AutoMapper;
using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Models.VMs.DashboardVMs;
using HR_PLUS.Application.Services.AdvancePaymentService;
using HR_PLUS.Application.Services.ExpenseService;
using HR_PLUS.Application.Services.PermissionService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {

        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IAdvancePaymentService _advancePaymentService;
        private readonly IPermissionService _permissionService;
        private readonly IExpenseService _expenseService;
        private readonly IExpenseRepository _expenseRepository;

        public DashboardService(IAdvancePaymentService advancePaymentService, IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository, IPermissionService permissionService, IExpenseRepository expenseRepository,IExpenseService expenseService)
        {
            _permissionRepository = permissionRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _permissionService = permissionService;
            _advancePaymentService = advancePaymentService;
            _expenseService = expenseService;
            _expenseRepository = expenseRepository;
        }

        public async Task<DashboardVM> GetDashboard(int CurrentUserId)
        {           
            var dashbordVM = new DashboardVM()
            {
                Permissions = await _permissionService.GetPermissionsTake(3, CurrentUserId),
                AdvancePayments = await _advancePaymentService.GetAdvancePaymentTake(3, CurrentUserId),
                Expenses = await _expenseService.GetExpenseTake(3, CurrentUserId)
            };
            return dashbordVM;
        }

        public async Task<DashboardVM> GetDashboardManager(int CompanyId)
        {
            var dashbordVM = new DashboardVM()
            {
                Permissions = await _permissionService.MenegerGetPermissionsTake(3, CompanyId)              
            };
            return dashbordVM;
        }
    }
}
