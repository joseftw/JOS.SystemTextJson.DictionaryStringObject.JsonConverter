using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Xunit;

namespace JOS.SystemTextJsonDictionaryObjectJsonConverter.Tests
{
    public class DictionaryObjectJsonModelBinderTests
    {
        private readonly DictionaryObjectJsonModelBinder _sut;

        public DictionaryObjectJsonModelBinderTests()
        {
            _sut = new DictionaryObjectJsonModelBinder(Substitute.For<ILogger<DictionaryObjectJsonModelBinder>>());
        }

        [Theory]
        [InlineData(typeof(Dictionary<string, string>))]
        [InlineData(typeof(object))]
        [InlineData(typeof(Dictionary<string, int>))]
        [InlineData(typeof(List<object>))]
        public async Task GivenNonDictionaryStringObject_WhenBindModelAsync_ThenThrowsException(Type type)
        {
            var modelBindingContext = Substitute.For<ModelBindingContext>();
            modelBindingContext.ModelType.Returns(type);
            var httpContext = new DefaultHttpContext();
            var json = "{}";
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));
            modelBindingContext.HttpContext.Returns(httpContext);

            var exception = await Should.ThrowAsync<NotSupportedException>(() => _sut.BindModelAsync(modelBindingContext));

            exception.Message.ShouldBe($"The 'DictionaryObjectJsonModelBinder' model binder should only be used on Dictionary<string, object>, it will not work on '{type.Name}'");
        }

        [Fact]
        public async Task GivenPlainObject_WhenBindModelAsync_ThenModelIsDictionaryStringObject()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": \"string\"}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = modelBindingContext.Result.Model;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.ShouldBeOfType<Dictionary<string, object>>();
        }

        [Fact]
        public async Task GivenObjectWithStringProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyAsString()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": \"string\"}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKeyAndValue("name", "string");
        }

        [Fact]
        public async Task GivenObjectWithStringPropertyWithDateTimeValue_WhenBindModelAsync_ThenModelContainsSaidPropertyAsDateTime()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": \"2020-01-23T01:02:03Z\"}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKeyAndValue("name", new DateTime(2020, 1, 23, 1, 2, 3, DateTimeKind.Utc));
        }

        [Fact]
        public async Task GivenObjectWithIntProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyAsLong()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": 1}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKeyAndValue("name", 1L);
        }

        [Fact]
        public async Task GivenObjectWithDecimalProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyAsDecimal()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": 1.234}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKeyAndValue("name", 1.234M);
        }

        [Fact]
        public async Task GivenObjectWithBoolProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyAsBool()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": true}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKeyAndValue("name", true);
        }

        [Fact]
        public async Task GivenObjectWithNullPropertyValue_WhenBindModelAsync_ThenModelContainsSaidPropertyWithNullValue()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": null}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result!.ShouldContainKeyAndValue("name", null);
        }

        [Fact]
        public async Task GivenObjectWithArrayProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyWithArrayValue()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": [1,2,3]}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKey("name");
            var array = (List<object>)result["name"];
            array.Count.ShouldBe(3);
            array.ShouldContain(1L);
            array.ShouldContain(2L);
            array.ShouldContain(3L);
        }

        [Fact]
        public async Task GivenObjectWithObjectProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyWithNestedDictionary()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": {\"property\": 100}}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKey("name");
            var nestedObject = (Dictionary<string, object>)result["name"];
            nestedObject.Count.ShouldBe(1);
            nestedObject["property"].ShouldBe(100L);
        }

        [Fact]
        public async Task GivenObjectPropertyWithArrayProperty_WhenBindModelAsync_ThenModelContainsSaidPropertyWithNestedDictionaryAndArrayProperty()
        {
            var modelBindingContext = CreateModelBindingContext("{\"name\": {\"property\": [1,2,3]}}");

            await _sut.BindModelAsync(modelBindingContext);
            var result = (Dictionary<string, object>)modelBindingContext.Result.Model!;

            modelBindingContext.Result.IsModelSet.ShouldBeTrue();
            result.Count.ShouldBe(1);
            result.ShouldContainKey("name");
            var nestedObject = (Dictionary<string, object>)result["name"];
            nestedObject.ShouldContainKey("property");
            var array = (List<object>)nestedObject["property"];
            array.Count.ShouldBe(3);
            array.ShouldContain(1L);
            array.ShouldContain(2L);
            array.ShouldContain(3L);
        }

        private static ModelBindingContext CreateModelBindingContext(string json)
        {
            var modelBindingContext = Substitute.For<ModelBindingContext>();
            modelBindingContext.ModelType.Returns(typeof(Dictionary<string, object>));
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));
            modelBindingContext.HttpContext.Returns(httpContext);
            return modelBindingContext;
        }
    }
}
