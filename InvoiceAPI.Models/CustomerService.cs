using InvoiceAPI.BP.Interface;
using Microsoft.Extensions.Configuration;
using InvoiceAPI.DataAccess.Models;
using InvoiceAPI.DataAccess.Interface;

namespace InvoiceAPI.BP
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(IConfiguration configuration,ICustomerRepository customerRepository)
        {
            _configuration = configuration;
            _customerRepository = customerRepository;
        }
        

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer Get(int id)
        {
            return _customerRepository.Get(id);
        }

        public void Add(Customer customer)
        {
            _customerRepository.Add(customer);

        }

        public void Update(int id, Customer updatedCustomer)
        {
            _customerRepository.Update(id, updatedCustomer);

        }

        public void Delete(int id)
        {
            _customerRepository.Delete(id);

        }

    }
}
