using Microsoft.EntityFrameworkCore;
using PersonsApi.Entities;

namespace PersonsApi.Data
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;
        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var personModelBuilder = modelBuilder.Entity<Person>();
            personModelBuilder.Property(x => x.Email).HasMaxLength(255);
            personModelBuilder.HasOne(p => p.Company)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.CompanyId);

            var companyModelBuilder = modelBuilder.Entity<Company>();
            companyModelBuilder.Property(z => z.Name).HasColumnName("Company").HasMaxLength(255);

            base.OnModelCreating(modelBuilder);
        }
    }
}
