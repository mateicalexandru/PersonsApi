using System.Collections.Generic;
using PersonsApi.Entities;
using PersonsApi.Repositories;

namespace PersonsApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Company GetCompanyById(int id)
        {
            return _companyRepository.GetCompanyById(id);
        }

        public List<Company> GetCompanies()
        {
            return _companyRepository.GetCompanies();
        }

        public void CreateCompany(Company company)
        {
            _companyRepository.CreateCompany(company);
        }

        public void UpdateCompany(Company company)
        {
            _companyRepository.UpdateCompany(company);
        }

        public void DeleteCompany(int id)
        {
            _companyRepository.DeleteCompany(id);
        }
    }
} 