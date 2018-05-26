
using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ProductApi.Models
{
   public class Product
   {
       public Guid ProductId { get; set;}
       
       [JsonProperty("productName")]
       [Required(ErrorMessage = "Product Name is required")]
       public string Name { get; set; }

       [JsonProperty("productDescription")]
       public string Description { get; set; }

       [JsonProperty("price")]
       [Required(ErrorMessage = "Price is required")]
       public decimal Price { get; set; }

       [JsonProperty("count")]
       [Range(0, int.MaxValue, ErrorMessage = "Product cannot have a negative count")]
       public int Count { get; set; }
       public int InOrder { get; set; }
   }
}