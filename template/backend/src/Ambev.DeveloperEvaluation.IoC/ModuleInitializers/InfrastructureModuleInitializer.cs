using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Context;
using Ambev.DeveloperEvaluation.ORM.Context.Interceptors;
using Ambev.DeveloperEvaluation.ORM.Context.Models;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();

        builder.Services.AddDbContext<IUnitOfWork, DefaultContext>((svcProvider, options) =>
        {
            var configuration = builder.Configuration;

            options
                .AddInterceptors(svcProvider.GetServices<AuditableEntityInterceptor>())
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                );
        });
        
        
        builder.Services.Configure<MongoSettings>(
            builder.Configuration.GetSection("MongoSettings"));

        
        var mongoSettings = builder.Configuration
            .GetSection("MongoSettings")
            .Get<MongoSettings>();
        builder.Services.AddDbContext<MongoContext>((svcProvider, options) =>
            {
                options
                    .AddInterceptors(svcProvider.GetServices<AuditableEntityInterceptor>());
                options.UseMongoDB(mongoSettings.ConnectionString ?? "", mongoSettings.Database ?? "");
            }
        ); 
        
        builder.Services.AddScoped<DefaultContext>();
        builder.Services.AddScoped<MongoContext>();
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<MongoContext>());
            
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBranchRepository, BranchRepository>();
        builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
        
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
    }
}