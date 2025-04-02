using bank_project.Models;
using bank_project.Repositories;

namespace bank_project.Services
{
    /*public class LikeListService : ILikeListService
    {
        private readonly ILikeListRepository _repository;

        public LikeListService(ILikeListRepository repository)
        {
            _repository = repository;
        }

        // 實作缺少的 RemoveFromLikeListAsync 方法
        public async Task RemoveFromLikeListAsync(int userId, int sn)
        {
            // 先檢查該項目是否屬於該用戶
            var likeListItem = await _repository.GetLikeListItemAsync(sn);

            if (likeListItem == null || likeListItem.UserID != userId)
            {
                throw new UnauthorizedAccessException("您無權刪除此項目");
            }

            await _repository.DeleteLikeListItemAsync(sn);
        }

        // 其他已存在的方法...
        public async Task AddToLikeListAsync(int userId, LikeList likeList)
        {
            if (!await _repository.ProductExistsAsync(likeList.No))
            {
                throw new ArgumentException("指定的商品不存在");
            }

            likeList.UserID = userId;
            likeList.OrderName = $"LIK-{DateTime.Now:yyyyMMddHHmmss}";

            await _repository.CalculateTotalsAsync(likeList);
            await _repository.AddToLikeListAsync(likeList);
        }

        public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<LikeList>> GetUserLikeListAsync(int userId)
        {
            return await _repository.GetUserLikeListAsync(userId);
        }

        public async Task<LikeList> GetLikeListItemAsync(int userId, int sn)
        {
            var item = await _repository.GetLikeListItemAsync(sn);
            if (item?.UserID != userId)
            {
                return null;
            }
            return item;
        }

        public async Task UpdateLikeListItemAsync(int userId, LikeList likeList)
        {
            if (likeList.UserID != userId)
            {
                throw new UnauthorizedAccessException("您無權修改此項目");
            }

            await _repository.CalculateTotalsAsync(likeList);
            await _repository.UpdateLikeListItemAsync(likeList);
        }
    }*/
}
