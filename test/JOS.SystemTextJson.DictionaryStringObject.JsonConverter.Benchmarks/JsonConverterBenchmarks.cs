using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace JOS.SystemTextJson.DictionaryStringObject.JsonConverter.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net70)]
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class JsonConverterBenchmarks
    {
        private readonly BenchmarkHelper _benchmarkHelper;

        public JsonConverterBenchmarks()
        {
            _benchmarkHelper = new BenchmarkHelper();
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Serialize")]
        public string Serialize_Default()
        {
            return _benchmarkHelper.SerializeDefault();
        }
        
        [Benchmark]
        [BenchmarkCategory("Serialize")]
        public string Serialize_Custom()
        {
            return _benchmarkHelper.SerializeCustom();
        }
        
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Deserialize")]
        public Dictionary<string, object>? Deserialize_Default()
        {
            return _benchmarkHelper.DeserializeDefault();
        }

        [Benchmark]
        [BenchmarkCategory("Deserialize")]
        public Dictionary<string, object>? Deserialize_Custom()
        {
            return _benchmarkHelper.DeserializeCustom();
        }
    }
}
