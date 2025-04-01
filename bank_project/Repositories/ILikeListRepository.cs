using bank_project.Models;

namespace bank_project.Repositories
{
    public interface ILikeListRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddToLikeListAsync(LikeList likeList);
        Task<IEnumerable<LikeList>> GetUserLikeListAsync(int userId);
        Task<LikeList> GetLikeListItemAsync(int sn);
        Task UpdateLikeListItemAsync(LikeList likeList);
        Task DeleteLikeListItemAsync(int sn);
        Task CalculateTotalsAsync(LikeList likeList);
        Task<bool> ProductExistsAsync(int no);
    }
    
}
