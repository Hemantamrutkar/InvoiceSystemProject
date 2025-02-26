using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceSystemProject.Models;

namespace InvoiceSystemProject.Services
{
    public class InvoiceService
    {
        InvoiceSystemDBEntities db;
        public InvoiceService() 
        {
            db = new InvoiceSystemDBEntities();
        }
        public void CreateNewInvoice(tblinvoice_details d)
        {
            db.tblinvoice_details.Add(d);
            db.SaveChanges();
        }
        public void AddInvoicePayment(tblinvoice_payments p)
        {
            db.tblinvoice_payments.Add(p);
            db.SaveChanges();
        }

        public tblinvoice_details GetInvoiceById(int invoice_id)
        {
            return db.tblinvoice_details.Find(invoice_id);
        }
        public List<InvoiceModel> GetAllInvoices()
        {
            List<InvoiceModel> lst=new List<InvoiceModel> ();
            foreach(tblinvoice_details d in db.tblinvoice_details.ToList())
            {

                float total_amount = (float)d.total_amount;
                float paid_amount = 0, remaining_amount = 0;
                List<tblinvoice_payments> payments = db.tblinvoice_payments.ToList().Where(e => e.invoice_id.Equals(d.invoice_id)).ToList();
                if (payments != null)
                {
                    paid_amount =(float) payments.Sum(e => e.payment_amount);
                }
                remaining_amount = total_amount - paid_amount;
                string status = "";
                if (paid_amount == 0)
                {
                    status = "Un Paid";
                }
                else if(paid_amount>0 && paid_amount < total_amount)
                {
                    status = "Partial Paid";
                }
                else
                {
                    status = "Paid";
                }

                InvoiceModel m = new InvoiceModel()
                {
                    customer_id = (int)d.customer_id,
                    customer_name = d.tblcustomer.customer_name,
                    invoice_date = (DateTime)d.invoice_date,
                    invoice_id = d.invoice_id,
                    total_amount = total_amount,
                    paid_amount = paid_amount,
                    remaining_amount = remaining_amount,
                    status = status
                };
                lst.Add(m);
            }
            return lst;
        }
    }
}