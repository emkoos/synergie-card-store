using SynergieCardStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SynergieCardStore.Models;

namespace SynergieCardStore.Infrastructure
{
    public class CartMenager
    {
        private SynergieEntities db;
        private ISessionMenager session;
        public CartMenager(ISessionMenager session, SynergieEntities db)
        {
            this.session = session;
            this.db = db;
        }

        public List<CartPosition> TakeCart()
        {
            List<CartPosition> cart;

            if (session.Get<List<CartPosition>>(Consts.CartSessionKey) == null)
                cart = new List<CartPosition>();
            else
                cart = session.Get<List<CartPosition>>(Consts.CartSessionKey) as List<CartPosition>;
            
            return cart;
        }

        public void AddToCart(int productId)
        {
            var cart = TakeCart();
            var cartPosition = cart.Find(p => p.Product.ProductId == productId);

            if (cartPosition != null)
                cartPosition.Quantity++;
            else
            {
                var productToAdd = db.Products.Where(p => p.ProductId == productId).SingleOrDefault();

                if(productToAdd != null)
                {
                    var newCartPosition = new CartPosition()
                    {
                        Product = productToAdd,
                        Quantity = 1,
                        Value = productToAdd.ProductPrice
                    };
                    cart.Add(newCartPosition);
                }
            }
            session.Set(Consts.CartSessionKey, cart);
        }

        public int DeleteFromCart(int productId)
        {
            var cart = TakeCart();
            var cartPosition = cart.Find(p => p.Product.ProductId == productId);

            if (cartPosition != null)
            {
                if(cartPosition.Quantity > 1)
                {
                    cartPosition.Quantity--;
                    return cartPosition.Quantity;
                }
                else
                {
                    cart.Remove(cartPosition);
                }               
            }
            return 0;
        }

        public int DeleteCart()
        {
            var cart = TakeCart();
            cart.RemoveAll(c => c.Quantity > 0);

            return 0;
        }

        public decimal TakeCartValue()
        {
            var cart = TakeCart();
            return cart.Sum(c => (c.Quantity * c.Product.ProductPrice));
        }

        public int TakeQuantityCartPositions()
        {
            var cart = TakeCart();
            int number = cart.Sum(c => c.Quantity);
            return number;
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = TakeCart();
            newOrder.AddedDate = DateTime.Now;
            //newOrder.UserId = userId;

            db.Orders.Add(newOrder);

            if (newOrder.OrderPositions == null)
            {
                newOrder.OrderPositions = new List<OrderPosition>();
            }

            decimal cartValue = 0;

            foreach (var cartItem in cart)
            {
                var newOrderPosition = new OrderPosition()
                {
                    ProductId = cartItem.Product.ProductId,
                    Quantity = cartItem.Quantity,
                    PurchasePrice = cartItem.Product.ProductPrice
                };

                cartValue += (cartItem.Quantity * cartItem.Product.ProductPrice);
                newOrder.OrderPositions.Add(newOrderPosition);
            }
            newOrder.OrderValue = cartValue;
            db.SaveChanges();

            return newOrder;
        }

        public void EmptyCart()
        {
            session.Set<List<CartPosition>>(Consts.CartSessionKey, null);
        }
    }
}