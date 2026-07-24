using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Interfaces;
using LalaCompanyThreeTier.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly AppDbContext _appDbContext;

        public BuyerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Buyer>> GetAllBuyers()
        {
            return await _appDbContext.Buyers.ToListAsync();
        }
        public async Task<Buyer?> GetBuyerById(int id)
        {
            return await _appDbContext.Buyers.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Buyer?> CreateBuyer(Buyer buyer)
        {
            _appDbContext.Buyers.Add(buyer);
            await _appDbContext.SaveChangesAsync();
            return buyer;
        }
        public async Task<Buyer?> UpdateBuyer(int id, Buyer buyer)
        {
            var existingBuyer = await _appDbContext.Buyers.FindAsync(id);
            if (existingBuyer == null)
            {
                return null;
            }
            _appDbContext.Entry(existingBuyer).CurrentValues.SetValues(buyer);
            await _appDbContext.SaveChangesAsync();
            return existingBuyer;
        }
        public async Task<Buyer> DeleteBuyer(int id)
        {
            var buyer = await _appDbContext.Buyers.FindAsync(id);
            if (buyer != null)
            {
                _appDbContext.Buyers.Remove(buyer);
                await _appDbContext.SaveChangesAsync();
                return buyer;
            }
            return null;
        }
    }
}
