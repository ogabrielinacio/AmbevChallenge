using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Context.Configurations;

public static class AuditableEntityConfiguration
{
    public static void ConfigureAuditableEntity<TBuilder>(this EntityTypeBuilder<TBuilder> builder,
        bool keyDefault = true) where TBuilder : BaseEntity 
    {
        if (keyDefault)
            builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

        builder.HasIndex(p => p.CreatedAt).IsUnique(false);
        builder.HasIndex(p => p.UpdatedAt).IsUnique(false);
    }
}