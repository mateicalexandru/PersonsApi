using PersonsApi.Entities;
using MySqlConnector;

namespace PersonsApi.Repositories
{
    public class CompanyAdoNetRepository : ICompanyRepository
    {
        private readonly string _connectionString;
        public CompanyAdoNetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Company GetCompanyById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT Id, Name FROM Company WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Company
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name")
                };
            }
            return null;
        }

        public List<Company> GetCompanies()
        {
            var companies = new List<Company>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT Id, Name FROM Company", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                companies.Add(new Company
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name")
                });
            }
            return companies;
        }

        public void CreateCompany(Company company)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO Company (Name) VALUES (@name); SELECT LAST_INSERT_ID();", conn);
            cmd.Parameters.AddWithValue("@name", company.Name);
            company.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void UpdateCompany(Company company)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE Company SET Name = @name WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", company.Id);
            cmd.Parameters.AddWithValue("@name", company.Name);
            cmd.ExecuteNonQuery();
        }

        public void DeleteCompany(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM Company WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
} 