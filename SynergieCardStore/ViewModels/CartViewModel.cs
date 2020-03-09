using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergieCardStore.ViewModels
{
    public class CartViewModel
    {
        public List<CartPosition> CartPositions { get; set; }
        public decimal TotalPrice { get; set; }
    }
}