using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPI.DataAccess.Models
{
    public class Category
    {
        //if ID is not passed , automatically next ID number is picked
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Category description cannot exceed 200 characters.")]
        public string Description { get; set; }

        [Range(0, 100, ErrorMessage = "Tax must be between 0 and 100.")]
        public decimal Tax { get; set; }
    }
}
