using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping
{
    public class ShopMap : IEntityTypeConfiguration<ShopModel>
    {
        public void Configure(EntityTypeBuilder<ShopModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Status);
            builder.HasOne(x => x.User);
        }
    }
}
