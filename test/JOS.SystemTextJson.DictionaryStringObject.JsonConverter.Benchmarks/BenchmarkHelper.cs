using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JOS.SystemTextJson.DictionaryStringObject.JsonConverter.Benchmarks;

public class BenchmarkHelper
{
    private static readonly JsonSerializerOptions? JsonSerializerOptions;
    private static readonly Dictionary<string, object> Values;
    private static readonly string Json;
    
    static BenchmarkHelper()
    {
        JsonSerializerOptions = JsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            Converters = { new DictionaryStringObjectJsonConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        Json = File.ReadAllText("testJson.json");
        Values = new Dictionary<string, object>
        {
            {"string", "string"},
            {"int", 1},
            {"bool", true},
            {"date", "2020-01-23T00:01:02Z"},
            {"decimal", 12.345M},
            {"null", null!},
            {"array", new long[] {1, 2, 3}},
            {
                "objectArray", new List<Dictionary<string, object>>
                {
                    {
                        new()
                        {
                            {"string", "string"},
                            {"int", 1},
                            {"bool", true}
                        }
                    },
                    {
                        new()
                        {
                            {"string", "string2"},
                            {"int", 2},
                            {"bool", true}
                        }
                    },
                    {
                        new()
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

    public string SerializeDefault()
    {
        return JsonSerializer.Serialize(Values);
    }
    
    public string SerializeCustom()
    {
        return JsonSerializer.Serialize(Values, JsonSerializerOptions);
    }
    
    public Dictionary<string, object>? DeserializeDefault()
    {
        return JsonSerializer.Deserialize<Dictionary<string, object>>(Json);
    }
    
    public Dictionary<string, object>? DeserializeCustom()
    {
        return JsonSerializer.Deserialize<Dictionary<string, object>>(Json, JsonSerializerOptions);
    }
}