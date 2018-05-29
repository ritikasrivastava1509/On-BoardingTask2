using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJECT2_CRUD_JQUERY_AJAX.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        [Required (ErrorMessage = "Name is a required field")]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is a required field")]
        [Range(0.05,100)]
        public decimal Price { get; set; }
    }
}