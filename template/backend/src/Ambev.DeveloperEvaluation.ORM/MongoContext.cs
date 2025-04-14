using System.Reflection;
using Ambev.DeveloperEvaluation.Domain.Aggregates.CartAggragate;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Context;

public class MongoContext: DbContext
{

    public MongoContext(DbContextOptions<MongoContext> options) : base(options)
    {
    }
    
    public DbSet<Cart> Carts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}