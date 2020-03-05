using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SynergieCardStore.Models
{
    public class CartPosition
    {
        [Key]
        public int CartPostionId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }

    }
}