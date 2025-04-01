namespace bank_project.Repositories
{
    using bank_project.Common;
    using bank_project.Models;
    using Dapper;
    using Microsoft.EntityFrameworkCore;
    using System.Data;

    public class LikeListRepository : ILikeListRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 新增這個方法來檢查產品是否存在
        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _context.Product
                .AnyAsync(p => p.No == productId);
        }

        public async Task CalculateTotalsAsync(LikeList likeList)
        {
            var product = await _context.Product
                .FirstOrDefaultAsync(p => p.No == likeList.No);

            if (product?.Price != null && product.FeeRate != null)
            {
                // 計算總金額和手續費（轉換為整數）
                likeList.TotalAmount = (int)(product.Price * likeList.OrderNUM);
                likeList.TotalFee = (int)(likeList.TotalAmount * (product.FeeRate / 100));
            }
        }

        public async Task AddToLikeListAsync(LikeList likeList)
        {
            //likeList.UpdatedDate = DateTime.Now;
            await _context.LikeList.AddAsync(likeList);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<IEnumerable<LikeList>> GetUserLikeListAsync(int userId)
        {
            return await _context.LikeList
                .Include(ll => ll.Product)
                .Where(ll => ll.UserID == userId)
                .ToListAsync();
        }

        public async Task<LikeList> GetLikeListItemAsync(int sn)
        {
            return await _context.LikeList
                .Include(ll => ll.Product)
                .FirstOrDefaultAsync(ll => ll.SN == sn);
        }

        public async Task UpdateLikeListItemAsync(LikeList likeList)
        {
            //likeList.UpdatedDate = DateTime.Now;
            _context.LikeList.Update(likeList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLikeListItemAsync(int sn)
        {
            var likeListItem = await _context.LikeList.FindAsync(sn);
            if (likeListItem != null)
            {
                _context.LikeList.Remove(likeListItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
