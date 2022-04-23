using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.DTO.Request
{
    public class ProductRequest
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Category to which this prodcut belongs, if you dont know the category consult admin/db
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        /// <summary>
        /// the maximum retail price
        /// </summary>
        [Required]
        public decimal MRP { get; set; }

        /// <summary>
        /// The discount offered
        /// </summary>
        public string Discount { get; set; }
        public bool InStock { get; set; } = true;
        public int MaxOrderAmount { get; set; } = 5;
    }
}
