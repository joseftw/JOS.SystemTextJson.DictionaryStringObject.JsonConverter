using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace JOS.SystemTextJson.DictionaryStringObject.JsonConverter;

public class Deserialization_DefaultTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions;

    static Deserialization_DefaultTests()
    {
        JsonSerializerOptions = new JsonSerializerOptions();
    }

    [Fact]
    public async Task GivenEmptyJsonObject_WhenDeserializeAsync_ThenReturnsEmptyDictionary()
    {
        var jsonString = "{}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result.ShouldBeEmpty();
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task
        GivenObjectWithStringPropertyWithDateTimeValue_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsDateTime()
    {
        var jsonString = "{\"name\": \"2020-01-23T01:02:03Z\"}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<DateTime>();
        result!["name"].ShouldBeEquivalentTo(new DateTime(2020, 1, 23, 1, 2, 3, 0, DateTimeKind.Utc));
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task GivenObjectWithIntProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsLong()
    {
        var jsonString = "{\"name\": 1}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<int>();
        result!["name"].ShouldBe(1);
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task GivenObjectWithDecimalProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsDecimal()
    {
        var jsonString = "{\"name\": 1.234}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<decimal>();
        result!["name"].ShouldBe(1.234M);
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task GivenObjectWithBoolProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsBool()
    {
        var jsonString = "{\"name\": true}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<bool>();
        result!["name"].ShouldBe(true);
    }

    [Fact]
    public async Task
        GivenObjectWithNullPropertyValue_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithNullValue()
    {
        var jsonString = "{\"name\": null}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeNull();
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task GivenObjectWithArrayProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithArrayValue()
    {
        var jsonString = "{\"name\": [1,2,3]}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<List<object>>();
        var array = (List<object>) result!["name"];
        array.Count.ShouldBe(3);
        array.ShouldContain(1L);
        array.ShouldContain(2L);
        array.ShouldContain(3L);
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task
        GivenObjectWithObjectProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithNestedDictionary()
    {
        var jsonString = "{\"name\": {\"property\": 100}}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<List<Dictionary<string, object>>>();
        var nestedObject = (Dictionary<string, object>) result!["name"];
        nestedObject.Count.ShouldBe(1);
        nestedObject["property"].ShouldBe(100L);
    }

    [Fact(DisplayName = "This should fail - if it succeeds, the default implementation works - no need for custom converter")]
    public async Task
        GivenObjectPropertyWithArrayProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithNestedDictionaryAndArrayProperty()
    {
        var jsonString = "{\"name\": {\"property\": [1,2,3]}}";
        var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        var result =
            await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions,
                CancellationToken.None);

        result!.ShouldContainKey("name");
        result!["name"].ShouldBeOfType<Dictionary<string, object>>();
        var nestedObject = (Dictionary<string, object>) result!["name"];
        nestedObject.ShouldContainKey("property");
        var array = (List<object>) nestedObject["property"];
        array.Count.ShouldBe(3);
        array.ShouldContain(1L);
        array.ShouldContain(2L);
        array.ShouldContain(3L);
    }
}