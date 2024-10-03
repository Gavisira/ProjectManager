using ProjectManager.Infrastructure.SQLServer.DependencyInjection;
using ProjectManager.MinimalApis;
using System.Reflection;
using ProjectManager.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services
    .AddSqlServerProjectManagerSetup(connectionString)
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProjectManager.Application.BaseResponse<>).Assembly));

var app = builder.Build();

app.SetupProjectsEndpoints();
app.SetupTasksEndpoints();
app.SetupUserEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

