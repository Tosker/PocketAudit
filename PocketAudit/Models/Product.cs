using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTool.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public ProductTypes Type { get; set; }
        public int CarrierId { get; set; }
        
        [JsonIgnore]
        public string CarrierName { get; set; }

        public Product()
        {

        }
    }
}
