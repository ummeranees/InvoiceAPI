using InvoiceAPI.BP.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;
using InvoiceAPI.DataAccess.Interface;

namespace InvoiceAPI.BP
{
    public class ProductService : IProductService
    {
        private readonly string _filePath;
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            _filePath = _configuration["Products_path"];

        }

        public List<Product> GetAll()
        {
            return ReadFromFile();
        }

        public Product Get(int id)
        {
            return ReadFromFile().FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            var products = ReadFromFile();
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            WriteToFile(products);
        }

        public void Update(int id, Product updatedProduct)
        {
            var products = ReadFromFile();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Price = updatedProduct.Price;
                product.Quantity = updatedProduct.Quantity;
                product.CategoryId = updatedProduct.CategoryId;
                WriteToFile(products);
            }
        }

        public void Delete(int id)
        {
            var products = ReadFromFile();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                WriteToFile(products);
            }
        }

        private List<Product> ReadFromFile()
        {
            var products = new List<Product>();
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    products.Add(new Product
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Description = parts[2],
                        Price = decimal.Parse(parts[3]),
                        Quantity = int.Parse(parts[4]),
                        CategoryId = int.Parse(parts[5])
                    });
                }
            }
            return products;
        }

        private void WriteToFile(List<Product> products)
        {
            var lines = products.Select(p => $"{p.Id},{p.Name},{p.Description},{p.Price},{p.Quantity},{p.CategoryId}");
            File.WriteAllLines(_filePath, lines);
        }

        private string FilePath
        {
            get {
                string path = string.Empty;
                if (_configuration != null)
                {
                    path = _configuration["Products_path"];
                    var dir = AppDomain.CurrentDomain.BaseDirectory;
                    var index = dir.LastIndexOf("bin");
                    if(index < 0)
                    {
                        path = Path.Combine(dir, path.Substring(2));
                    }
                    else
                    {
                        path = Path.Combine(dir.Remove(index), path.Substring(2));
                    }
                    return path;
                }

                return null;
            }

        }

        //private string ResolvePath(string path)
        //{

        //}
    }

    // Repeat similar structure for CategoryService, CustomerService, and InvoiceService
}
