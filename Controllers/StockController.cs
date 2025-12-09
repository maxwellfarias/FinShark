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
using FinShark.Repository;
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
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }


        /*IActionResult ermite retornar diferentes tipos de respostas HTTP:

            ✅ 200 OK - Sucesso
            ❌ 404 NotFound - Recurso não encontrado
            ⚠️ 400 BadRequest - Requisição inválida
            🔒 401 Unauthorized - Não autorizado 
            */
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks);
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            await _stockRepository.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = await _stockRepository.UpdateAsync(stockDto, id);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDto()); 
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteAsync(id);
            if(stock == null) return NotFound();
            return NoContent();
        }
    }
}