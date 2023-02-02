using HR_PLUS.Application.Models.VMs.DashboardVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.DashboardService
{
    public interface IDashboardService
    {
        Task<DashboardVM> GetDashboard(int CurrentUserId);
        Task<DashboardVM> GetDashboardManager(int CompanyId);
    }
}
