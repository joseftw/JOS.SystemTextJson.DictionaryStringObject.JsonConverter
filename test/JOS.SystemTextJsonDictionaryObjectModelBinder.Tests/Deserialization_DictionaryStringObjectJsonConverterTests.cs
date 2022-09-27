using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace JOS.SystemTextJsonDictionaryObjectJsonConverter.Tests
{
    public class Deserialization_DictionaryStringObjectJsonConverterTests
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions;

        static Deserialization_DictionaryStringObjectJsonConverterTests()
        {
            JsonSerializerOptions = new JsonSerializerOptions
            {
                Converters = {new DictionaryStringObjectJsonConverter()}
            };
        }
        
        [Fact]
        public async Task GivenEmptyJsonObject_WhenDeserializeAsync_ThenReturnsEmptyDictionary()
        {
            var jsonString = "{}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result.ShouldBeEmpty();
        }

        [Fact]
        public async Task GivenObjectWithStringPropertyWithDateTimeValue_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsDateTime()
        {
            var jsonString = "{\"name\": \"2020-01-23T01:02:03Z\"}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKeyAndValue("name", new DateTime(2020, 1, 23, 1, 2, 3, DateTimeKind.Utc));
        }

        [Fact]
        public async Task GivenObjectWithIntProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsLong()
        {
            var jsonString = "{\"name\": 1}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKeyAndValue("name", 1L);
        }

        [Fact]
        public async Task GivenObjectWithDecimalProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsDecimal()
        {
            var jsonString = "{\"name\": 1.234}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKeyAndValue("name", 1.234M);
        }

        [Fact]
        public async Task GivenObjectWithBoolProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyAsBool()
        {
            var jsonString = "{\"name\": true}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            
            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKeyAndValue("name", true);
        }

        [Fact]
        public async Task GivenObjectWithNullPropertyValue_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithNullValue()
        {
            var jsonString = "{\"name\": null}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKeyAndValue("name", null);
        }

        [Fact]
        public async Task GivenObjectWithArrayProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithArrayValue()
        {
            var jsonString = "{\"name\": [1,2,3]}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKey("name");
            var array = (List<object>)result!["name"];
            array.Count.ShouldBe(3);
            array.ShouldContain(1L);
            array.ShouldContain(2L);
            array.ShouldContain(3L);
        }

        [Fact]
        public async Task GivenObjectWithObjectProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithNestedDictionary()
        {
            var jsonString = "{\"name\": {\"property\": 100}}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            
            var result =
                await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKey("name");
            var nestedObject = (Dictionary<string, object>)result!["name"];
            nestedObject.Count.ShouldBe(1);
            nestedObject["property"].ShouldBe(100L);
        }

        [Fact]
        public async Task GivenObjectPropertyWithArrayProperty_WhenDeserializeAsync_ThenResultContainsSaidPropertyWithNestedDictionaryAndArrayProperty()
        {
            var jsonString = "{\"name\": {\"property\": [1,2,3]}}";
            var json = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            
            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json, JsonSerializerOptions, CancellationToken.None);

            result!.ShouldContainKey("name");
            var nestedObject = (Dictionary<string, object>)result!["name"];
            nestedObject.ShouldContainKey("property");
            var array = (List<object>)nestedObject["property"];
            array.Count.ShouldBe(3);
            array.ShouldContain(1L);
            array.ShouldContain(2L);
            array.ShouldContain(3L);
        }
    }
}
