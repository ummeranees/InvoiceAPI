using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPI.DataAccess.Models
{
    public class Invoice
    {
        // automatically next ID number is picked
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceItem> Items { get; set; } /*= new List<InvoiceItem>();*/
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment option is required.")]
        [MaxLength(50, ErrorMessage = "Payment option cannot exceed 50 characters.")]
        public string PaymentOption { get; set; }
        public DateTime Date { get; set; }
        public decimal FlatDiscount { get; set; }
    }
}
