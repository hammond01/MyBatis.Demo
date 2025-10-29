using Application.Services;
using Domain;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories.Repository;
using MyBatis.NET.Core;
using MyBatis.NET.Mapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Configure MyBatis
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=.;Initial Catalog=MybatisDemo;User ID=sa;Password=123456;TrustServerCertificate=True;";

// Load embedded mappers
MapperAutoLoader.AutoLoadFromAssemblies(Assembly.Load("Infrastructure"));

// Register services
builder.Services.AddScoped(sp => new SqlSession(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
