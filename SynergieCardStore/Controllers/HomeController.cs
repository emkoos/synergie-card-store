﻿using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            logger.Info("To jest strona główna");
            IEnumerable<Product> products;

            products = db.Products.Where(p => !p.Old && !p.Preview && !p.Hidden).ToList();

            
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