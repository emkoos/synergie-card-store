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
        private CardsEntities db = new CardsEntities();

        public ActionResult Index()
        {
            Category category = new Category { CategoryName = "Książki i karty", CategoryDescription = "W kategorii znajdują się wszystkie książki i karty dotyczące tarota" };
            db.Categories.Add(category);
            db.SaveChanges();

            return View();
        }

        
    }
}