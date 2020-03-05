using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergieCardStore.Models
{
    public partial class Product
    {
        public override string ToString()
        {
            return $"Product: {this.ProductTitle ?? "**Bez nazwy**"}, Autor: {this.ProductAuthor ?? "**Bez autora**"}, Cena: {this.ProductPrice}.";
        }
    }
}