using ListVsDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ListVsDictionary
{
    public class TestRepository
    {
        private List<Customer> _customers = new List<Customer>();
        private List<Order> _orders = new List<Order>();
        private List<OrderItem> _orderItems = new List<OrderItem>();
        private List<Product> _products = new List<Product>();

        public TestRepository(int numCustomers, int numOrdersPerCustomer, int numOrderItemsPerOrder, int numProducts)
        {
            for (int i = 1; i <= numProducts; i++)
            {
                _products.Add(new Product() { ProductId = i, Name = i.ToString(), Price = i });
            }

            for (int i = 1; i <= numCustomers; i++)
            {
                _customers.Add(new Customer() { CustomerId = i, Name = i.ToString() });
            }

            for (int i = 1; i <= numCustomers; i++)
            {
                for (int j = 1; j <= numOrdersPerCustomer; j++)
                {
                    _orders.Add(new Order() { OrderId = i * j, CustomerId = i, OrderNumber = (i * j).ToString() });
                }
            }

            var random = new Random();

            for(int i = 1; i <= numCustomers; i++)
            {
                for(int j = 1; j <= numOrdersPerCustomer; j++)
                {
                    for(int z = 1; z <= numOrderItemsPerOrder; z++)
                    {
                        _orderItems.Add(new OrderItem() { OrderItemId = i * j * z, OrderId = i * j, PriceCharged = i * j * z, Qty = 1, ProductId = random.Next(1, numProducts) });
                    }
                }
            }

        }

        public IEnumerable<Customer> GetCustomers()
        {
            foreach(var orderItem in _orderItems)
            {
                orderItem.Product = _products.First(_ => _.ProductId == orderItem.ProductId);
            }

            foreach(var order in _orders)
            {
                order.OrderItems = _orderItems.Where(_ => _.OrderId == order.OrderId).ToList();
            }

            foreach (var customer in _customers)
            {
                customer.Orders = _orders.Where(_ => _.CustomerId ==  customer.CustomerId).ToList();
            }

            return _customers;
        }

        public IEnumerable<Customer> GetCustomersWithSpan()
        {
            var products = CollectionsMarshal.AsSpan<Product>(_products);
            var orderItems = CollectionsMarshal.AsSpan<OrderItem>(_orderItems);
            var orders = CollectionsMarshal.AsSpan<Order>(_orders);
            var customers = CollectionsMarshal.AsSpan<Customer>(_customers);

            foreach (var orderItem in orderItems)
            {
                foreach(var product in products)
                {
                    if (product.ProductId == orderItem.ProductId)
                    {
                        orderItem.Product = product;
                        break;
                    }
                    orderItem.Product = null;    
                }
            }

            foreach (var order in orders)
            {
                var items = new List<OrderItem>();
                foreach (var orderItem in orderItems)
                {
                    if (orderItem.OrderId == order.OrderId)
                    {
                        items.Add(orderItem);
                        break;
                    }
                }
                order.OrderItems = items;
            }

            foreach (var customer in customers)
            {
                var customerOrders = new List<Order>();
                foreach (var order in orders)
                {
                    if (order.CustomerId == customer.CustomerId)
                    {
                        customerOrders.Add(order);
                        break;
                    }
                }
                customer.Orders = customerOrders;
            }

            return _customers;
        }

        public IEnumerable<Customer> GetCustomersWithDictionary()
        {
            foreach (var orderItem in _orderItems)
            {
                orderItem.Product = _products.First(_ => _.ProductId == orderItem.ProductId);
            }

            var orderItems = _orderItems.GroupBy(_ => _.OrderId).ToDictionary(_ => _.Key, _ => _.ToList());
            var orders = _orders.GroupBy(_ => _.CustomerId).ToDictionary(_ => _.Key, _ => _.ToList());

            foreach (var customer in _customers)
            {
                customer.Orders = orders[customer.CustomerId];
                foreach (var order in orders[customer.CustomerId])
                {
                    order.OrderItems = orderItems[order.OrderId];
                }
            }

            return _customers;
        }
    }
}
