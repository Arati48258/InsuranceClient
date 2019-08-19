using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication2.Models
{
    public class Customer:TableEntity
    {

        public Customer(string customerId, string insuranceType)
        {
            this.RowKey = customerId;
            this.PartitionKey = insuranceType;
        }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public double Amount { get; set; }
        public DateTime AppDate { get; set; }

        public DateTime EndDate { get; set; }
        public string ImageUrl { get; set; }
        public string ImageURL { get; internal set; }
        public double Premium { get; internal set; }
    }
}
