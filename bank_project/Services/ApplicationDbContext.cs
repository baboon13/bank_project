using bank_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace bank_project.Services
{  
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext<UserData>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 不需要再定義 DbSet<UserData>，因為 IdentityDbContext 已經提供
        //public DbSet<UserData> Users { get; set; }
        public DbSet<ProductData> Products { get; set; }
        public DbSet<LikeListData> LikeLists { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 必須先調用基類方法
            base.OnModelCreating(modelBuilder);

            // 可選：重命名 Identity 相關表名
            modelBuilder.Entity<UserData>().ToTable("UserData");          // 用戶表
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");      // 角色表
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");          // 用戶角色關聯表
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");        // 用戶聲明表
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");        // 用戶登入表
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");        // 用戶令牌表
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");        // 角色聲明表
            // 其他 Identity 表名配置...

            // 移除對 UserData 的主鍵配置，因為 IdentityUser 已經處理

            // 只配置額外屬性
            modelBuilder.Entity<UserData>(entity =>
            {
                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
                
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
                entity.HasKey(p => p.No);
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(p => p.FeeRate)
                    .HasColumnType("decimal(5,4)");
            });

            // 設定 LikeList 實體
            modelBuilder.Entity<LikeListData>(entity =>
            {
                entity.HasKey(l => l.SN);
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

                // 設定與 User 的關係 (使用 Account 作為外鍵)
                entity.HasOne<UserData>()
                    .WithMany()
                    .HasForeignKey(l => l.Account)
                    .HasPrincipalKey(u => u.Account)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            
        }
    }
}


