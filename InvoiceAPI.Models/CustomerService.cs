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
            try
            {
                return _customerRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Customer Get(int id)
        {
            try
            {
                return _customerRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Customer customer)
        {
            try
            {
                _customerRepository.Add(customer);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Update(int id, Customer updatedCustomer)
        {
            try
            {
                _customerRepository.Update(id, updatedCustomer);
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
                _customerRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
