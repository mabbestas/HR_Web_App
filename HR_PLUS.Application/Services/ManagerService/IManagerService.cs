using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.ManagerService
{
    public interface IManagerService
    {
        Task<List<ManagerVM>> GetManagers();
    }
}
