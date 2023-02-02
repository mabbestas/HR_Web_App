using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.CompanyService
{
   public interface ICompanyService
    {
        Task Update(UpdateCompanyDTO model);
        Task<UpdateCompanyDTO> GetById(int id);
        Task<CreateCompanyDTO> GetCompanyByEmail(string email);
        Task Create(CreateCompanyDTO model);
        Task<List<CompaniesVM>> GetCompanies();
        Task Delete(int id);
    }
}
