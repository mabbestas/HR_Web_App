using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.ManagerService
{
    public class ManagerService : IManagerService
    {

        private readonly IAppUserRepository _appUserRepository;

        public ManagerService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<List<ManagerVM>> GetManagers()
        {
            var appUser = await _appUserRepository.GetFilteredList(
               select: x => new ManagerVM
               {

                   AppUserId = x.Id,
                   FullName = x.Name + " " + x.Surname,
                   Email = x.Email,
                   CompanyName = x.Company.CompanyName
               },
               where: x => x.Status != Status.Passive && x.IsManager == true);
            return appUser;
        }
    }
}
