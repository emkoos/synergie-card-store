using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergieCardStore.ViewModels
{
    public class DeleteFromCartViewModel
    {
        public decimal CartTotalPrice { get; set; }
        public int CartItemsNumber { get; set; }
        public int RemovedItemNumber { get; set; }
        public int RemovedItemId { get; set; }
        public decimal CartTotalProductAmount { get; set; }
    }
}