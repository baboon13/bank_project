using bank_project.Models;

namespace bank_project.Services
{
    
    public interface ILikeListService
    {
        Task<IEnumerable<Product>> GetAvailableProductsAsync();
        Task AddToLikeListAsync(int userId, LikeList likeList);
        Task<IEnumerable<LikeList>> GetUserLikeListAsync(int userId);
        Task<LikeList> GetLikeListItemAsync(int userId, int sn);
        Task UpdateLikeListItemAsync(int userId, LikeList likeList);
        Task RemoveFromLikeListAsync(int userId, int sn); // 這個方法未被實作
    }
}
