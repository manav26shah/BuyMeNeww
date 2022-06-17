using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BuyMe.DL.Entities
{
    public class Pincode
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public string city { get; set; }
        public string deliverydays { get; set; }
    }
}
