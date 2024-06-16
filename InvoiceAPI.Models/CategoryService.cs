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
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IConfiguration configuration, ICategoryRepository categoryRepository)
        {
            _configuration = configuration;
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            try
            {
                var result = _categoryRepository.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        public Category Get(int id)
        {
            try
            {
                return _categoryRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Category category)
        {
            try
            {
                _categoryRepository.Add(category);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Update(int id, Category updatedCategory)
        {
            try
            {
                _categoryRepository.Update(id, updatedCategory);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Delete(int id)
        {
            try
            {
                _categoryRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //private List<Category> ReadFromFile()
        //{
        //    var categories = new List<Category>();
        //    if (File.Exists(_filePath))
        //    {
        //        var lines = File.ReadAllLines(_filePath);
        //        foreach (var line in lines)
        //        {
        //            var parts = line.Split(',');
        //            categories.Add(new Category
        //            {
        //                Id = int.Parse(parts[0]),
        //                Name = parts[1],
        //                Description = parts[2],
        //                Tax = decimal.Parse(parts[3])
        //            });
        //        }
        //    }
        //    return categories;
        //}

        //private void WriteToFile(List<Category> categories)
        //{
        //    var lines = categories.Select(c => $"{c.Id},{c.Name},{c.Description},{c.Tax}");
        //    File.WriteAllLines(_filePath, lines);
        //}
    }
}
