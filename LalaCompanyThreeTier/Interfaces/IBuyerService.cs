using LalaCompanyThreeTier.Models;

namespace LalaCompanyThreeTier.Interfaces
{
    public interface IBuyerService
    {
        Task<List<Buyer>> GetAllBuyers();
        Task<Buyer?> GetBuyerById(int id);
        Task<Buyer?> CreateBuyer(Buyer Buyer);
        Task<Buyer?> UpdateBuyer(int id, Buyer Buyer);
        Task<Buyer> DeleteBuyer(int id);

    }
}
