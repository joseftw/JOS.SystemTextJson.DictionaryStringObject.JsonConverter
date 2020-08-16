using System.Collections.Generic;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using JOS.SystemTextJsonDictionaryObjectJsonConverter;

namespace JOS.SystemTextJsonDictionary.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [MemoryDiagnoser]
    public class SerializeBenchmark
    {
        private JsonSerializerOptions _jsonSerializerOptions;
        private JsonSerializerOptions _jsonSerializerOptionsCustomWrite;
        private Dictionary<string, object> _values;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _jsonSerializerOptions = _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Converters = { new DictionaryStringObjectJsonConverter() }
            };
            _jsonSerializerOptionsCustomWrite = _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Converters = { new DictionaryStringObjectJsonConverterCustomWrite() }
            };
            _values = new Dictionary<string, object>
            {
                {"string", "string"},
                {"int", 1},
                {"bool", true},
                {"date", "2020-01-23T00:01:02Z"},
                {"decimal", 12.345M},
                {"null", null},
                {"array", new long[] {1, 2, 3}},
                {
                    "objectArray", new List<Dictionary<string, object>>
                    {
                        {
                            new Dictionary<string, object>
                            {
                                {"string", "string"},
                                {"int", 1},
                                {"bool", true}
                            }
                        },
                        {
                            new Dictionary<string, object>
                            {
                                {"string", "string2"},
                                {"int", 2},
                                {"bool", true}
                            }
                        },
                        {
                            new Dictionary<string, object>
                            {
                                {"string", "string3"},
                                {"int", 3},
                                {"bool", true}
                            }
                        }
                    }
                }
            };
        }

        [Benchmark(Baseline = true)]
        public string Default()
        {
            return JsonSerializer.Serialize(_values);
        }

        [Benchmark]
        public string DictionaryStringObjectJsonConverter()
        {
            return JsonSerializer.Serialize(_values, _jsonSerializerOptions);
        }

        [Benchmark]
        public string DictionaryStringObjectJsonConverterCustomWrite()
        {
            return JsonSerializer.Serialize(_values, _jsonSerializerOptionsCustomWrite);
        }
    }
}
