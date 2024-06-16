using InvoiceAPI.BP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceAPI.DataAccess.Models;
using InvoiceAPI.BP.Interface;

namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;

        public CustomerController( IConfiguration configuration,ICustomerService customerService)
        {
            _configuration = configuration;
            _customerService =  customerService;


        }

        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customerService.Get(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult AddCustomer([FromBody] Customer customer)
        {
            _customerService.Add(customer);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (_customerService.Get(id) == null)
                return NotFound();

            _customerService.Update(id, customer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            if (_customerService.Get(id) == null)
                return NotFound();

            _customerService.Delete(id);
            return Ok();
        }
    }
}
