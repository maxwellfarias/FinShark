using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Models;
using FinShark.Controllers.Dtos.Stock;
using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinShark.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        //ctro eh a abreviação para construir um construtor
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }


        /*IActionResult ermite retornar diferentes tipos de respostas HTTP:

            ✅ 200 OK - Sucesso
            ❌ 404 NotFound - Recurso não encontrado
            ⚠️ 400 BadRequest - Requisição inválida
            🔒 401 Unauthorized - Não autorizado 
            */
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var total = await _context.Stocks.CountAsync();
            var stoks = await _context.Stocks
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
            //retorna uma resposta HTTP 200 OK com o objeto stocks serializado como JSON.
            return Ok(new {total, data = stoks.Select(s => s.ToStockDto())});
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            await _context.Stocks.AddAsync(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            _context.SaveChanges();
            return Ok(stock.ToStockDto()); 
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> delete([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound();
            } 
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();
        }
    }
}