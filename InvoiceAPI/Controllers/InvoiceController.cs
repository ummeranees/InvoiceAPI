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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IConfiguration _configuration;

        public InvoiceController(IInvoiceService invoiceService,IConfiguration configuration )
        {
            _invoiceService = invoiceService;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<Invoice>> GetInvoices()
        {
            try
            {
                return Ok(_invoiceService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            try
            {
                var invoice = _invoiceService.Get(id);
                if (invoice == null)
                    return NotFound();

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Invoice> GenerateInvoice([FromBody] Invoicerequest invoice)
        {
            try
            {
                Invoice result = _invoiceService.Add(invoice);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    
    
}
