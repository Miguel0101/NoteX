using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NoteX.Application.Common.Interfaces;
using NoteX.Domain.Notes.Interfaces;
using NoteX.Domain.Users.Interfaces;
using NoteX.Infrastructure.Data;
using NoteX.Infrastructure.Localization;
using NoteX.Infrastructure.Notes.Repositories;
using NoteX.Infrastructure.Security;
using NoteX.Infrastructure.Users.Repositories;

namespace NoteX.Infrastructure.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string? connectionString, IConfigurationSection jwtSettingsSection)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        // Persistency
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();

        // Localization
        services.AddScoped<ILocalizationResolver, LocalizationResolver>();

        // Security
        services.Configure<JwtSettings>(jwtSettingsSection);

        var jwtSettings = jwtSettingsSection.Get<JwtSettings>() 
                      ?? throw new InvalidOperationException("JwtSettings not found in configuration.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.PrivateKey)),

                    ValidateLifetime = true
                };
            });

        services.AddAuthorization();
        services.AddHttpContextAccessor();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}