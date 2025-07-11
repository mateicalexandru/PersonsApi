using System.Data;
using MySqlConnector;
using PersonsApi.Entities;

namespace PersonsApi.Repositories
{
    public class PersonAdoNetRepository : IPersonRepository
    {
        private readonly string _connectionString;
        public PersonAdoNetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Person GetPersonById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand(
                    @"SELECT p.Id, p.Name, c.Id as CompanyId, c.Name as CompanyName 
                    FROM   Person p 
                           LEFT JOIN Company c ON p.CompanyId = c.Id 
                    WHERE p.Id = @id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Person
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Company = new Company
                    {
                        Id = reader.IsDBNull("CompanyId") ? 0 : reader.GetInt32("CompanyId"),
                        Name = reader.IsDBNull("CompanyName") ? null : reader.GetString("CompanyName")
                    }
                };
            }
            return null;
        }

        public List<Person> GetPersons()
        {
            var persons = new List<Person>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand(@"
                    SELECT  p.Id, p.Name, c.Id as CompanyId, c.Name as CompanyName 
                    FROM    Person p 
                            LEFT JOIN Company c ON p.CompanyId = c.Id", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                persons.Add(new Person
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Company = new Company
                    {
                        Id = reader.IsDBNull("CompanyId") ? 0 : reader.GetInt32("CompanyId"),
                        Name = reader.IsDBNull("CompanyName") ? null : reader.GetString("CompanyName")
                    }
                });
            }
            return persons;
        }

        public void CreatePerson(Person person)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand(@"
                INSERT INTO Person (Name, CompanyId) 
                VALUES (@name, @companyId); SELECT LAST_INSERT_ID();", conn);

            cmd.Parameters.AddWithValue("@name", person.Name);
            cmd.Parameters.AddWithValue("@companyId", person.Company?.Id ?? (object)DBNull.Value);
            
            person.Id = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void UpdatePerson(Person person)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand(@"
                UPDATE  Person 
                SET     Name = @name, 
                        CompanyId = @companyId 
                WHERE Id = @id", conn);

            cmd.Parameters.AddWithValue("@id", person.Id);
            cmd.Parameters.AddWithValue("@name", person.Name);
            cmd.Parameters.AddWithValue("@companyId", person.Company?.Id ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void DeletePerson(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand(@"
                DELETE 
                FROM    Person 
                WHERE   Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
} 