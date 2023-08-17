using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status);

        }
    }
}
