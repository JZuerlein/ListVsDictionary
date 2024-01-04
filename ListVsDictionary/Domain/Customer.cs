using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ListVsDictionary.Domain
{
    public class Customer : IComparable<Customer>
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public int CompareTo(Customer? other)
        {
            if (this.CustomerId < other.CustomerId) return -1;
            if (this.CustomerId == other.CustomerId) return 0;
            return 1;
        }
    }

    public class Order : IComparable<Order>
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int CompareTo(Order? other)
        {
            if (this.OrderId < other.OrderId) return -1;
            if (this.OrderId == other.OrderId) return 0;
            return 1;
        }
    }

    public class OrderItem : IComparable<OrderItem> 
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int Qty { get; set; }
        public decimal PriceCharged { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CompareTo(OrderItem? other)
        {
            if (this.OrderItemId < other.OrderItemId) return -1;
            if (this.OrderItemId == other.OrderItemId) return 0;
            return 1;
        }
    }

    public class SortOrderItemsByOrderId : IComparer<OrderItem>
    {
        public int Compare(OrderItem x, OrderItem y)
        {
            return x.OrderId.CompareTo(y.OrderId);
        }
    }

    public class SortByOrderId : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            return x.OrderId.CompareTo(y.OrderId);
        }
    }

    public class SortOrdersByCustomerId : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            return x.CustomerId.CompareTo(y.CustomerId);
        }
    }

    public class Product : IComparable<Product>
    {
        public int ProductId { get; set; }
        public string Name { set; get; }
        public decimal Price { get; set; }

        public int CompareTo(Product? other)
        {
            if (this.ProductId < other.ProductId) return -1;
            if (this.ProductId == other.ProductId) return 0;
            return 1;
        }
    }
}
