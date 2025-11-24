using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using FinShark.Controllers.Dtos.Stock;
using FinShark.Dtos.Stock;


namespace FinShark.Repository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(UpdateStockRequestDto stock, int id);
        Task<Stock?> DeleteAsync(int id);
    }
}