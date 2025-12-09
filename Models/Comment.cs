using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        //StockId eh chave estrangeira, para que a navegacao pudesse acontecer, foi necessario declarar o objeto nullable Stock
        //isso permitir devegar dentro desse relacionamento
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Stock? Stock { get; set; }
        public int? StockId { get; set; }
    }
}