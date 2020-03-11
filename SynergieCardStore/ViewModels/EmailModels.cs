using Postal;
using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergieCardStore.ViewModels
{
    public class EmailModels
    {
        public class OrderConfirmationEmail : Email
        {
            public string To { get; set; }
            public string From { get; set; }
            public decimal Value { get; set; }
            public int OrderNumber { get; set; }
            public List<OrderPosition> OrderPositions { get; set; }
        }

        public class OrderCompletedEmail : Email
        {
            public string To { get; set; }
            public string From { get; set; }
            public int OrderNumber { get; set; }
        }
    }
}