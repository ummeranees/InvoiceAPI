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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _filePath;
        private readonly IConfiguration _configuration;
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _filePath = _configuration["Customer_path"];
        }

        public List<Customer> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Customer>();
            }

            var lines = File.ReadAllLines(_filePath);
            return lines.Select(ConvertFromCsv).ToList();
        }

        public Customer Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Customer customer)
        {
            var customers = GetAll();
            customer.Id = customers.Count > 0 ? customers.Max(x => x.Id) + 1 : 1;
            customers.Add(customer);
            File.WriteAllLines(_filePath, customers.Select(ConvertToCsv));
        }

        public void Update(int id, Customer customer)
        {
            var customers = GetAll();
            var index = customers.FindIndex(x => x.Id == id);
            if (index >= 0)
            {
                customers[index] = customer;
                File.WriteAllLines(_filePath, customers.Select(ConvertToCsv));
            }
        }

        public void Delete(int id)
        {
            var customers = GetAll();
            var customer = customers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                File.WriteAllLines(_filePath, customers.Select(ConvertToCsv));
            }
        }

        private Customer ConvertFromCsv(string line)
        {
            var values = line.Split(',');
            return new Customer
            {
                Id = int.Parse(values[0]),
                Name = values[1],
                Email = values[2],
                Address = values[3],
                ContactNumber = values[4]
            };
        }

        private string ConvertToCsv(Customer customer)
        {
            return $"{customer.Id},{customer.Name},{customer.Email},{customer.Address},{customer.ContactNumber}";
        }
    }
}
