using Ambev.DeveloperEvaluation.ORM.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Context.Factory;

public class MongoContextFactory : IDesignTimeDbContextFactory<MongoContext>
{
    public MongoContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var mongoSettings = configuration
                                .GetSection("MongoSettings")
                                .Get<MongoSettings>()
                            ?? throw new InvalidOperationException("MongoSettings not configured properly.");

        var client = new MongoClient(mongoSettings.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Database);

        var optionsBuilder = new DbContextOptionsBuilder<MongoContext>();
        optionsBuilder.UseMongoDB(client, mongoSettings.Database);

        return new MongoContext(optionsBuilder.Options);
    }
}