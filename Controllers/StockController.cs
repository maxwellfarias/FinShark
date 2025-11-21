using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using FinShark.Data;
using FinShark.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
            //retorna uma resposta HTTP 200 OK com o objeto stocks serializado como JSON.
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIId([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}