using Microsoft.EntityFrameworkCore;
using PersonsApi.Data;
using PersonsApi.Repositories;
using PersonsApi.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// Register in-memory (default) implementation
//builder.Services.AddScoped<IPersonRepository, PersonRepository>();
//builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Register ADO.NET implementation (uncomment to use ADO.NET)
builder.Services.AddScoped<IPersonRepository, PersonAdoNetRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyAdoNetRepository>();

// Register EF implementation
//builder.Services.AddScoped<IPersonRepository, PersonEFRepository>();
//builder.Services.AddScoped<ICompanyRepository, CompanyEFRepository>();

builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


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
