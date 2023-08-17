using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping
{
    public class OfficeMap : IEntityTypeConfiguration<OfficeModel>
    {
        public void Configure(EntityTypeBuilder<OfficeModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(45);
            builder.HasIndex(u => u.Name)
            .IsUnique();
        }
    }
}
