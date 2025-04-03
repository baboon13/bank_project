namespace bank_project.Services
{
    using bank_project.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserData> Users { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<LikeListData> LikeLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 設定 User 實體
            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasKey(u => u.UserID); // 主鍵設定
                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(u => u.Account)
                    .IsRequired()
                    .HasMaxLength(50);

                // 建立索引
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Account).IsUnique();
            });

            // 設定 Product 實體
            modelBuilder.Entity<ProductData>(entity =>
            {
                entity.HasKey(p => p.No); // 主鍵設定
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(p => p.FeeRate)
                    .HasColumnType("decimal(5,4)"); // 例如 0.1234 表示 12.34%
            });

            // 設定 LikeList 實體
            modelBuilder.Entity<LikeListData>(entity =>
            {
                entity.HasKey(l => l.SN); // 主鍵設定
                entity.Property(l => l.OrderName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(l => l.Account)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(l => l.TotalFee)
                    .HasColumnType("decimal(18,2)");
                entity.Property(l => l.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                // 設定與 User 的關係 ( Account 是外鍵)
                entity.HasOne<UserData>()
                    .WithMany()
                    .HasForeignKey(l => l.Account)
                    .HasPrincipalKey(u => u.Account)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}


