using System;
using System.Collections.Generic;
using System.Text.Json;
using Shouldly;
using Xunit;

namespace JOS.SystemTextJson.DictionaryStringObject.JsonConverter;

public class Serialization_DictionaryStringObjectJsonConverterTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions;

    static Serialization_DictionaryStringObjectJsonConverterTests()
    {
        JsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = {new DictionaryStringObjectJsonConverter()}
        };
    }
    
    [Fact]
    public void GivenEmptyDictionary_WhenSerializeAsync_ThenReturnsEmptyObject()
    {
        var input = new Dictionary<string, object>();
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe("{}");
    }
    
    [Fact]
    public void GivenEmptyDictionary_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var input = new Dictionary<string, object>();

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullDictionary_WhenSerializeAsync_ThenReturnsNullString()
    {
        Dictionary<string, object> input = null!;
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe("null");
    }
    
    [Fact]
    public void GivenNullDictionary_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        Dictionary<string, object> input = null!;

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenString_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var input = new Dictionary<string, object>
        {
            ["name"] = "Josef"
        };
        var expected = "{\"name\":\"Josef\"}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expected);
    }
    
    [Fact]
    public void GivenString_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var input = new Dictionary<string, object>
        {
            ["name"] = "Josef"
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullString_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        string? value = null;
        var input = new Dictionary<string, object>
        {
            ["name"] = value!
        };
        var expected = "{\"name\":null}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expected);
    }
    
    [Fact]
    public void GivenNullString_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        string? value = null;
        var input = new Dictionary<string, object>
        {
            ["name"] = value!
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenDateTime_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = new DateTime(2020, 01, 23, 01, 02, 03, DateTimeKind.Utc);
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expected = "{\"name\":\"2020-01-23T01:02:03Z\"}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expected);
    }
    
    [Fact]
    public void GivenDateTime_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = new DateTime(2020, 01, 23, 01, 02, 03, DateTimeKind.Utc);
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullDateTime_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        DateTime? value = null;
        var input = new Dictionary<string, object>
        {
            ["name"] = value!
        };
        var expected = "{\"name\":null}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expected);
    }
    
    [Fact]
    public void GivenNullDateTime_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        DateTime? value = null;
        var input = new Dictionary<string, object>
        {
            ["name"] = value!
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenBool_WhenSerializeAsync_ThenShouldDeserializeCorrectly(bool value)
    {
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expected = $"{{\"name\":{value.ToString().ToLower()}}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expected);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenBool_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer(bool value)
    {
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullBool_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        bool? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expected = $"{{\"name\":null}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expected);
    }
    
    [Fact]
    public void GivenNullBool_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        bool? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenDecimal_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = 1M;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":1}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenDecimal_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = 1M;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullDecimal_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        decimal? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":null}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenNullDecimal_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        decimal? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };

        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenDecimalWithDecimalPoints_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = 1.01M;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":1.01}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenDecimalWithDecimalPoints_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = 1.01M;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenDouble_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = 1D;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":1}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenDouble_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = 1D;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullDouble_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        double? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":null}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenNullDouble_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        double? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenDoubleWithDecimalPoints_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = 1.01D;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":1.01}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenDoubleWithDecimalPoints_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = 1.01D;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenFloat_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = 1F;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":1}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenFloat_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = 1F;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenNullFloat_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        float? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":null}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenNullFloat_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        float? value = null!;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenFloatWithDecimalPoints_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = 1.01F;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":1.01}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenFloatWithDecimalPoints_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = 1.01F;
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
   
    [Fact]
    public void GivenClass_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = new MyItem {Property = "Hello"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":{{\"Property\":\"Hello\"}}}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenClass_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = new MyItem {Property = "Hello"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }

    [Fact]
    public void GivenSimpleList_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = new List<string> {"Item 1", "Item 2", "Item 3"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":[\"Item 1\",\"Item 2\",\"Item 3\"]}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenSimpleList_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = new List<string> {"Item 1", "Item 2", "Item 3"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenSimpleArray_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = new[] {"Item 1", "Item 2", "Item 3"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":[\"Item 1\",\"Item 2\",\"Item 3\"]}}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }

    [Fact]
    public void GivenSimpleArray_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = new[] {"Item 1", "Item 2", "Item 3"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }
    
    [Fact]
    public void GivenComplexList_WhenSerializeAsync_ThenShouldDeserializeCorrectly()
    {
        var value = new List<MyItem>
        {
            new() {Property = "Property 1"},
            new() {Property = "Property 2"},
            new() {Property = "Property 3"}
        };
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = "{\"name\":[{\"Property\":\"Property 1\"},{\"Property\":\"Property 2\"},{\"Property\":\"Property 3\"}]}";
        
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void GivenComplexList_WhenSerializeAsync_ThenBehavesSameAsDefaultSerializer()
    {
        var value = new List<MyItem>
        {
            new() {Property = "Property 1"},
            new() {Property = "Property 2"},
            new() {Property = "Property 3"}
        };
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        
        var defaultResult = JsonSerializer.Serialize(input);
        var result = JsonSerializer.Serialize(input, JsonSerializerOptions);

        result.ShouldBe(defaultResult);
    }

    [Fact]
    public void RespectsPropertyNamingPolicy_Class()
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerOptions)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var value = new MyItem {Property = "Hello"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":{{\"property\":\"Hello\"}}}}";
        
        var result = JsonSerializer.Serialize(input, jsonOptions);

        result.ShouldBe(expectedJson);
    }
    
    [Fact]
    public void RespectsPropertyNamingPolicy_ComplexList()
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerOptions)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var value = new List<MyItem>
        {
            new() {Property = "Property 1"}
        };
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expected = "{\"name\":[{\"property\":\"Property 1\"}]}";
        
        var result = JsonSerializer.Serialize(input, jsonOptions);

        result.ShouldBe(expected);
    }

    [Fact]
    public void RespectsPropertyNamingPolicy_SimpleList()
    {
        var jsonOptions = new JsonSerializerOptions(JsonSerializerOptions)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var value = new[] {"Item 1", "Item 2", "Item 3"};
        var input = new Dictionary<string, object>
        {
            ["name"] = value
        };
        var expectedJson = $"{{\"name\":[\"Item 1\",\"Item 2\",\"Item 3\"]}}";
        
        var result = JsonSerializer.Serialize(input, jsonOptions);

        result.ShouldBe(expectedJson);
    }
    
    public class MyItem
    {
        public string Property { get; set; } = null!;
    }
}