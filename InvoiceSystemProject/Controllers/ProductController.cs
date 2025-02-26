using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceSystemProject.Models;
using InvoiceSystemProject.Services;

namespace InvoiceSystemProject.Controllers
{
    public class ProductController : Controller
    {
        ProductService productService;
        public ProductController()
        {
            productService = new ProductService();
        }
        public ActionResult Index()
        {
            ViewBag.weights = new SelectList(productService.GetAlLWeights(), "weight_title", "weight_title");
            ViewData["products"] = productService.GetAllProducts();
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblproduct p)
        {

            productService.AddOrUpdateProduct(p);
            ViewBag.msg = "Product Added Successfully";
            ModelState.Clear();
            ViewData["products"] = productService.GetAllProducts();
            ViewBag.weights = new SelectList(productService.GetAlLWeights(), "weight_title", "weight_title");
            return View();
        }
    }
}