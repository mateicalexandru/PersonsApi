using System.Collections.Generic;
using PersonsApi.Entities;

namespace PersonsApi.Services
{
    public interface ICompanyService
    {
        Company GetCompanyById(int id);
        List<Company> GetCompanies();
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(int id);
    }
} 