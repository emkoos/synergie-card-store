using SynergieCardStore.EF;
using SynergieCardStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SynergieCardStore.Controllers
{
    public class ProductController : Controller
    {
        private SynergieEntities db = new SynergieEntities();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            var name = User.Identity.Name;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Archives
        public ActionResult Archives()
        {
            IEnumerable<Product> archivedProducts;
            archivedProducts = from aP in db.Products where aP.Old && !aP.Hidden select aP;


            return View(archivedProducts);
        }

        // GET: Product/Previews
        public ActionResult Previews()
        {
            IEnumerable<Product> futureProducts;
            
            futureProducts = from fP in db.Products where fP.Preview && !fP.Hidden select fP;

            return View(futureProducts);
        }
        // GET: Product/News
        public ActionResult News()
        {
            IEnumerable<Product> newProducts;

            newProducts = db.Products.Where(n => !n.Hidden && !n.Old && !n.Preview).OrderByDescending(n => n.AddedDate).Take(6);

            return View(newProducts);
        }
    }
}