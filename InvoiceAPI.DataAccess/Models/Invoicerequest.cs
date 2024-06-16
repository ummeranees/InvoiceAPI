using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPI.DataAccess.Models
{
    public  class Invoicerequest
    {
        public int CustomerId { get; set; }
        public string PaymentOption { get; set; }
        public List<Item> Items { get; set; }
        public int flatdiscount { get; set; }

    }

    public class Item
    {
        public int qty { get; set; }
        public int ItemId { get; set;}
        public decimal ItemDiscount { get; set;}
    }
}
