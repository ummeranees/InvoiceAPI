using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPI.DataAccess.Models
{
    public  class Invoicerequest
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Payment option is required.")]
        [MaxLength(50, ErrorMessage = "Payment option cannot exceed 50 characters.")]
        public string PaymentOption { get; set; }


        public List<Item> Items { get; set; }
        public int flatdiscount { get; set; }

    }

    public class Item
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int qty { get; set; }

        [Required(ErrorMessage = "Itemid is required.")]
        public int ItemId { get; set;}
        public decimal ItemDiscount { get; set;}
    }
}
