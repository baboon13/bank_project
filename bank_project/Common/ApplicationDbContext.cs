using bank_project.Models;

namespace bank_project.Common
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        // 修正 DbSet 屬性名稱為複數 (對應資料庫表名)
        public DbSet<User> Users { get; set; }  // 單數實體類，複數 DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<LikeList> LikeLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置 LikeList 與 User 的關係
            modelBuilder.Entity<LikeList>()
                .HasOne(ll => ll.Users)  // 單數導航屬性
                .WithMany(u => u.LikeLists)  // 複數集合
                .HasForeignKey(ll => ll.UserID);

            // 配置 LikeList 與 Product 的關係
            modelBuilder.Entity<LikeList>()
                .HasOne(ll => ll.Product)
                .WithMany(p => p.LikeList)
                .HasForeignKey(ll => ll.No)
                .OnDelete(DeleteBehavior.Restrict);

            // 設定 decimal 類型的精度
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.FeeRate)
                .HasColumnType("decimal(5,2)");
            // 明確指定表名為複數 (可選，但建議保留以保持清晰)
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<LikeList>().ToTable("LikeLists");


        }

    }
}
  
     
