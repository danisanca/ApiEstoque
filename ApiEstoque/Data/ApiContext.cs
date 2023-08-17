using ApiEstoque.Data.Mapping;
using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ApiEstoque.Data
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext>options):base(options) {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OfficeModel> Offices { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<StockModel> Stock { get; set; }
        public DbSet<ShopModel> Shop { get; set; }
        public DbSet<EmployeeModel> Employee { get; set; }
        public DbSet<TransactionHistoryModel> TransactionsHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new OfficeMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new ShopMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new TransactionHistoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
        