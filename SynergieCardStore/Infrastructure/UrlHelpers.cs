using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynergieCardStore.Infrastructure
{
    public static class UrlHelpers
    {
        public static string ProductsMiniaturesPath(this UrlHelper helper, string nameOfProductImage)
        {
            var ProductsMiniaturesFolder = AppConfig.ProductsMiniaturesRelativeFolder;
            var path = Path.Combine(ProductsMiniaturesFolder, nameOfProductImage);
            var absolutePath = helper.Content(path);

            return absolutePath;
        }

        public static string ProductsImagesPath(this UrlHelper helper, string nameOfFolderImages, string nameOfProductImages)
        {
            var ProductsImagesFolder = AppConfig.ProductsImagesRelativeFolder;
            var path = Path.Combine(ProductsImagesFolder, nameOfFolderImages, nameOfProductImages);

            var absolutePath = helper.Content(path);

            return absolutePath;
        }
    }
}