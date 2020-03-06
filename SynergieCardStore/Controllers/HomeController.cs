using SynergieCardStore.EF;
using SynergieCardStore.Infrastructure;
using SynergieCardStore.Models;
using SynergieCardStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynergieCardStore.Controllers
{
    public class HomeController : Controller
    {
        private SynergieEntities db = new SynergieEntities();

        public ActionResult Index()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            IEnumerable<Product> products;

            if (cache.IsSet(Consts.mainProductsCacheKey))
            {
                products = cache.Get(Consts.mainProductsCacheKey) as IEnumerable<Product>;
            }
            else
            {
                products = db.Products.Where(p => !p.Old && !p.Preview && !p.Hidden).ToList();
                cache.Set(Consts.mainProductsCacheKey, products, 2);
            }
            
            var vm = new HomeViewModel()
            {
                Products = products
            };
            return View(vm);
        }

        public ActionResult StaticPages(string name)
        {
            return View(name);
        }

    }
}