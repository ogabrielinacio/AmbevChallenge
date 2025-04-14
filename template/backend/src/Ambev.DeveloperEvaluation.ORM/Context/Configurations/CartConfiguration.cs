using Ambev.DeveloperEvaluation.Domain.Aggregates.CartAggragate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Ambev.DeveloperEvaluation.ORM.Context.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToCollection("Carts"); 

        builder.Property(c => c.CustomerId).IsRequired();

        builder.OwnsMany(c => c.Items, items =>
        {
            items.Property(i => i.ProductId).IsRequired();
            items.Property(i => i.Quantity).IsRequired();
        });

    }
}