using bank_project.Models;

namespace bank_project.Repositories
{
    public interface ILikeListRepository
    {
        Task<IEnumerable<ProductData>> GetAllProductsAsync();
        Task AddToLikeListAsync(LikeListData likeList);
        Task<IEnumerable<LikeListData>> GetUserLikeListAsync(int userId);
        Task<LikeListData> GetLikeListItemAsync(int sn);
        Task UpdateLikeListItemAsync(LikeListData likeList);
        Task DeleteLikeListItemAsync(int sn);
        Task CalculateTotalsAsync(LikeListData likeList);
        Task<bool> ProductExistsAsync(int no);
    }
    
}
