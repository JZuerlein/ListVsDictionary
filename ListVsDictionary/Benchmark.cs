using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListVsDictionary
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Benchmark
    {
        private TestRepository repo;
        private int i = 0;

        [GlobalSetup]
        public void Setup()
        {
            repo = new TestRepository(100, 10, 10, 50);
            i = 0;
        }

        [Benchmark]
        public void Get()
        {
            var customers = repo.GetCustomers();
        }

        [Benchmark]
        public void GetWithDictionary()
        {
            var customers = repo.GetCustomersWithDictionary();
        }

        [Benchmark]
        public void GetWithSpan()
        {
            var customers = repo.GetCustomersWithSpan();
        }

        [Benchmark]
        public void GetWithSortedSpanAndSlice()
        {
            var customers = repo.GetCustomerWithSortedSpanAndSlice();
        }

        [Benchmark]
        public void GetWithSortedSpanAndAdd()
        {
            var customers = repo.GetCustomerWithSortedSpanAndAdd();
        }
    }
}
