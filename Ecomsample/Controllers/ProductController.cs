using Ecomsample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomsample.Controllers
{
    public class ProductController : Controller
    {

        private readonly EcommerceDbcontext _db;
        private readonly IWebHostEnvironment _webHosEnvironment;


        public ProductController(EcommerceDbcontext db , IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHosEnvironment = webHostEnvironment;
        }

        public ActionResult Index()
        {
            var all = _db.products.Include("Category").ToList();



            return View(all);
        }
        [Authorize]
        public ActionResult CreateProduct()
        {

            ViewBag.a = new SelectList(_db.categories, "Id", "CName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (product.p_image != null)
            {
                var folder = "images/product/";
                folder += Guid.NewGuid().ToString() +'_' + product.p_image.FileName ;
                product.productimageurl = "/"+folder;
                var serverfolder = Path.Combine(_webHosEnvironment.WebRootPath, folder);
                product.p_image.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }

            _db.products.Add(new Product()
            {
                PName = product.PName,
                price = product.price,
                productimageurl = product.productimageurl,
                CategoryId = product.CategoryId
            }) ;

            _db.SaveChanges();
            return View();
        }
       
        public ActionResult showproducts()
        {
            var a = _db.products.Include("Category").ToList();
            return View(a);
        }
        public ActionResult productdetails(int id)
        {
            var a = _db.products.Where(a => a.Id == id).Include("Category").FirstOrDefault();
            return View(a);
        }
    }
}
