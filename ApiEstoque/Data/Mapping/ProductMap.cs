using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(45);
            builder.Property(x => x.ProductCode).IsRequired().HasMaxLength(12);
            builder.Property(x => x.Description).HasMaxLength(120);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.UnitOfMeasure).IsRequired().HasMaxLength(12);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(24);
            builder.HasIndex(u => u.ProductCode)
            .IsUnique();
        }
    }
}
