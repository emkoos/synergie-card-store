using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergieCardStore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; } 

    }
}