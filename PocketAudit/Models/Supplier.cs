using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTool.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
