using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceSystemProject.Models;
using InvoiceSystemProject.Services;

namespace InvoiceSystemProject.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService custService;
        public CustomerController()
        {
            custService = new CustomerService();
        }
        public ActionResult Index()
        {
            ViewData["customers"] = custService.GetAllCustomers();
            tblcustomer c = new tblcustomer();
            return View(c);
        }
        [HttpPost]
        public ActionResult Index(tblcustomer customer)
        {
            custService.AddOrUpdateCustomer(customer);
            ViewBag.msg = "Customer Added Successfully";
            ModelState.Clear();
            ViewData["customers"] = custService.GetAllCustomers();

            tblcustomer c = new tblcustomer();
            return View(c);
        }
    }
}