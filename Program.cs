using PersonsApi.Repositories;
using PersonsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// Register in-memory (default) implementation
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Register ADO.NET implementation (uncomment to use ADO.NET)
//builder.Services.AddScoped<IPersonRepository, PersonAdoNetRepository>();
//builder.Services.AddScoped<ICompanyRepository, CompanyAdoNetRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
