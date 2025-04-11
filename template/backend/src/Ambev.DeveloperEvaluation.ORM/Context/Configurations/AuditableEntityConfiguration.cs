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
        builder.Property(e => e.Created).HasColumnName("CreatedDate").IsRequired();
        builder.Property(e => e.Updated).HasColumnName("UpdatedDate");

        builder.HasIndex(p => p.Created).IsUnique(false);
        builder.HasIndex(p => p.Updated).IsUnique(false);
    }
}