using BenchmarkDotNet.Running;

namespace JOS.SystemTextJson.DictionaryStringObject.JsonConverter.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary2 = BenchmarkRunner.Run<JsonConverterBenchmarks>();
        }
    }
}
