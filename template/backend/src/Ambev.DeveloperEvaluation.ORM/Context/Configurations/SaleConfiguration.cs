using Ambev.DeveloperEvaluation.Domain.Aggregates.SaleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Context.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.Property(s => s.SaleNumber)
            .IsRequired();

        builder.Property(s => s.CustomerId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(s => s.BranchId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(s => s.Cancelled)
            .IsRequired();

        builder.OwnsMany(s => s.Items, items =>
        {
            items.ToTable("SaleItems");

            items.WithOwner().HasForeignKey("SaleId");

            items.Property(i => i.ProductId)
                .HasColumnType("uuid")
                .IsRequired();

            items.Property(i => i.Quantity)
                .IsRequired();

            items.Property(i => i.UnitPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            items.Property(i => i.Discount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            items.HasKey("SaleId", "ProductId");
        });

        builder.ConfigureAuditableEntity();
    }
}