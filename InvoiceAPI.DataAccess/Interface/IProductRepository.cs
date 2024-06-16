using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.DataAccess.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product Get(int id);
        void Add(Product product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
