using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergieCardStore.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool? Confirmation { get; set; }
    }
}