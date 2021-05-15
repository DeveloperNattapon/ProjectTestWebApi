using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectTest.Models
{
    public class ProductEntity
    {
        [Column("PersonsId")]
        public int? PersonsId { get; set; }
        [Column("PersonsName")]
        public string PersonsName { get; set; }
        [Column("imageUrl")]
        public string imageUrl { get; set; }
        [Column("price")]
        public decimal? price { get; set; }
    }
}
