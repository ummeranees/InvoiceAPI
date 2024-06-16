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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _filePath;
        private readonly IConfiguration _configuration;
        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _filePath = _configuration["Category_path"];
        }

        public List<Category> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Category>();
            }

            var lines = File.ReadAllLines(_filePath);
            return lines.Select(ConvertFromCsv).ToList();
        }

        public Category Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Category category)
        {
            var categories = GetAll();
            category.Id = categories.Count > 0 ? categories.Max(x => x.Id) + 1 : 1;
            categories.Add(category);
            File.WriteAllLines(_filePath, categories.Select(ConvertToCsv));
        }

        public void Update(int id, Category category)
        {
            var categories = GetAll();
            var index = categories.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                categories[index] = category;
                File.WriteAllLines(_filePath, categories.Select(ConvertToCsv));
            }
        }

        public void Delete(int id)
        {
            var categories = GetAll();
            var category = categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                categories.Remove(category);
                File.WriteAllLines(_filePath, categories.Select(ConvertToCsv));
            }
        }

        private Category ConvertFromCsv(string line)
        {
            var values = line.Split(',');
            return new Category
            {
                Id = int.Parse(values[0]),
                Name = values[1],
                Description = values[2],
                Tax = decimal.Parse(values[3])
            };
        }

        private string ConvertToCsv(Category category)
        {
            return $"{category.Id},{category.Name},{category.Description},{category.Tax}";
        }
    }
}
