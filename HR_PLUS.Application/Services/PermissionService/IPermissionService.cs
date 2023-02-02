using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Models.VMs.DashboardVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.PermissionService
{
    public interface IPermissionService
    {
        Task Create(CreatePermissionDTO model);
        Task<CreatePermissionDTO> CreatePermission();
        Task<List<PermissionVM>> GetPermissions(int CurrentUserId);
        Task Delete(int id);
        Task<UpdatePermissionDTO> GetById(int id);
        Task Update(UpdatePermissionDTO model);
        Task<List<PermissionVM>> GetPermissionsTake(int take, int CurrentUserId);
        Task<List<PermissionVM>> MenegerGetPermissionsTake(int take, int CompanyId);
    }
}
