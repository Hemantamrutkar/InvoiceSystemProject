using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InvoiceSystemProject.Models;

namespace InvoiceSystemProject.Services
{
    public class CustomerService
    {
        InvoiceSystemDBEntities db;
        public CustomerService()
        {
            db=new InvoiceSystemDBEntities ();
        }

        public void AddOrUpdateCustomer(tblcustomer customer)
        {
            db.tblcustomers.AddOrUpdate(customer);
            db.SaveChanges();
        }
        public List<tblcustomer> GetAllCustomers()
        {
            return db.tblcustomers.ToList();
        }
        public tblcustomer GetCustomerById(int customer_id)
        {
            return db.tblcustomers.Find(customer_id);
        }

        public void DeleteCustomer(int customer_id)
        {
            tblcustomer c = db.tblcustomers.Find(customer_id);
            db.tblcustomers.Remove(c);
            db.SaveChanges();
        }
    }
}