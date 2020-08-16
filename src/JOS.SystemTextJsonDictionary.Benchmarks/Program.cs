using BenchmarkDotNet.Running;

namespace JOS.SystemTextJsonDictionary.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary1 = BenchmarkRunner.Run<DeserializeBenchmark>();
            var summary2 = BenchmarkRunner.Run<SerializeBenchmark>();
        }
    }
}
