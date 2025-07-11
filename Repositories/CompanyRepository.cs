using PersonsApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PersonsApi.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private static List<Company> _companies = new List<Company>
        {
            new Company { Id = 1, Name = "Company 1" },
            new Company { Id = 2, Name = "Company 2" },
            new Company { Id = 3, Name = "Company 3" }
        };

        public Company GetCompanyById(int id)
        {
            return new Company
            {
                Id = id,
                Name = $"Company {id}"
            };
        }

        public List<Company> GetCompanies()
        {
            var company1 = new Company
            {
                Id = 1,
                Name = "Company 1"
            };
            var company2 = new Company
            {
                Id = 2,
                Name = "Company 2"
            };
            var company3 = new Company
            {
                Id = 3,
                Name = "Company 3"
            };
            return new List<Company>()
            {
                company1,
                company2,
                company3
            };
        }

        public void CreateCompany(Company company)
        {
            company.Id = _companies.Count > 0 ? _companies.Max(c => c.Id) + 1 : 1;
            _companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            var existing = _companies.FirstOrDefault(c => c.Id == company.Id);
            if (existing != null)
            {
                existing.Name = company.Name;
            }
        }

        public void DeleteCompany(int id)
        {
            var company = _companies.FirstOrDefault(c => c.Id == id);
            if (company != null)
            {
                _companies.Remove(company);
            }
        }
    }
} 