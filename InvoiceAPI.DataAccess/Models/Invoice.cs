using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPI.DataAccess.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceItem> Items { get; set; } /*= new List<InvoiceItem>();*/
        public decimal TotalAmount { get; set; }
        public string PaymentOption { get; set; }
        public DateTime Date { get; set; }
        public decimal FlatDiscount { get; set; }
    }
}
