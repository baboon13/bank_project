using bank_project.Models;

namespace bank_project.Common
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 使用單數形式與資料庫表名對應
        public DbSet<Users> Users { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<LikeList> LikeList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置 LikeList 與 Users 的關係
            modelBuilder.Entity<LikeList>()
                .HasOne(ll => ll.Users)
                .WithMany(u => u.LikeList)
                .HasForeignKey(ll => ll.UserID)
                .OnDelete(DeleteBehavior.Restrict); // 防止級聯刪除

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

            // 設定表名為單數形式（可選）
            modelBuilder.Entity<Users>().ToTable("User");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<LikeList>().ToTable("LikeList");

            // 設定 LikeList 的預設值
            //modelBuilder.Entity<LikeList>()
                //.Property(ll => ll.CreatedDate)
                //.HasDefaultValueSql("GETDATE()");
        }
    }
}
