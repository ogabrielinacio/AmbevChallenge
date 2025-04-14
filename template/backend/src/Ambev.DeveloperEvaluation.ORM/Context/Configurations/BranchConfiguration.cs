using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Context.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(80);

        builder.OwnsOne(b => b.Address, address =>
        {
            address.Property(a => a.Street).IsRequired().HasMaxLength(100);
            address.Property(a => a.City).IsRequired().HasMaxLength(50);
            address.Property(a => a.State).IsRequired().HasMaxLength(2);
            address.Property(a => a.ZipCode).IsRequired().HasMaxLength(10);
            address.Property(a => a.Country).IsRequired().HasMaxLength(50);
        });

        builder.Property(b => b.OwnerId)
            .HasColumnType("uuid").IsRequired();

        builder.HasMany(b => b.Products)
            .WithOne()
            .HasForeignKey(p => p.BranchId);

        builder.OwnsMany(b => b.Users, users =>
        {
            users.ToTable("BranchUsers");

            users.WithOwner().HasForeignKey("BranchId");

            users.Property(u => u.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uuid").IsRequired();

            users.Property(u => u.Role)
                .HasConversion(
                    role => role.Value.ToString(),          
                    value => BranchUserRole.Create(Enum.Parse<UserRole>(value))
                )
                .HasMaxLength(20)
                .IsRequired();


            users.HasKey("BranchId", "UserId");
        });

        builder.ConfigureAuditableEntity();
    }
}