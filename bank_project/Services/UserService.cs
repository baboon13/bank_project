namespace bank_project.Services
{
    using bank_project.Common;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography;
    using System.Text;


    public interface IUserService  // 用戶服務介面定義
    {
        Task<bool> RegisterAsync(User user);  // 註冊新用戶
        Task<User> AuthenticateAsync(string username, string password);  // 用戶認證（登入檢查）
        Task<bool> UserExistsAsync(string username);  // 檢查使用者名稱是否已存在
    }

 
    // 用戶服務實作
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            // 檢查用戶名是否已存在
            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                return false;
            }

            // 使用簡單哈希 (實際專案建議用 BCrypt)
            user.Password1 = SimpleHash(user.Password1);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 可選: 同時寫入SQL腳本備份
            LogUserToSqlScript(user);

            return true;
        }
        

        // 新增 AuthenticateAsync 方法實作
        public async Task<Users> AuthenticateAsync(string username, string password)
        {
            // 從資料庫查找用戶
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null; // 用戶不存在
            }

            // 驗證密碼 (使用相同的哈希方法)
            var hashedPassword = SimpleHash(password);
            if (user.Password1 != hashedPassword)
            {
                return null; // 密碼不匹配
            }

            return user; // 認證成功
        }

        // 新增 UserExistsAsync 方法實作
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        private string SimpleHash(string input)
        {
            // 簡易哈希範例 (僅用於開發測試)
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input + _configuration["Salt"]));
            return Convert.ToBase64String(bytes);
        }

        private void LogUserToSqlScript(Users user)
        {
            // 將新增用戶記錄到SQL腳本文件
            var sql = $@"
        -- 自動生成的用戶註冊記錄
        INSERT INTO Users (UserName, Email, Account, Password) 
        VALUES ('{user.UserName.Replace("'", "''")}', 
                '{user.Email.Replace("'", "''")}', 
                '{user.Account.Replace("'", "''")}', 
                '{user.Password1.Replace("'", "''")}');
        ";

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "DB", "02_DML_Users.sql");
            File.AppendAllText(filePath, sql + Environment.NewLine);
        }
    }

}
