using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InvoiceSystemProject.Models;

namespace InvoiceSystemProject.Services
{
    public class ProductService
    {
        InvoiceSystemDBEntities db;
        public ProductService()
        {
            db = new InvoiceSystemDBEntities();
        }
        public void AddOrUpdateProduct(tblproduct product)
        {
            db.tblproducts.AddOrUpdate(product);
            db.SaveChanges();
        }
        public List<tblproduct> GetAllProducts()
        {
            return db.tblproducts.ToList();
        }

        public List<tblproduct> GetUniqueProducts()
        {
            List<tblproduct> lst = new List<tblproduct>();
            foreach(tblproduct p in db.tblproducts.ToList())
            {
                List<tblproduct> pr = db.tblproducts.ToList().Where(e => e.product_name.Equals(p.product_name) & e.weight.Equals(p.weight)).ToList();

                if (pr.Count > 1 )
                {
                    int stock = (int)pr.Sum(e => e.stock_quantity);
                    float rate =(float) pr.Max(e => e.rate);
                    tblproduct pp = pr.Last();
                    tblproduct prod = lst.FirstOrDefault(e => e.product_id.Equals(pp.product_id));
                    if (prod==null)
                    {

                    pp.stock_quantity = stock;
                        pp.rate = rate;
                        pp.tblinvoice_products.Clear();
                      
                    lst.Add(pp);
                    }
                }
                else
                {
                    lst.Add(pr.First());
                }
            }
            return lst;
        }
        public tblproduct GetProductById(int id)
        {
            return db.tblproducts.Find(id);
        }
        public List<tblweight> GetAlLWeights()
        {
            return db.tblweights.ToList();
        }
    }
}