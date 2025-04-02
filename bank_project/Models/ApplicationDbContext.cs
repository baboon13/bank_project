namespace bank_project.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // 修正 DbSet 屬性名稱為複數 (對應資料庫表名)
        public DbSet<UserData> Users { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<LikeListData> LikeLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 处理带空格的列名
            modelBuilder.Entity<ProductData>()
                .Property(p => p.ProductName)
                .HasColumnName("Product Name");

            modelBuilder.Entity<ProductData>()
                .Property(p => p.FeeRate)
                .HasColumnName("Fee rate");
        }

    }
}


