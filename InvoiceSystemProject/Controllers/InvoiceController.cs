using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceSystemProject.Models;
using InvoiceSystemProject.Services;

namespace InvoiceSystemProject.Controllers
{
    public class InvoiceController : Controller
    {
        CustomerService customerService;
        ProductService productService;
        InvoiceService invoiceService;
        ExtraService extraService;
        public InvoiceController()
        {
            customerService = new CustomerService();
            productService = new ProductService();
            invoiceService = new InvoiceService();
            extraService = new ExtraService();
        }
        public ActionResult Index()
        {
            List<InvoiceModel>invoices=invoiceService.GetAllInvoices();
            return View(invoices);
        }
        public ActionResult NewInvoice()
        {
            ViewBag.customers = new SelectList(customerService.GetAllCustomers(), "customer_id", "customer_name");
            //ViewBag.products = new SelectList(productService.GetAllProducts(), "product_id", "product_name");
            ViewBag.products = GetProducts();
            return View();
        }

        [HttpPost]
        public string GenerateNewInvoice(tblinvoice_details d)
        {
           invoiceService.CreateNewInvoice(d);
            return "Invoice Generated Successfully";
        }
        public JsonResult GetProductById(int id)
        {
            tblproduct pr = productService.GetUniqueProducts().FirstOrDefault(e => e.product_id.Equals(id));
            pr.tblinvoice_products.Clear();    
            return Json(pr,JsonRequestBehavior.AllowGet);
        }


        public ActionResult PayNow(int id)
        {
            InvoiceModel m = invoiceService.GetAllInvoices().FirstOrDefault(e => e.invoice_id.Equals(id));
            ViewData["invoice"] = m;
            tblinvoice_payments p = new tblinvoice_payments() 
            {
             invoice_id= id
            };
            return View(p);
        }

        [HttpPost]
        public ActionResult PayNow(tblinvoice_payments payment)
        {
            invoiceService.AddInvoicePayment(payment);
            ModelState.Clear();
            ViewBag.msg = "Payment Accepted Successfully";
            InvoiceModel m = invoiceService.GetAllInvoices().FirstOrDefault(e => e.invoice_id.Equals(payment.invoice_id));
            ViewData["invoice"] = m;
            tblinvoice_payments p = new tblinvoice_payments()
            {
                invoice_id = payment.invoice_id
            };
            return View(p);
        }

        public ActionResult ViewInvoice(int id)
        {

            tblinvoice_details d=invoiceService.GetInvoiceById(id);
            ViewBag.amount = ExtraService.NumberToWords((int)d.total_amount);
            return View(d);
        }
        public List<SelectListItem> GetProducts()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach(tblproduct p in productService.GetUniqueProducts())
            {
                SelectListItem s = new SelectListItem()
                {
                     Text=p.product_name+" ("+p.weight+")",
                      Value=p.product_id.ToString()
                };
                lst.Add(s);
            }
            return lst;
        }
    }
}