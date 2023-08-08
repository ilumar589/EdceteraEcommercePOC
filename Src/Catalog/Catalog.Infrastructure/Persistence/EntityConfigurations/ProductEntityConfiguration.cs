using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(p => p.Id);
        builder.Property<int>("_productTypeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ProductTypeId")
            .IsRequired();

        builder.HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey("_productTypeId");
    }
}