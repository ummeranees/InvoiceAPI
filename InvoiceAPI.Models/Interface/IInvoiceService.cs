using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.BP.Interface
{
    public interface IInvoiceService
    {
        List<Invoice> GetAll();
        Invoice Get(int id);
        void Add(Invoicerequest invoice);
    }
}
