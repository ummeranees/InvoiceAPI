using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.DataAccess.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category Get(int id);
        void Add(Category category);
        void Update(int id, Category category);
        void Delete(int id);
    }
}
