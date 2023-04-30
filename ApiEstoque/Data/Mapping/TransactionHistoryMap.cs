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
            builder.HasOne(x => x.User);
            builder.HasOne(x => x.Product);
        }
    }
}
