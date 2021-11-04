using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomsample.Models
{
    public class Product
    {
        public int Id { get; set; }
    
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "name not null")]
        public string PName { get; set; }
        [Range(10,20 ,ErrorMessage = "num between")]
        public int price { get; set; }
        [NotMapped]
        public IFormFile p_image { get; set; }

        public string productimageurl { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }
}
