using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectTest.Models
{
    public class StockEntity
    {
        [Column("StockId")]
        public int? StockId { get; set; }
        [Column("productId")]
        public int? productId { get; set; }
       
        [Column("amount")]
        public int? amount { get; set; }
    }
}
