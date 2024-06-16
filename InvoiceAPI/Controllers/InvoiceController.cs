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
            return Ok(_invoiceService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            var invoice = _invoiceService.Get(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPost]
        public ActionResult<Invoice> GenerateInvoice([FromBody] Invoicerequest invoice)
        {
            Invoice result = _invoiceService.Add(invoice);
            return Ok(result);
        }
    }

    
    
}
