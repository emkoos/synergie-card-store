using SynergieCardStore.EF;
using SynergieCardStore.Models;
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
            var categoriesList = db.Categories.ToList();

            return View();
        }

        
    }
}