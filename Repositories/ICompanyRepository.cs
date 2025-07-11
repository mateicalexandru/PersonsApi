using PersonsApi.Entities;
using System.Collections.Generic;

namespace PersonsApi.Repositories
{
    public interface ICompanyRepository
    {
        Company GetCompanyById(int id);
        List<Company> GetCompanies();
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(int id);
    }
} 