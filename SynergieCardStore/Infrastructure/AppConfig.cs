using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SynergieCardStore.Infrastructure
{
    public class AppConfig
    {
        private static string _productsMiniaturesRelativeFolder = ConfigurationManager.AppSettings["ProductsMiniaturesFolder"];
        private static string _productsImagesRelativeFolder = ConfigurationManager.AppSettings["ProductsImagesFolder"];

        public static string ProductsMiniaturesRelativeFolder
        {
            get { return _productsMiniaturesRelativeFolder; }
        }


        public static string ProductsImagesRelativeFolder
        {
            get { return _productsImagesRelativeFolder; }
        }
    }
}