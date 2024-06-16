using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.BP.Interface
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(int id, Customer customer);
        void Delete(int id);
    }
}
