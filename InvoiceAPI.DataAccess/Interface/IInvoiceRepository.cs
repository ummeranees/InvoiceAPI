using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.DataAccess.Interface
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetAll();
        Invoice Get(int id);
        void Add(Invoice invoice);
    }
}
