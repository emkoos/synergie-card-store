using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SynergieCardStore.App_Start;
using SynergieCardStore.EF;
using SynergieCardStore.Infrastructure;
using SynergieCardStore.Models;
using SynergieCardStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SynergieCardStore.ViewModels.EmailModels;

namespace SynergieCardStore.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private SynergieEntities db = new SynergieEntities();
        private IMailService mailService;

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        public ManageController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        private ApplicationSignInManager _signInManager;
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

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var name = User.Identity.Name;

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")]UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult OrdersList()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Order> userOrders;

            if (isAdmin)
            {
                userOrders = db.Orders.Include("OrderPositions").OrderByDescending(o => o.AddedDate).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userOrders = db.Orders.Where(o => o.UserId == userId).Include("OrderPositions").OrderByDescending(o => o.AddedDate).ToArray();
            }

            return View(userOrders);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public OrderStatus OrderStatusChange(Order order)
        {
            Order orderForModification = db.Orders.Find(order.OrderId);
            orderForModification.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            if (orderForModification.OrderStatus == OrderStatus.Zrealizowane)
            {
                this.mailService.SendOrderCompletedEmail(orderForModification);
            }

            return order.OrderStatus;
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AddProduct(int? productId, bool? confirmation)
        {
            Product product;

            if (productId.HasValue)
            {
                ViewBag.EditMode = true;
                product = db.Products.Find(productId);
            }
            else
            {
                ViewBag.EditMode = false;
                product = new Product();
            }

            var result = new EditProductViewModel();
            result.Categories = db.Categories.ToList();
            result.Product = product;
            result.Confirmation = confirmation;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(EditProductViewModel model,IEnumerable<HttpPostedFileBase> file)
        {
            if (model.Product.ProductId > 0)
            {
                // modyfikacja produktu
                db.Entry(model.Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddProduct", new { confirmation = true });
            }
            else
            {
                // Sprawdzenie, czy użytkownik wybrał plik
                if (file != null)
                {
                    if (ModelState.IsValid)
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.Take(1).First().FileName);
                        var jpgfilename = Path.ChangeExtension(fileExt, ".jpg");

                        var filename = Guid.NewGuid() + jpgfilename;

                        var miniaturepath = Path.Combine(Server.MapPath(AppConfig.ProductsMiniaturesRelativeFolder), filename);
                        file.Take(1).First().SaveAs(miniaturepath);

                        model.Product.ImageFileName = filename;
                        System.IO.Directory.CreateDirectory(Server.MapPath(AppConfig.ProductsImagesRelativeFolder) + model.Product.ProductTitle);
                        int i = 1;
                        foreach (var item in file)
                        {
                            if (item  != null)
                            {
                                var itemExt = Path.GetExtension(item.FileName);
                                var jpgitemname = Path.ChangeExtension(itemExt, ".jpg");
                                var itemname = model.Product.ProductTitle + '_' + i + jpgitemname;
                                var imagespath = Path.Combine(Server.MapPath(AppConfig.ProductsImagesRelativeFolder), model.Product.ProductTitle, itemname);
                                item.SaveAs(imagespath);
                                i++;
                            }
                            else
                            {
                                i++;
                                continue;
                            }
                            
                        }

                        model.Product.ImagesName = model.Product.ProductTitle;
                        model.Product.AddedDate = DateTime.Now;

                        db.Entry(model.Product).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("AddProduct", new { confirmation = true });
                    }
                    else
                    {
                        var categories = db.Categories.ToList();
                        model.Categories = categories;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var categories = db.Categories.ToList();
                    model.Categories = categories;
                    return View(model);
                }
            }
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult HideProduct(int productId)
        {
            var product = db.Products.Find(productId);
            product.Hidden = true;
            db.SaveChanges();

            return RedirectToAction("AddProduct", new { confirmation = true });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowProduct(int productId)
        {
            var product = db.Products.Find(productId);
            product.Hidden = false;
            db.SaveChanges();

            return RedirectToAction("AddProduct", new { confirmation = true });
        }

        [AllowAnonymous]
        public ActionResult SendOrderConfirmationEmail(int orderId, string lastname)
        {
            var order = db.Orders.Include("OrderPositions").Include("OrderPositions.Product")
                                  .SingleOrDefault(o => o.OrderId == orderId && o.LastName == lastname);

            if (order == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            OrderConfirmationEmail email = new OrderConfirmationEmail();
            email.To = order.Email;
            email.From = "emil.saracyn@gmail.com";
            email.Value = order.OrderValue;
            email.OrderNumber = order.OrderId;
            email.OrderPositions = order.OrderPositions;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        public ActionResult SendOrderCompletedEmail(int orderId, string lastname)
        {
            var order = db.Orders.Include("OrderPositions").Include("OrderPositions.Product")
                                  .SingleOrDefault(o => o.OrderId == orderId && o.LastName == lastname);

            if (order == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            OrderCompletedEmail email = new OrderCompletedEmail();
            email.To = order.Email;
            email.From = "emil.saracyn@gmail.com";
            email.OrderNumber = order.OrderId;
            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}