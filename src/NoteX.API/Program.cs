using NoteX.API.Configurations;
using NoteX.API.Extensions.Middleware;
using NoteX.Application.Extensions.DependencyInjection;
using NoteX.Infrastructure.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration Sections
string? connectionString = builder.Configuration.GetConnectionString("DbCredentials");
IConfigurationSection localizationSection = builder.Configuration.GetSection("Localization");
IConfigurationSection jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");

// Hosting
builder.WebHost.UseQuic();

// Dependency Injection
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddControllers();

// DDD Layers
builder.Services.AddInfrastructureLayer(connectionString, jwtSettingsSection);
builder.Services.AddApplicationLayer();

// Configurations
builder.Services.Configure<Localization>(localizationSection);

var app = builder.Build();

// Middlewares
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseTranslation();

// Endpoint Mapping
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

app.Run();
