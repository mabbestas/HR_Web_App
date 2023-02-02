using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.PermissionTypeService
{
    public class PermissionTypeService : IPermissionTypeService
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public PermissionTypeService(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }

        public async Task<List<PermissionTypeVM>> GetPermissionTypes()
        {
            return await _permissionTypeRepository.GetFilteredList(
                select: x => new PermissionTypeVM
                {
                    PermissionTypeId = x.PermissionTypeId,
                    PermissionTypeName = x.PermissionTypeName,
                    
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.PermissionTypeName)
                );
        }
    }
}
