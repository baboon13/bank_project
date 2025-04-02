namespace bank_project.Services
{
    using bank_project.Models;
    using global::bank_project.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Security.Cryptography;
    using System.Text;


    public interface IUserService  // 用戶服務介面定義
    {
        Task<bool> RegisterAsync(UserData user);  // 註冊新用戶
       // Task<UserData> GetUserByIdAsync(string userId);
        Task<bool> UserExistsAsync(string username);  // 檢查使用者名稱是否已存在
   
        Task<UserData> AuthenticateAsync(string username, string password);
        

    }

    // 用戶服務實作
        public class UserService : IUserService
        {
            private readonly ApplicationDbContext _context;

            public UserService(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<UserData> AuthenticateAsync(string username, string password)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

                
                return user;
            }

            public async Task<bool> RegisterAsync(UserData user)
            {
                if (await UserExistsAsync(user.UserName))
                    return false;

                //user.Password1 = HashPassword(user.Password1);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> UserExistsAsync(string username)
            {
                return await _context.Users.AnyAsync(u => u.UserName == username);
            }

            private string HashPassword(string password)
            {
                // 實現密碼哈希邏輯
                return password; // 實際應使用 BCrypt 等
            }

            private bool VerifyPassword(string inputPassword, string storedHash)
            {
                // 實現密碼驗證邏輯
                return inputPassword == storedHash; // 簡化示例，實際應安全比較
            }
        }
    

}
