using SynergieCardStore.EF;
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
            var products = db.Products.ToList();

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