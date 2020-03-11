using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SynergieCardStore.App_Start;
using SynergieCardStore.EF;
using SynergieCardStore.Infrastructure;
using SynergieCardStore.Models;
using SynergieCardStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: Cart/Pay
        public async Task<ActionResult> Pay()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    FirstName = user.UserData.FirstName,
                    LastName = user.UserData.LastName,
                    Address = user.UserData.Address,
                    City = user.UserData.City,
                    PostalCode = user.UserData.PostalCode,
                    Email = user.UserData.Email,
                    Phone = user.UserData.Phone
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Pay", "Cart") });
            }
        }

        // POST: Cart/Pay
        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var newOrder = cartMenager.CreateOrder(orderDetails, userId);

                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                cartMenager.EmptyCart();

                var order = db.Orders.Include("OrderPositions").Include("OrderPositions.Product").SingleOrDefault(o => o.OrderId == newOrder.OrderId);
                OrderConfirmationEmail email = new OrderConfirmationEmail();
                email.To = order.Email;
                email.From = "synergiepolska@gmail.com";
                email.Value = order.OrderValue;
                email.OrderNumber = order.OrderId;
                email.OrderPositions = order.OrderPositions;
                email.Send();

                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderDetails);
        }

        public ActionResult OrderConfirmation()
        {
            var name = User.Identity.Name;
            return View();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}