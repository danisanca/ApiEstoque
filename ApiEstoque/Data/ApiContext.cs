using ApiEstoque.Data.Mapping;
using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext>options):base(options) {}
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
