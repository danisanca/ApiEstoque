using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping
{
    public class UserMap:IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Status).IsRequired();
            builder.HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}
