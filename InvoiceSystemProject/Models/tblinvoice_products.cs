//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceSystemProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblinvoice_products
    {
        public int invoice_product_id { get; set; }
        public Nullable<int> invoice_id { get; set; }
        public Nullable<int> product_id { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual tblinvoice_details tblinvoice_details { get; set; }
        public virtual tblproduct tblproduct { get; set; }
    }
}
