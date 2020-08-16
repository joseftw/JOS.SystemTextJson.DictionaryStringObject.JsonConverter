using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using JOS.SystemTextJsonDictionaryObjectJsonConverter;

namespace JOS.SystemTextJsonDictionary.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [MemoryDiagnoser]
    public class DeserializeBenchmark
    {
        private string _json;
        private JsonSerializerOptions _jsonSerializerOptions;
        private Stream _stream;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _json = File.ReadAllText("testJson.json");
            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Converters = { new DictionaryStringObjectJsonConverter() }
            };
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _stream = new MemoryStream(Encoding.UTF8.GetBytes(_json));
        }

        [Benchmark(Baseline = true)]
        public async Task<Dictionary<string, object>> Default()
        {
            return await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(_stream);
        }

        [Benchmark]
        public async Task<Dictionary<string, object>> CustomJsonConverter()
        {
            return await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(_stream, _jsonSerializerOptions);
        }
    }
}
