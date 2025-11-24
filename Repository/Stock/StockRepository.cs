using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using FinShark.Controllers.Dtos.Stock;
using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock  = await _context.Stocks.FindAsync(id);
            if(stock == null) return null;
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            var stocks = await _context.Stocks.ToListAsync();
            return stocks;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            return stock;
        }

        public async Task<Stock?> UpdateAsync(UpdateStockRequestDto stock, int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if(stockModel == null) return null;
            stockModel.Industry = stock.Industry;
            stockModel.CompanyName = stock.CompanyName;
            stockModel.LastDiv = stock.LastDiv;
            stockModel.MarketCap = stock.MarketCap;
            stockModel.Purchase = stock.Purchase;
            stockModel.Symbol = stock.Symbol;
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}