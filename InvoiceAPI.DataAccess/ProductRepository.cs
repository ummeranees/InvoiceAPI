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
    public class ProductRepository : IProductRepository
    {
        private readonly string _filePath;
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _filePath = _configuration["Products_path"];
        }

        public List<Product> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Product>();
            }

            var lines = File.ReadAllLines(_filePath);
            return lines.Select(ConvertFromCsv).ToList();
        }

        public Product Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Product product)
        {
            var products = GetAll();
            product.Id = products.Count > 0 ? products.Max(x => x.Id) + 1 : 1;
            products.Add(product);
            File.WriteAllLines(_filePath, products.Select(ConvertToCsv));
        }

        public void Update(int id, Product product)
        {
            var products = GetAll();
            var index = products.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                products[index] = product;
                File.WriteAllLines(_filePath, products.Select(ConvertToCsv));
            }
        }

        public void Delete(int id)
        {
            var products = GetAll();
            var product = products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                products.Remove(product);
                File.WriteAllLines(_filePath, products.Select(ConvertToCsv));
            }
        }

        private Product ConvertFromCsv(string line)
        {
            var values = line.Split(',');
            return new Product
            {
                Id = int.Parse(values[0]),
                Name = values[1],
                Description = values[2],
                Price = decimal.Parse(values[3]),
                Quantity = int.Parse(values[4]),
                CategoryId = int.Parse(values[5])
            };
        }

        private string ConvertToCsv(Product product)
        {
            return $"{product.Id},{product.Name},{product.Description},{product.Price},{product.Quantity},{product.CategoryId}";
        }
    }
}
