using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping
{
    public class StockMap : IEntityTypeConfiguration<StockModel>
    {
        public void Configure(EntityTypeBuilder<StockModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Barcode).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Amount).IsRequired().HasMaxLength(12);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(24);
            builder.HasIndex(u => u.Barcode)
           .IsUnique();
            builder.HasOne(u => u.Shop)
                .WithMany()
                .HasForeignKey(u => u.ShopId)
                .OnDelete(DeleteBehavior.NoAction) ;

            builder.HasOne(s => s.Product)
            .WithMany(p => p.Stock)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
