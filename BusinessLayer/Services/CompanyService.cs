
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.Threading.Tasks;
using System;

namespace BusinessLayer.Services
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
        
        public async Task< CompanyInfo> GetCompanyByCode(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }


        public async Task SaveCompany(CompanyInfo companyInfo)
        {
            var res = _mapper.Map<Company>(companyInfo);
            await _companyRepository.SaveCompany(res);
        }

        public async Task UpdateCompany(string companyId, CompanyInfo companyInfo)
        {
            var company = await GetCompanyByCode(companyId);
            if (company ==null)
            {
                throw new Exception($"Company code {companyId} doesn't exist");
            }
            else
            {
                var targetCompany=_mapper.Map<Company>(companyInfo);
                await _companyRepository.SaveCompany(targetCompany); //resue savecompany method in repository level
            }
        }

        public async Task DeleteCompany(string companyId)
        {
            await _companyRepository.DeleteCompany(companyId);
        }
    }
}
