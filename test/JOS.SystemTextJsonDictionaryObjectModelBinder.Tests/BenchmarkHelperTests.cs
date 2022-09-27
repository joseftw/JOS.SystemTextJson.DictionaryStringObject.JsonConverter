using JOS.SystemTextJsonDictionary.Benchmarks;
using Shouldly;
using Xunit;

namespace JOS.SystemTextJsonDictionaryObjectJsonConverter.Tests;

public class BenchmarkHelperTests
{
    private readonly BenchmarkHelper _sut;
    
    public BenchmarkHelperTests()
    {
        _sut = new BenchmarkHelper();
    }

    [Fact]
    public void DeserializeDefault_ShouldNotThrowExceptionWhenCalledInSuccession()
    {
        Should.NotThrow(() => _sut.DeserializeDefault());
        Should.NotThrow(() => _sut.DeserializeDefault());
        Should.NotThrow(() => _sut.DeserializeDefault());
    }
    
    [Fact]
    public void DeserializeCustom_ShouldNotThrowExceptionWhenCalledInSuccession()
    {
        Should.NotThrow(() => _sut.DeserializeCustom());
        Should.NotThrow(() => _sut.DeserializeCustom());
        Should.NotThrow(() => _sut.DeserializeCustom());
    }
    
    [Fact]
    public void SerializeDefault_ShouldNotThrowExceptionWhenCalledInSuccession()
    {
        Should.NotThrow(() => _sut.SerializeDefault());
        Should.NotThrow(() => _sut.SerializeDefault());
        Should.NotThrow(() => _sut.SerializeDefault());
    }
    
    [Fact]
    public void SerializeCustom_ShouldNotThrowExceptionWhenCalledInSuccession()
    {
        Should.NotThrow(() => _sut.SerializeCustom());
        Should.NotThrow(() => _sut.SerializeCustom());
        Should.NotThrow(() => _sut.SerializeCustom());
    }
}