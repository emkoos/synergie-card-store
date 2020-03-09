using SynergieCardStore.EF;
using SynergieCardStore.Infrastructure;
using SynergieCardStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynergieCardStore.Controllers
{
    public class CartController : Controller
    {
        private CartMenager cartMenager;
        private ISessionMenager sessionMenager { get; set; }
        private SynergieEntities db;

        
        public CartController()
        {
            db = new SynergieEntities();
            sessionMenager = new SessionMenager();
            cartMenager = new CartMenager(sessionMenager, db);
        } 

        // GET: Cart
        public ActionResult Index()
        {
            var cartPositions = cartMenager.TakeCart();
            var totalPrice = cartMenager.TakeCartValue();
            CartViewModel cartVM = new CartViewModel()
            {
                CartPositions = cartPositions,
                TotalPrice = totalPrice
            };

            return View(cartVM);
        }

        public ActionResult AddToCart(int id)
        {
            cartMenager.AddToCart(id);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFromCart(int prodId)
        {
            int positionsNumber = cartMenager.DeleteFromCart(prodId);
            int cartPositionsNumber = cartMenager.TakeQuantityCartPositions();
            decimal cartValue = cartMenager.TakeCartValue();
            
            decimal productPrice = db.Products.Find(prodId).ProductPrice;

            var result = new DeleteFromCartViewModel
            {
                RemovedItemId = prodId,
                RemovedItemNumber = positionsNumber,
                CartTotalPrice = cartValue,
                CartItemsNumber = cartPositionsNumber,
                CartTotalProductAmount = productPrice * positionsNumber
            };

            return Json(result);
        }
        public ActionResult DeleteCart()
        {
            cartMenager.DeleteCart();
            return RedirectToAction("Index");
        }

        public int CartItemsNumber()
        {
            return cartMenager.TakeQuantityCartPositions();
        }

        public decimal CartItemsTotalPrice()
        {
            return cartMenager.TakeCartValue();
        }


    }
}