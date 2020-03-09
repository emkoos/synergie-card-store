using SynergieCardStore.EF;
using SynergieCardStore.Infrastructure;
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
        public ActionResult Details(int? id)
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
            ICacheProvider cache = new DefaultCacheProvider();
            IEnumerable<Product> archivedProducts;

            if (cache.IsSet(Consts.ArchivesCacheKey))
            {
                archivedProducts = cache.Get(Consts.ArchivesCacheKey) as IEnumerable<Product>;
            }
            else
            {
                archivedProducts = from aP in db.Products where aP.Old && !aP.Hidden select aP;
                cache.Set(Consts.ArchivesCacheKey, archivedProducts, 2);
            }


            return View(archivedProducts);
        }

        // GET: Product/Previews
        public ActionResult Previews()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            IEnumerable<Product> futureProducts;
            
            if (cache.IsSet(Consts.PreviewsCacheKey))
            {
                futureProducts = cache.Get(Consts.PreviewsCacheKey) as IEnumerable<Product>;
            }
            else
            {
                futureProducts = from fP in db.Products where fP.Preview && !fP.Hidden select fP;
                cache.Set(Consts.PreviewsCacheKey, futureProducts, 2);
            }

            return View(futureProducts);
        }
        // GET: Product/News
        public ActionResult News()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            IEnumerable<Product> newProducts;

            if (cache.IsSet(Consts.NewsCacheKey))
            {
                newProducts = cache.Get(Consts.NewsCacheKey) as IEnumerable<Product>;
            }
            else
            {
                newProducts = db.Products.Where(n => !n.Hidden && !n.Old && !n.Preview).OrderByDescending(n => n.AddedDate).Take(6);
                cache.Set(Consts.NewsCacheKey, newProducts, 2);
            }

            return View(newProducts);
        }
    }
}