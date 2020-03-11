using SynergieCardStore.Models;
using SynergieCardStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SynergieCardStore.ViewModels.EmailModels;

namespace SynergieCardStore.Infrastructure
{
    public class PostalMailService : IMailService
    {
        public void SendOrderConfirmationEmail(Order order)
        {
            OrderConfirmationEmail email = new OrderConfirmationEmail();
            email.To = order.Email;
            email.From = "synergiepolska@gmail.com";
            email.Value = order.OrderValue;
            email.OrderNumber = order.OrderId;
            email.OrderPositions = order.OrderPositions;
            email.Send();
        }

        public void SendOrderCompletedEmail(Order order)
        {
            OrderCompletedEmail email = new OrderCompletedEmail();
            email.To = order.Email;
            email.From = "synergiepolska@gmail.com";
            email.OrderNumber = order.OrderId;
            email.Send();
        }
    }
}