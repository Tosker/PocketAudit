using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTool.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TrackingNumber { get; set; }
        public string PaymentNumber { get; set; }
        public string ConsumerEmail { get; set; }
        public string ConsumerFirstName { get; set; }
        public string ConsumerLastName { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string ConsumerPhoneNumber { get; set; }
        public ShippingCarriers Carrier { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public List<Product> Products { get; set; }
        public double SubTotal { get; set; }
        public double TaxTotal { get; set; }
        public double ShippingTotal { get; set; }
        public double Total { get; set; }

        //Display strings
        public string ConsumerName { get; set; }
        public string ShortDate { get; set; }

        public Transaction()
        {
            ShortDate = Date.ToShortDateString();
            ConsumerName = ConsumerFirstName + " " + ConsumerLastName;
        }
    }
}
