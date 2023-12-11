using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ListVsDictionary.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int Qty { get; set; }
        public decimal PriceCharged { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { set; get; }
        public decimal Price { get; set; }
    }
}
