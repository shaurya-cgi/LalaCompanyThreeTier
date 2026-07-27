using LalaCompanyThreeTier.Dtos.Buyer;
using LalaCompanyThreeTier.Models;

namespace LalaCompanyThreeTier.Interfaces
{
    public interface IBuyerService
    {
        Task<List<Buyer>> GetAllBuyers();
        Task<Buyer?> GetBuyerById(int id);
        Task<Buyer> CreateBuyer(CreateBuyerDto Buyer);
        Task<Buyer?> UpdateBuyer(int id, UpdateBuyerDto dto);
        Task<Buyer?> DeleteBuyer(int id);

    }
}
