using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using IncidentManagement.Application.Commands;
using IncidentManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Load Key Vault URL from environment or config
var vaultUrl =
    Environment.GetEnvironmentVariable("KeyVaultUrl")
    ?? Environment.GetEnvironmentVariable("Values__KeyVaultUrl")
    ?? builder.Configuration["KeyVaultUrl"];

if (string.IsNullOrWhiteSpace(vaultUrl))
{
    throw new InvalidOperationException("KeyVaultUrl is not configured.");
}

// Azure Key Vault
builder.Configuration.AddAzureKeyVault(
    new Uri(vaultUrl),
    new DefaultAzureCredential());

// SQL Server (EF Core)
builder.Services.AddDbContext<IncidentDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlConnection")));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateIncidentCommand).Assembly));

builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();