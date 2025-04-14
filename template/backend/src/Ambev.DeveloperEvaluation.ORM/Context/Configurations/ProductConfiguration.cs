using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.BranchId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Image)
            .IsRequired()
            .HasMaxLength(255);

        builder.OwnsOne(p => p.Rating, rating =>
        {
            rating.Property(r => r.Rate)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            rating.Property(r => r.Count)
                .IsRequired();
        });

        builder.ConfigureAuditableEntity(); 
    }
}