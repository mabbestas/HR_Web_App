using AutoMapper;
using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using HR_PLUS.Application.Models.VMs.DashboardVMs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper, IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _permissionTypeRepository = permissionTypeRepository;
        }

        public async Task Create(CreatePermissionDTO model)
        {
            var permission = _mapper.Map<Permission>(model);
            await _permissionRepository.Create(permission);
        }

        public async Task<CreatePermissionDTO> CreatePermission()
        {
            CreatePermissionDTO model = new CreatePermissionDTO()
            {
                PermissionTypes = await _permissionTypeRepository.GetFilteredList(
                    select: x => new PermissionTypeVM
                    {
                        PermissionTypeId = x.PermissionTypeId,
                        PermissionTypeName = x.PermissionTypeName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.PermissionTypeName))
            };
            return model;
        }

        public async Task Delete(int id)
        {
            Permission permission = await _permissionRepository.GetDefault(x => x.PermissionId == id);
            permission.DeleteDate = DateTime.Now;
            permission.Status = Status.Passive;
            await _permissionRepository.Delete(permission);
        }

        public async Task<UpdatePermissionDTO> GetById(int id)
        {
            var permission = await _permissionRepository.GetFilteredFirstOrDefault(
                select: x => new PermissionVM
                {
                    PermissionId = x.PermissionId,
                    PermissionDescription = x.PermissionDescription,
                    PermissionStartDate = x.PermissionStartDate,
                    PermissionExpiryDate = x.PermissionExpiryDate,
                    PermissionTypeId = x.PermissionTypeId,
                    CreateDate = x.CreateDate

                },
                where: x => x.PermissionId == id);

                var model = _mapper.Map<UpdatePermissionDTO>(permission);
                model.PermissionTypes = await _permissionTypeRepository.GetFilteredList(
                select: x => new PermissionTypeVM
                {
                    PermissionTypeId = x.PermissionTypeId,
                    PermissionTypeName = x.PermissionTypeName

                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.PermissionTypeName));
            return model;

            //Permission permission = await _permissionRepository.GetDefault(x => x.PermissionId == id);

            //var model = _mapper.Map<UpdatePermissionDTO>(permission);
            //return model;
        }

        public async Task<List<PermissionVM>> GetPermissions(int CurrentUserId)
        {
            var permissions = await _permissionRepository.GetFilteredList(
                select: x => new PermissionVM
                {
                    PermissionId = x.PermissionId,
                    PermissionTypeName = x.PermissionType.PermissionTypeName,
                    PermissionDescription = x.PermissionDescription,
                    PermissionExpiryDate = x.PermissionExpiryDate,
                    PermissionStartDate = x.PermissionStartDate

                },
                 where: x => x.Status != Status.Passive && x.AppUserId == CurrentUserId,
                orderBy: x => x.OrderByDescending(x => x.PermissionId),
                include: x => x.Include(x => x.PermissionType)
                );
            return permissions;
        }

        public async Task<List<PermissionVM>> GetPermissionsTake(int take, int CurrentUserId)
        {
            var permissions = await _permissionRepository.GetFilteredList(
            select: x => new PermissionVM
            {
                PermissionId = x.PermissionId,
                PermissionTypeName = x.PermissionType.PermissionTypeName,
                PermissionDescription = x.PermissionDescription,
                PermissionExpiryDate = x.PermissionExpiryDate,
                PermissionStartDate = x.PermissionStartDate

            },
             where: x => x.Status != Status.Passive && x.AppUserId == CurrentUserId,
            orderBy: x => x.Take(take).OrderByDescending(x => x.PermissionId),
            include: x => x.Include(x => x.PermissionType)
            );
            return permissions;
        }

        public async Task<List<PermissionVM>> MenegerGetPermissionsTake(int take, int CompanyId)
        {
            var permissions = await _permissionRepository.GetFilteredList(
                select: x => new PermissionVM
                {
                    PermissionId = x.PermissionId,
                    PermissionTypeName = x.PermissionType.PermissionTypeName,
                    PermissionDescription = x.PermissionDescription,
                    PermissionExpiryDate = x.PermissionExpiryDate,
                    PermissionStartDate = x.PermissionStartDate,
                    FullName = x.AppUser.Name + " " + x.AppUser.Surname
                },
                 where: x => x.Status != Status.Passive && x.AppUser.CompanyId == CompanyId,
                orderBy: x => x.OrderByDescending(x => x.PermissionId),
                include: x => x.Include(x => x.PermissionType)
                );
            return permissions;
        }

        public async Task Update(UpdatePermissionDTO model)
        {
            var permission = _mapper.Map<Permission>(model);
            await _permissionRepository.Update(permission);
        }
    }
}
