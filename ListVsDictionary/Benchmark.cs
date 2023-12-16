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
        [GlobalSetup]
        public void Setup()
        {
            repo = new TestRepository(1000, 10, 10, 50);
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
    }
}
