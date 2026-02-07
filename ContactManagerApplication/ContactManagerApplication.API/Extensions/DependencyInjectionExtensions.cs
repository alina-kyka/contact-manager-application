using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Repositories;
using ContactManagerApplication.Application.Services;
using ContactManagerApplication.Infrastructure.Context;
using ContactManagerApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


namespace ContactManagerApplication.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContactsRepository, ContactsRepository>();
    }
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ICsvService<ContactModel>, CsvService>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationAssembly = typeof(ContactManagerApplicationDbContext).Assembly.GetName().Name;

        services.AddDbContext<ContactManagerApplicationDbContext>(options =>
            options.UseSqlServer(configuration["ConnectionStrings:ContactManagerApplicationDbContext"],
            opt => opt.MigrationsAssembly(migrationAssembly)));
    }

    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
