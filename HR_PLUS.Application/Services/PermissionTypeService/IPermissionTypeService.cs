using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.PermissionTypeService
{
    public interface IPermissionTypeService
    {
        Task<List<PermissionTypeVM>> GetPermissionTypes();
        //Task Create(CreateGenreDTO model);
        //Task Update(UpdateGenreDTO model);
        //Task Delete(int id);
        //Task<UpdateGenreDTO> GetById(int id);
    }
}
