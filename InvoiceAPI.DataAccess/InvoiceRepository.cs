using InvoiceAPI.DataAccess.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.DataAccess
{
    public class InvoiceRepository : IInvoiceRepository
    {

        private readonly string _filePath;
        private readonly IConfiguration _configuration;

        public InvoiceRepository(  IConfiguration configuration)
        {
            _configuration = configuration;
            _filePath = _configuration["Invoice_path"];
        }

        public List<Invoice> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Invoice>();
            }

            var lines = File.ReadAllLines(_filePath);
            return lines.Select(ConvertFromCsv).ToList();
        }

        public Invoice Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Invoice invoice)
        {
            var invoices = GetAll();
            invoice.Id = invoices.Count > 0 ? invoices.Max(x => x.Id) + 1 : 1;
            invoices.Add(invoice);
            File.WriteAllLines(_filePath, invoices.Select(ConvertToCsv));
        }

        private Invoice ConvertFromCsv(string line)
        {
            var values = line.Split(';');
            return new Invoice
            {
                Id = int.Parse(values[0]),
                CustomerId = int.Parse(values[1]),
                TotalAmount = decimal.Parse(values[2]),
                PaymentOption = values[3],
                Date = DateTime.Parse(values[4]),
                Items = values[5].Split('|').Select(ConvertFromCsvItem).ToList()
            };
        }

        private InvoiceItem ConvertFromCsvItem(string item)
        {
            var values = item.Split(',');
            return new InvoiceItem
            {
                ProductId = int.Parse(values[0]),
                Quantity = int.Parse(values[1]),
                Price = decimal.Parse(values[2]),
                Tax = decimal.Parse(values[3])
            };
        }

        private string ConvertToCsv(Invoice invoice)
        {
            var items = string.Join('|', invoice.Items.Select(ConvertToCsvItem));
            return $"{invoice.Id};{invoice.CustomerId};{invoice.TotalAmount};{invoice.PaymentOption};{invoice.Date};{items}";
        }

        private string ConvertToCsvItem(InvoiceItem item)
        {
            return $"{item.ProductId},{item.Quantity},{item.Price}";
        }
        
    }
}
