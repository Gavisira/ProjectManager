using ProjectManager.Application;
using ProjectManager.Infrastructure.SQLServer.DependencyInjection;
using ProjectManager.MinimalApis;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services
    .AddSqlServerProjectManagerSetup(connectionString)
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BaseResponse<>).Assembly));

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

[ExcludeFromCodeCoverage]
public partial class Program { }