using InvoiceAPI.BP.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;
using InvoiceAPI.DataAccess.Interface;
using InvoiceAPI.DataAccess;

namespace InvoiceAPI.BP
{
    public class InvoiceService : IInvoiceService
    {
        private readonly string _filePath;
        private readonly ICategoryService _categoryService;
        private readonly   IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;
        public InvoiceService(ICategoryService categoryservice, IProductService productService,IConfiguration configuration, ICustomerService customerService)
        {
            _configuration = configuration;
            _categoryService = categoryservice;
            _productService = productService;
            _filePath = _configuration["Invoice_path"];
            _customerService = customerService;
        }

        public void Add(Invoicerequest invoice)
        {
            Invoice invoice1 = new Invoice();
            invoice1 = CalculateInvoice(invoice);
            var lines = File.ReadAllLines(_filePath).ToList();
            var newId = lines.Any() ? int.Parse(lines.Last().Split(',')[0]) + 1 : 1;
            invoice1.Id = newId;
            var invoiceData = $"{invoice1.Id},{invoice1.CustomerId},{invoice1.Date=DateTime.Now},{invoice.PaymentOption},{invoice1.TotalAmount},{invoice1.FlatDiscount}";
            foreach (var item in invoice1.Items)
            {
                invoiceData += $",{item.ProductId},{item.Quantity},{item.Price},{item.Tax},{item.Discount}";
            }
            lines.Add(invoiceData);
            File.WriteAllLines(_filePath, lines);
            //_customerService.Add(invoice1.Customer);
        }

        public Invoice Get(int id)
        {
            var lines = File.ReadAllLines(_filePath);
            var line = lines.FirstOrDefault(l => l.Split(',')[0] == id.ToString());
            if (line == null) return null;

            var parts = line.Split(',');
            var invoice = new Invoice
            {
                Id = int.Parse(parts[0]),
                CustomerId = int.Parse(parts[1]),
                Date = DateTime.Parse(parts[2]),
                PaymentOption = parts[3],
                TotalAmount = decimal.Parse(parts[4]),
                FlatDiscount = decimal.Parse(parts[5]),
                Items = new List<InvoiceItem>()
            };

            for (int i = 6; i < parts.Length; i += 5)
            {
                invoice.Items.Add(new InvoiceItem
                {
                    ProductId = int.Parse(parts[i]),
                    Quantity = int.Parse(parts[i + 1]),
                    Price = decimal.Parse(parts[i + 2]),
                    Tax = decimal.Parse(parts[i + 3]),
                    Discount = decimal.Parse(parts[i + 4])
                });
            }

            invoice.Customer = _customerService.Get(invoice.CustomerId);
            return invoice;
        }

        public List<Invoice> GetAll()
        {
            var lines = File.ReadAllLines(_filePath);
            var invoices = new List<Invoice>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var invoice = new Invoice
                {
                    Id = int.Parse(parts[0]),
                    CustomerId = int.Parse(parts[1]),
                    Date = DateTime.Parse(parts[2]),
                    PaymentOption = parts[3],
                    TotalAmount = decimal.Parse(parts[4]),
                    FlatDiscount = decimal.Parse(parts[5]),
                    Items = new List<InvoiceItem>()
                };

                for (int i = 6; i < parts.Length; i += 5)
                {
                    invoice.Items.Add(new InvoiceItem
                    {
                        ProductId = int.Parse(parts[i]),
                        Quantity = int.Parse(parts[i + 1]),
                        Price = decimal.Parse(parts[i + 2]),
                        Tax = decimal.Parse(parts[i + 3]),
                        Discount = decimal.Parse(parts[i + 4])
                    });
                }

                invoice.Customer = _customerService.Get(invoice.CustomerId);
                invoices.Add(invoice);
            }

            return invoices;
        }

        public Invoice CalculateInvoice(Invoicerequest invoicereq)
        {
            Invoice invoice = new Invoice();
            invoice.Items = new List<InvoiceItem>();
            decimal totalAmount = 0;
            invoice.CustomerId = invoicereq.CustomerId;
            invoice.FlatDiscount = invoicereq.flatdiscount;
            invoice.Date = DateTime.Now;
            invoice.PaymentOption = invoicereq.PaymentOption;

            foreach (var item in invoicereq.Items)
            {
                InvoiceItem l = new InvoiceItem();
                var product = _productService.Get(item.ItemId);
                if (product != null)
                {
                    l.ProductId = product.Id; l.Quantity = item.qty;l.Discount = item.ItemDiscount;
                    var category = _categoryService.Get(product.CategoryId);
                    l.Price = product.Price;
                    l.Tax = product.Price * (category.Tax / 100);
                    totalAmount = (l.Price * item.qty) + (l.Tax * item.qty) - item.ItemDiscount;
                }
                invoice.Items.Add(l);
            }

            totalAmount -= invoicereq.flatdiscount;
            invoice.TotalAmount = totalAmount;
            invoice.Customer = _customerService.Get(invoicereq.CustomerId);

            return invoice;
        }
    }
}
