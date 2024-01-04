// See https://aka.ms/new-console-template for more information

using ListVsDictionary;
using BenchmarkDotNet.Running;

var repo = new TestRepository(3, 5, 5, 100);

BenchmarkRunner.Run<Benchmark>();

Console.ReadLine();


