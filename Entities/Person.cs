namespace PersonsApi.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}