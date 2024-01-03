// See https://aka.ms/new-console-template for more information

using ListVsDictionary;
using BenchmarkDotNet.Running;

var repo = new TestRepository(3, 3, 3, 27);

var customers = repo.GetCustomers();

foreach (var customer in customers)
{
    Console.WriteLine("CustomerID = " + customer.CustomerId);

    foreach (var order in customer.Orders)
    {
        Console.WriteLine("OrderId = " + order.OrderId);
        Console.WriteLine("Order.CustomerId = " + order.CustomerId);

        foreach (var orderItem in order.OrderItems)
        {
            Console.WriteLine("OrderItemId = " + orderItem.OrderItemId);
            Console.WriteLine("OrderItem.OrderId = " + orderItem.OrderId);
        }
    }
}


//BenchmarkRunner.Run<Benchmark>();

Console.ReadLine();


