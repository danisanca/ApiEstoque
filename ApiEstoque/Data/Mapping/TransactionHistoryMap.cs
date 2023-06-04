using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping
{
    public class TransactionHistoryMap : IEntityTypeConfiguration<TransactionHistoryModel>
    {
        public void Configure(EntityTypeBuilder<TransactionHistoryModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Reason).IsRequired().HasMaxLength(45);
            builder.HasOne(u => u.Shop)
                .WithMany()
                .HasForeignKey(u => u.ShopId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Product)
                .WithMany()
                .HasForeignKey(u => u.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
