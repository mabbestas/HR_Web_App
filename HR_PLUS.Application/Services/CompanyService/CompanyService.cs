using AutoMapper;
using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateCompanyDTO model)
        {
            Company company = _mapper.Map<Company>(model);
            await _companyRepository.Create(company);
        }

        public async Task<UpdateCompanyDTO> GetById(int id)
        {
            Company company = await _companyRepository.GetDefault(x => x.CompanyId == id);
            var model = _mapper.Map<UpdateCompanyDTO>(company);
            return model;
        }

        public async Task<List<CompaniesVM>> GetCompanies()
        {
            var companies = await _companyRepository.GetFilteredList(
                select: x => new CompaniesVM
                {
                    CompanyId = x.CompanyId,
                    CompanyEmail = x.CompanyEmail,
                    CompanyName = x.CompanyName,
                    TaxIdentificationNumber = x.TaxIdentificationNumber
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.CompanyId)
                );
            return companies;
        }

        public async Task<CreateCompanyDTO> GetCompanyByEmail(string email)
        {
            Company company = await _companyRepository.GetDefault(x => x.CompanyEmail == email && x.Status != Status.Passive);
            var model = _mapper.Map<CreateCompanyDTO>(company);
            return model;
        }

        public async Task Update(UpdateCompanyDTO model)
        {
            var company = _mapper.Map<Company>(model);
            await _companyRepository.Update(company);
        }

        public async Task Delete(int id)
        {
            Company company = await _companyRepository.GetDefault(x => x.CompanyId == id);
            company.DeleteDate = DateTime.Now;
            company.Status = Status.Passive;
            await _companyRepository.Delete(company);
        }
    }
}
