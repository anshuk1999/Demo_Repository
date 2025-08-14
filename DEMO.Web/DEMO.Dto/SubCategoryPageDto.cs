using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto
{
    public class SubCategoryPageDto
    {
        public int Id { get; set; }
        //[Required]
        public string? SubName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public int CategoryId { get; set; } // Foreign key to Category
        public string? Name { get; set; } // Navigation property for Category name
    }
}
