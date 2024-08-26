using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        //change to async and add more method to align with controller
        Task<IEnumerable<CompanyInfo>> GetAllCompanies();
        Task <CompanyInfo> GetCompanyByCode(string companyCode);
        Task SaveCompany(CompanyInfo companyInfo);
        Task UpdateCompany(string companyId, CompanyInfo companyInfo);
        Task DeleteCompany(string companyId);

    }
}
