using bank_project.Models;

namespace bank_project.Services
{
    
    public interface ILikeListService
    {
        Task<IEnumerable<ProductData>> GetAvailableProductsAsync();
        Task AddToLikeListAsync(int userId, LikeListData likeList);
        Task<IEnumerable<LikeListData>> GetUserLikeListAsync(int userId);
        Task<LikeListData> GetLikeListItemAsync(int userId, int sn);
        Task UpdateLikeListItemAsync(int userId, LikeListData likeList);
        Task RemoveFromLikeListAsync(int userId, int sn); // 這個方法未被實作
    }
}
