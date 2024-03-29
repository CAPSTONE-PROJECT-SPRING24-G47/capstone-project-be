using capstone_project_be.Application;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Infrastructure;
using capstone_project_be.Infrastructure.Context;
using capstone_project_be.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("Policy", build => build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddDbContext<ProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices();

builder.Services.AddTransient<IStorageRepository, StorageRepository>();

var app = builder.Build();
app.UseCors("Policy");

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
